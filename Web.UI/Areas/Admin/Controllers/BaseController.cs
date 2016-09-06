using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.IService;
using Web.EFEntity;
using Web.Admin.Attr;
using Web.DTOEntity.DTO;
using Web.Admin.Help;
using Web.Admin.Validate;

namespace Web.Admin.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public Web.Admin.Help.OperationContext OpeCur
        {
            get
            {
                return Web.Admin.Help.OperationContext.current;
            }
        }
        #region 1 菜单管理
        public ActionResult MenuManage()
        {
            return View();
        }

        [SkipPermissionAttribute]
        public ActionResult MenuManageJson()
        {
            var tree = OpeCur.BllServices.MenuService.GetMenuTreeJson();
            List<Tree> treeList = new List<Tree>() { tree };
            return Json(treeList, JsonRequestBehavior.AllowGet);
        }


        #region 移动菜单
        public ActionResult DropMenu(int target, int source)
        {
            Menu menu = new Menu() { MenuId = source, FatherID = target };
            OpeCur.BllServices.MenuService.UpdateEntityFields(menu, new List<string> { "FatherID" });
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                return OpeCur.AjaxMsgOK("移动成功~");
            }
            return OpeCur.AjaxMsgNOOK("移动失败~");
        }
        #endregion

        #region 更新菜单视图
        [HttpGet]
        public ActionResult UpdateMenu()
        {
            return View();
        }
        #endregion

        #region 更新菜单
        [HttpPost]
        public ActionResult UpdateMenu(Menu menu)
        {
            Menu temp = OpeCur.BllServices.MenuService.LoadEntities(c => c.MenuId == menu.MenuId).SingleOrDefault();
            temp.MenuName = menu.MenuName;
            temp.OrderNo = menu.OrderNo;
            temp.Area = menu.Area;
            temp.ControllerName = menu.ControllerName;
            temp.ActionName = menu.ActionName;
            OpeCur.BllServices.MenuService.Update(temp);
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                return OpeCur.AjaxMsgOK("更新成功~");
            }

            return OpeCur.AjaxMsgNOOK("更新失败~");
        }
        #endregion

        #region 添加菜单

        public ActionResult AddMenu(int target)
        {

            Menu menu = new Menu()
            {
                MenuName = "new node",
                FatherID = target,
                Type = (int)MenuType.MENU,
                Method = 3,
                IsDelete = false,
                OrderNo = 30
            };
            OpeCur.BllServices.MenuService.Insert(menu);
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                return OpeCur.AjaxMsgOK("添加成功~");
            }

            return OpeCur.AjaxMsgNOOK("添加失败~");
        }
        #endregion

        #region 删除菜单
        public ActionResult DeleteMenu(int source)
        {

            OpeCur.BllServices.MenuService.DeleteMenu(source);
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                return OpeCur.AjaxMsgOK("添加成功~");
            }

            return OpeCur.AjaxMsgNOOK("添加失败~");
        }
        #endregion

        #region 获取子功能列表
        [SkipPermission]
        public ActionResult GetSonMenuBtnJson(int page, int rows, int meunId = 0)
        {
            int total = 0;
            var menu = OpeCur.BllServices.MenuService.LoadPageEntities(rows, page, out total, c => c.FatherID == meunId && c.Type != (int)MenuType.MENU && !c.IsDelete.Value, true, c => c.MenuId).ToList().Select(c => c.ToPOCO()).ToList();
            var json = new
            {
                total = total,
                rows = menu
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加菜单功能
        [HttpGet]
        public ActionResult AddFunctionMenu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFunctionMenu(Menu menu)
        {
            MenuValidation validator = new MenuValidation();
            menu.IsDelete = false;
            var results = validator.Validate(menu);

            if (!results.IsValid)
            {
                return OpeCur.AjaxMsgNOOK(results.Errors[0].ErrorMessage);
            }

            OpeCur.BllServices.MenuService.Insert(menu);
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                return OpeCur.AjaxMsgOK("添加成功");
            }
            return OpeCur.AjaxMsgNOOK("添加失败");

        }
        #endregion
        #region 修改菜单功能
        [HttpGet]
        public ActionResult UpdateFunctionMenu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateFunctionMenu(Menu menu)
        {
            Menu newMenu = OpeCur.BllServices.MenuService.LoadEntities(c => c.MenuId == menu.MenuId).FirstOrDefault();
            if (newMenu != null)
            {
                newMenu.MenuName = menu.MenuName;
                newMenu.Method = menu.Method;
                newMenu.OrderNo = menu.OrderNo;
                newMenu.Type = menu.Type;
                newMenu.ActionName = menu.ActionName;
                newMenu.Area = menu.Area;
                newMenu.ControllerName = menu.ControllerName;
                newMenu.ToolIco = menu.ToolIco;

                MenuValidation validator = new MenuValidation();
                menu.IsDelete = false;
                var results = validator.Validate(newMenu);

                if (!results.IsValid)
                {
                    return OpeCur.AjaxMsgNOOK(results.Errors[0].ErrorMessage);
                }
                OpeCur.BllServices.MenuService.Update(newMenu);
                if (OpeCur.BllServices.SaveChange() > 0)
                {
                    return OpeCur.AjaxMsgOK("更新成功");
                }

            }

            return OpeCur.AjaxMsgNOOK("更新失败");


        }
        #endregion
        #region 删除菜单功能
        public ActionResult DeleteFunctionMenu(int MenuId)
        {
            Menu menu = OpeCur.BllServices.MenuService.LoadEntities(c => c.MenuId == MenuId).FirstOrDefault();
            menu.IsDelete = true;
            OpeCur.BllServices.MenuService.Update(menu);
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                return OpeCur.AjaxMsgOK("删除成功");
            }
            return OpeCur.AjaxMsgNOOK("删除失败");
        }
        #endregion
        #endregion


        #region 2.角色管理
        #region 角色页面
        [HttpGet]
        public ActionResult RoleIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RoleIndex(int page, int rows)
        {

   

            int total = 0;
            var roleList = OpeCur.BllServices.RoleService.LoadPageEntities(rows, page, out total, c => !c.IsDelete.Value, true, c => c.RoleId).ToList().Select(c => c.ToPOCO()).ToList();

            var json = new
            {
                total = total,
                rows = roleList
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加角色
        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(Role role)
        {
            RoleValidation validator = new RoleValidation();
            role.IsDelete = false;
            var results = validator.Validate(role);
            if (!results.IsValid)
            {
                return OpeCur.AjaxMsgNOOK(results.Errors[0].ErrorMessage);
            }
            OpeCur.BllServices.RoleService.Insert(role);
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                return OpeCur.AjaxMsgOK("添加成功");
            }
            return OpeCur.AjaxMsgNOOK("添加失败");
          
        }
        #endregion

        #region 修改角色
        [HttpGet]
        public ActionResult UpdateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateRole(Role role)
        {
            Role newRole = OpeCur.BllServices.RoleService.LoadEntities(c => c.RoleId == role.RoleId).FirstOrDefault();
            newRole.RoleName = role.RoleName;
            newRole.IsLoginBack = role.IsLoginBack;
            newRole.RoleDiscription = role.RoleDiscription;
            OpeCur.BllServices.RoleService.Update(newRole);
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                return OpeCur.AjaxMsgOK("更新成功");
            }
            return OpeCur.AjaxMsgNOOK("更新失败");
        }
        #endregion

        #region 删除角色
        public ActionResult DeleteRole(int RoleId)
        {
            var role= OpeCur.BllServices.RoleService.LoadEntities(c=>c.RoleId==RoleId).FirstOrDefault();
            if(role!=null)
            {
                role.IsDelete = true;
                OpeCur.BllServices.RoleService.Update(role);
                if (OpeCur.BllServices.SaveChange() > 0)
                {
                    return OpeCur.AjaxMsgOK("删除成功");
                }
            }

            return OpeCur.AjaxMsgNOOK("删除失败");

        }
        #endregion

        #region 设置权限
        [HttpGet]
        public ActionResult SetRight()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetRight(int RoleId, string menus="")
        {
            //第一种情况.返回Tree 的json 数据
            if (string.IsNullOrEmpty(menus))
            {
                var tree = OpeCur.BllServices.MenuService.GetRightMenuTree(RoleId);
                List<Tree> treeList = new List<Tree>() { tree };
                return Json(treeList, JsonRequestBehavior.AllowGet);
            }
            //第二种情况
            var r_Role_Menu=OpeCur.BllServices.R_Role_MenuService.LoadEntities(c => c.RoleId == RoleId).ToList();
            #region 删除所有权限和添加所有新权限
            foreach (var r in r_Role_Menu)
            {
                OpeCur.BllServices.R_Role_MenuService.Delete(r);
            }
            if (menus != "clear")
            {
                string[] menuStrArr = menus.Trim(',').Split(',');
                foreach (var s in menuStrArr)
                {
                    OpeCur.BllServices.R_Role_MenuService.Insert(new R_Role_Menu() { MenuId = Convert.ToInt32(s), RoleId = RoleId });
                }
            }
            #endregion
            if (OpeCur.BllServices.SaveChange() > 0)
            {
                OpeCur.ClearFlush();
                return OpeCur.AjaxMsgOK("权限修改成功");
            }
            return OpeCur.AjaxMsgNOOK("权限修改失败");


        }
        #endregion
        #endregion

        #region 3.用户管理
        [HttpGet]
        public ActionResult UserInfoManage()
        {
            return View();
        }
        public ActionResult UserInfoManage(string UserName)
        {
            return Content("");
        }
        #endregion
    }
}
