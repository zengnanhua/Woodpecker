namespace Web.IService
{
    using Web.EFEntity;
    using System;
    using System.Collections.Generic;
    using Web.DTOEntity.DTO;

    public partial interface IMenuService : Web.IService.IBaseService<Menu>
    {
        /// <summary>
        /// 根据用户id查询用户权限集合
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        List<Web.EFEntity.Menu> GetUserPermission(string userName);

        /// <summary>
        /// 获取EasyUI Tree 树结构
        /// </summary>
        /// <returns></returns>
        Tree GetMenuTreeJson();
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="source"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        void DeleteMenu(int source, List<Menu> menu = null);

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        Tree GetRightMenuTree(int RoleId);
    }

}