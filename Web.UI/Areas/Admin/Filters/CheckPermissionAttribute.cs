using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Admin.Help;

namespace Web.Admin.Filters
{
    public class CheckPermissionAttribute : System.Web.Mvc.AuthorizeAttribute
    {

        private Web.Admin.Help.OperationContext opeCur = new Web.Admin.Help.OperationContext();
        /// <summary>
        /// 区域黑名单
        /// </summary>
        List<string> blackAreaNames = new List<string>() { "admin" };
        
        /// <summary>
        /// 授权方法-在此检查权限
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                string strCurAreaName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
                if (blackAreaNames.Contains(strCurAreaName))
                {
                    if (!IsDefind<Web.Admin.Attr.SkipLoginAttribute>(filterContext))
                    {
                        if (IsLogin())
                        {
                            //登录了加载指定东西
                            filterContext.Controller.ViewBag.UserInfo= opeCur.CurrentUserInfo;
                            filterContext.Controller.ViewBag.MenuStr = opeCur.UserMenuStr;
                            LoadMenuBtns(filterContext);
                            //判断 当前访问的控制器后方法 是否有贴【SkipPer】
                            if (!IsDefind<Web.Admin.Attr.SkipPermissionAttribute>(filterContext))
                            {
                                //检查登录用户是否有 访问当前url的权限
                                if (!opeCur.HasPermission(strCurAreaName,
                                     filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                                     filterContext.ActionDescriptor.ActionName,
                                     opeCur.Request.HttpMethod))
                                {
                                    filterContext.Result = SendMsg("您没有进行此项操作的权限~~~","");
                                }

                            }

                        }
                        else
                        {
                            filterContext.Result = SendMsg("您尚未登录~~~", "/admin/login/index");
                        }
                    }
                }
            }
        }

        #region 检查 过滤器上下文 中的当前被请求的方法 和 控制器 是否有贴标签 -- bool IsDefind<AttrType>(System.Web.Mvc.AuthorizationContext filterContext)
        /// <summary>
        /// 检查 过滤器上下文 中的当前被请求的方法 和 控制器 是否有贴标签
        /// </summary>
        /// <typeparam name="AttrType">要检查的标签类型</typeparam>
        /// <param name="filterContext">过滤器上下文</param>
        /// <returns></returns>
        bool IsDefind<AttrType>(System.Web.Mvc.AuthorizationContext filterContext)
        {
            //获取要检查的标签 的 类型对象
            Type attrTypeObj = typeof(AttrType);
            //分别检查 被请求的方法 和 控制器上 是否有贴 指定的标签，如果任意贴了，则返回true
            return filterContext.ActionDescriptor.IsDefined(attrTypeObj, false)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(attrTypeObj, false);
        }
        #endregion

        #region 1.0 判断当前访问用户 是否登录 -bool IsLogin()
        private bool IsLogin()
        {
            //先判断是否有Session
            if (opeCur.CurrentUserInfo == null)
            {
                if (string.IsNullOrEmpty(opeCur.UserNameCookie))
                {
                    return false;
                }
                else 
                {
                    var UserName = opeCur.UserNameCookie;
                    //根据cookie里的用户id重新查询用户，并存入Session 【自动登录】
                    var sigleUserInfo = opeCur.BllServices.UserInfoService.LoadEntities(c => c.UserName == UserName).SingleOrDefault();
                    if (sigleUserInfo == null)
                    {
                        return false;
                    }
                    opeCur.CurrentUserInfo = sigleUserInfo;
                    opeCur.UserMenus = opeCur.BllServices.MenuService.GetUserPermission(UserName);
                  

                }
            }

            return true;
        }
        #endregion

        #region  根据当前访问的页面 查找 登录用户的 子按钮权限 +void LoadMenuBtns(System.Web.Mvc.AuthorizationContext filterContext)
        private void LoadMenuBtns(System.Web.Mvc.AuthorizationContext filterContext)
        {
            //获取当前请求url数据
            string strCurAreaName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
            string strControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string strActionName = filterContext.ActionDescriptor.ActionName;

            EFEntity.Menu menuPer = opeCur.GetUserMenuPermission(strCurAreaName, strControllerName, strActionName, opeCur.Request.HttpMethod);
            //2如果存在此权限在，则加载用户 在此页面的 按钮集合
            if (menuPer != null)
            {
                //再根据菜单权限 去 当前登录用户Session的 权限集合中 查找 子按钮权限集合
                var sonBtns = opeCur.UserMenus.Where(c => c.FatherID == menuPer.MenuId && c.Type == (int)MenuType.BUTTON && !c.IsDelete.Value).OrderBy(c => c.OrderNo).ToList();
                if (sonBtns == null)
                    filterContext.Controller.ViewBag.sonBtns = emptyBtns;
                else
                    filterContext.Controller.ViewBag.sonBtns = sonBtns;
            }
            else
            {
                filterContext.Controller.ViewBag.sonBtns = emptyBtns;
            }
        }

        static List<EFEntity.Menu> emptyBtns = new List<EFEntity.Menu>();
        #endregion

        #region 3.0 根据是否为异步请求 返回不同的消息 +ActionResult SendMsg(string strMsg, string strBackUrl)
        /// <summary>
        /// 3.0 根据是否为异步请求 返回不同的消息
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="strBackUrl"></param>
        /// <returns></returns>
        System.Web.Mvc.ActionResult SendMsg(string strMsg, string strBackUrl)
        {
            //1.判断请求报文中是否包含 X-Requested-With: XMLHttpRequest
            //1.1如果包含，则代表是 浏览器端通过 jquery异步方法 创建的 异步对象请求的
            if (opeCur.Request.Headers.AllKeys.Contains("X-Requested-With"))
            {
                return opeCur.AjaxMsgNOOK(strMsg, strBackUrl);
            }
            //1.2如果不包含，则代表是浏览器直接请求的
            else
            {
                return opeCur.JsMsg(strMsg, strBackUrl);
            }
        }
        #endregion
    }
}