/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/16 15:55:46

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Service
{
    using System;
    using System.Collections.Generic;
    using Web.DTOEntity;
    using Web.DTOEntity.DTO;
    using Web.EFEntity;

    public partial class MenuService : Web.Service.BaseService<Web.EFEntity.Menu>, Web.IService.IMenuService
    {

        #region 获取权限
        public List<Web.EFEntity.Menu> GetUserPermission(string userName)
        {
            //1.查询角色权限
            //1.根据用户Id查询 角色Id集合
            List<int> listRoleIds = DBSession.R_UserInfo_RoleRespository.LoadEntities(c => c.UserName == userName).Select(c=>c.RoleId.Value).ToList();

            List<int> listMenuIds = DBSession.R_Role_MenuRespository.LoadEntities(c=>listRoleIds.Contains(c.RoleId.Value)).Select(c=>c.MenuId.Value).ToList();

            List<int> listVipIds = DBSession.OtherMenuRespository.LoadEntities(c=>c.UserName==userName).Select(c=>c.MenuId.Value).ToList();

            if (listMenuIds == null)
            {
                listMenuIds = new List<int>();
            }

            listVipIds.ForEach(listVipId => {
                if (!listMenuIds.Contains(listVipId))
                {
                    listMenuIds.Add(listVipId);
                }
            });

            return DBSession.MenuRespository.LoadEntities(c => listMenuIds.Contains(c.MenuId)).ToList().Select(c=>c.ToPOCO()).OrderBy(c=>c.OrderNo).ToList();
        }
        #endregion

        #region 获取json Tree树
        public Tree GetMenuTreeJson()
        {
            List<Menu> menus = this.LoadEntities(c =>c.Type==1&&!c.IsDelete.Value).ToList();
            Menu Gen = menus.Where(c => c.MenuId == 1).SingleOrDefault();
            if (Gen!=null)
            {
                Tree sonTree = AutoMapperHelper.Map<Menu, Tree>(Gen);

                GetMenuListTree(menus, sonTree);

                return sonTree;
            }
            return null;
        }
        #endregion

        #region 获取角色权限Tree
        public Tree GetRightMenuTree(int RoleId)
        {
            List<Menu> menus = iSonDal.LoadEntities(c=>!c.IsDelete.Value).ToList();
            Menu Gen = menus.Where(c => c.MenuId == 1).SingleOrDefault();
            Tree sonTree = AutoMapperHelper.Map<Menu, Tree>(Gen);
            List<R_Role_Menu> roleMenus= DBSession.R_Role_MenuRespository.LoadEntities(c => c.RoleId == RoleId).ToList();
            GetRightMenuListTree(menus, sonTree, roleMenus);
            return sonTree;
        }

        #endregion
        #region 删除菜单
        public void DeleteMenu(int source, List<Menu> menu = null)
        {
            if (menu == null)
            {
                menu = this.LoadEntities(c => !c.IsDelete.Value).ToList();
                List<Menu> m = menu.Where(c => c.MenuId == source).ToList();
                if (m.Count() <= 0)
                {
                    return;
                }
            }
            List<Menu> sonMenu = menu.Where(c => c.FatherID == source).ToList();
            if (sonMenu.Count() > 0)
            {
                foreach (var n in sonMenu)
                {
                    DeleteMenu(n.MenuId, menu);
                }
            }
            else
            {
                List<Menu> m = menu.Where(c => c.MenuId == source).ToList();
                m[0].IsDelete =true;
                this.Update(m[0]);
                //this.UpdateEntityFields(new Menu() { MenuId = source, MenuName = "bb", IsStatus = "1" }, new List<string> { "IsStatus" });
            }
            return;
        }
        #endregion

        #region -------------------------------私有方法---------------------------------------------
        #region 递归得到Menu
        public void GetMenuListTree(List<Menu> menus, Tree tree)
        {
            if (tree == null)
            {
                return;
            }
            List<Menu> sonMenu = menus.Where(m => m.FatherID == Convert.ToInt32(tree.id) && !m.IsDelete.Value).ToList();
            if (sonMenu.Count() > 0)
            {
                tree.children = new List<Tree>();
                foreach (var m in sonMenu)
                {
                    Tree sonTree = AutoMapperHelper.Map<Menu, Tree>(m);
                    List<Menu> sonsonMenu = menus.Where(c => c.FatherID == m.MenuId && !m.IsDelete.Value).ToList();
                    tree.children.Add(sonTree);
                    if (sonsonMenu.Count() > 0)
                    {
                        GetMenuListTree(menus, sonTree);
                    }
                }
            }
        }
        #endregion

        #region 获取角色权限
        private void GetRightMenuListTree(List<Menu> menus, Tree tree, List<R_Role_Menu> roleMenu)
        {
            if (tree == null)
            {
                return;
            }
            List<Menu> sonMenu = menus.Where(m => m.FatherID == Convert.ToInt32(tree.id) && !m.IsDelete.Value).ToList();
            if (sonMenu.Count() > 0)
            {
                tree.children = new List<Tree>();
                foreach (var m in sonMenu)
                {
                    Tree sonTree = AutoMapperHelper.Map<Menu, Tree>(m);
                   
                    tree.children.Add(sonTree);
                    List<Menu> sonsonMenu = menus.Where(c => c.FatherID == m.MenuId && !c.IsDelete.Value).ToList();
                    if (sonsonMenu.Count() > 0)
                    {
                        GetRightMenuListTree(menus, sonTree, roleMenu);
                    }
                    else {
                        if (roleMenu.Where(c => c.MenuId == m.MenuId).ToList().Count() > 0)
                        {
                            sonTree.@checked = "true";
                        }
                        else
                        {
                            sonTree.@checked ="";
                        }
                    }
                }
            }
        }
        #endregion
        #endregion



    }
}
