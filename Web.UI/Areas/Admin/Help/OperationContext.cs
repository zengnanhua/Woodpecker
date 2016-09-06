using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.SessionState;
using NanHuaDDD.Util.CString;
using System.Web.Mvc;
namespace Web.Admin.Help
{
    public class OperationContext
    {
        
        /// <summary>
        /// 用户Session键
        /// </summary>
        public const string USER_SESSION_KEY = "UserInfo";
        /// <summary>
        /// 用户权限集合Session键
        /// </summary>
        public const string USER_MENU_SESSION_KEY = "user_Menu_list";
        /// <summary>
        /// 用户id Cookie 键
        /// </summary>
        public const string USERID_COOKIE_KEY = "UserInfoId";
        /// <summary>
        /// 
        /// </summary>
        public const string USERMENUSTR = "UserMenuStr";
        private const int ExpiresMinutes=50;

        public static OperationContext current
        {
            get
            {
                //1.从线程中取出 键 对应的值
                var opeContext = CallContext.GetData(typeof(OperationContext).FullName) as OperationContext;
                //2.如果为空（线程中不存在）
                if (opeContext == null)
                {
                    opeContext = new OperationContext();
                    //4.并存入线程
                    CallContext.SetData(typeof(OperationContext).FullName, opeContext);
                }
                //5.返回
                return opeContext;
            }
        }

        //---------------------------------业务仓储属性-----------------------------------------------------
        #region 
        private IService.IServiceSession _bllServices;
        public IService.IServiceSession BllServices
        {
            get
            {
                if (_bllServices == null)
                {
                    _bllServices = NanHuaDDD.IoC.IoCFactory.Instance.CurrentContainer.Resolve<IService.IServiceSession>();
                }
                return _bllServices;
            }
        }
        #endregion

        //---------------------------------Http各种属性的便捷访问-----------------------------------------------------
        #region Http各种属性的便捷访问
        public HttpContext ContextHttp
        {
            get
            {
                return HttpContext.Current;
            }
        }
        public HttpSessionState Session
        {
            get
            {
                return ContextHttp.Session;
            }
        }
        public HttpResponse Response
        {
            get
            {
                return ContextHttp.Response;
            }
        }

        public HttpRequest Request
        {
            get
            {
                return ContextHttp.Request;
            }
        }
        #endregion
        //-----------------------------公共属性方法--------------------------------------------------

        #region 操作session中的登录用户对象 +CurrentUserInfo
        /// <summary>
        /// 操作session中的登录用户对象
        /// </summary>
        public EFEntity.UserInfo CurrentUserInfo
        {
            get 
            {
                return Session[USER_SESSION_KEY] as EFEntity.UserInfo;
            }
            set
            {
                Session[USER_SESSION_KEY] = value;
            }
        }
        #endregion

        #region 操作Session中的登录用户的权限集合 +UserMenus
        public List<EFEntity.Menu> UserMenus
        {
            get 
            {
                return Session[USER_MENU_SESSION_KEY] as List<EFEntity.Menu>;
            }
            set 
            {
                Session[USER_MENU_SESSION_KEY] = value;
            }
        }
        #endregion

        #region 操作Cookie中的登录用户ID
        public string UserNameCookie
        {
            set
            {
                HttpCookie cookie = new HttpCookie(USERID_COOKIE_KEY, value.ToString().ToEncyptFormsAuthenticationString());
                cookie.Expires = DateTime.Now.AddMinutes(ExpiresMinutes);
                Response.Cookies.Add(cookie);
            }
            get
            {
                HttpCookie cookie = Request.Cookies[USERID_COOKIE_KEY];
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    return cookie.Value.ToDecyptFormsAuthenticationString();
                }
                else 
                {
                    return null;
                }
            }
        }
        #endregion

        #region 操作菜单转换成Html +UserMenuStr
        public string UserMenuStr
        {
            get {
                if (Session[USERMENUSTR]==null)
                {
                    Web.EFEntity.MenuModel.MenuTreeHtml menuTreeHeml = new EFEntity.MenuModel.MenuTreeHtml();
                    string menuStr=menuTreeHeml.GetHtmlMennTree(this.UserMenus.Where(c=>c.Type==1&&!c.IsDelete.Value).ToList(), 1);
                    Session[USERMENUSTR] = menuStr;
                    return menuStr;
                }
                return Session[USERMENUSTR].ToString();
               
            }
            private set {
                Session[USERMENUSTR] = value;
            }
        }
        #endregion

        #region 根据url 获取 当前登录用户 权限对象 +Permission GetUsrPermission(string strAreaName, string strControllerName, string strActionName, string strFormMethod)
        public EFEntity.Menu GetUserMenuPermission(string strAreaName, string strControllerName, string strActionName, string strFormMethod)
        {
            int formMethod = strFormMethod.ToLower() == "get" ? 1 : 2;
            if (UserMenus == null)
            {
                return null;
            }
            var curPer = UserMenus.SingleOrDefault(c => c.Area.IsSame(strAreaName)
                &&c.ControllerName.IsSame(strControllerName)
                &&c.ActionName.IsSame(strActionName)
                && (c.Method == 3 ? true : (c.Method == formMethod))
                );
            return curPer;
        }
        #endregion
        
        #region  检查 当前登录用户 是否有 访问权限 + bool HasPermission
        /// <summary>
        /// 1.2 检查 当前登录用户 是否有 访问权限
        /// </summary>
        /// <param name="strAreaName">当前访问的区域名</param>
        /// <param name="strControllerName">当前访问的控制器名</param>
        /// <param name="strActionName">当前访问的Action方法名</param>
        /// <param name="strFormMethod">当前的请求方式(GET/POST)</param>
        /// <returns></returns>
        public bool HasPermission(string strAreaName, string strControllerName, string strActionName, string strFormMethod)
        {
            return GetUserMenuPermission(strAreaName, strControllerName, strActionName, strFormMethod) != null;
        }
        #endregion


        //--------------------------------通用返回消息方法--------------------
        #region 1.0 返回 针对Ajax 的统一格式的消息字符串（JSON）+AjaxMsg(AjaxMsgStatu statu, string strMsg, string strBackUrl, object data = null)
        /// <summary>
        /// 1.0 返回 针对Ajax 的统一格式的消息字符串（JSON）
        /// </summary>
        /// <param name="statu"></param>
        /// <param name="strMsg"></param>
        /// <param name="strBackUrl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult AjaxMsg(AjaxMsgStatu statu, string strMsg, string strBackUrl, object data = null)
        {
            AjaxMsg msgObj = new AjaxMsg()
            {
                Status = statu,
                Msg = strMsg,
                BackUrl = strBackUrl,
                Data = data
            };
            return new JsonResult()
            {
                Data = msgObj,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        #region 1.0.1 返回各种消息的 快捷方法 AjaxMsgOK，AjaxMsgNOOK，AjaxMsgError 等
        public JsonResult AjaxMsgOK(string strMsg = "操作成功~", string strBackUrl = "", object data = null)
        {
            return AjaxMsg(AjaxMsgStatu.Ok, strMsg, strBackUrl, data);
        }

        public JsonResult AjaxMsgNOOK(string strMsg = "操作失败~", string strBackUrl = "", object data = null)
        {
            return AjaxMsg(AjaxMsgStatu.NoOk, strMsg, strBackUrl, data);
        }

        public JsonResult AjaxMsgError(string strMsg = "操作异常~", string strBackUrl = "", object data = null)
        {
            return AjaxMsg(AjaxMsgStatu.Error, strMsg, strBackUrl, data);
        }

        public JsonResult AjaxMsgError(Exception ex, string strBackUrl = "", object data = null)
        {
            return AjaxMsg(AjaxMsgStatu.Error, ex.Message, strBackUrl, data);
        }

        public JsonResult AjaxMsgNoLogin(string strBackUrl = "", object data = null)
        {
            return AjaxMsg(AjaxMsgStatu.NoLogin, "尚未登录~~", strBackUrl, data);
        }

        public JsonResult AjaxMsgNoPermission(string strBackUrl = "", object data = null)
        {
            return AjaxMsg(AjaxMsgStatu.NoPermission, "您没有访问此操作的权限~~~", strBackUrl, data);
        }
        #endregion

        #region 2.0 返回 js 提示 和 跳转 代码片段 +ContentResult JsMsg(string strMsg, string strBackUrl = "")
        /// <summary>
        /// 2.0 返回 js 提示 和 跳转 代码片段
        /// </summary>
        /// <param name="strMsg">消息</param>
        /// <param name="strBackUrl">跳转页面的url</param>
        /// <returns></returns>
        public ContentResult JsMsg(string strMsg, string strBackUrl = "")
        {
            System.Text.StringBuilder sbJs = new System.Text.StringBuilder("<script>alert(\"").Append(strMsg).Append("\");");
            if (!strBackUrl.IsNullOrEmpty())
            {
                sbJs.Append("if(window.top!=window)window.top.location=\"").Append(strBackUrl).Append("\";");
                sbJs.Append("else window.location=\"").Append(strBackUrl).Append("\";");
            }
            sbJs.Append("</script>");
            return new ContentResult()
            {
                Content = sbJs.ToString()
            };
        }
        #endregion

        //--------------------------------清楚缓存--------------------
        public void ExitLogin()
        {
            UserMenus = null;
            CurrentUserInfo = null;
            UserNameCookie = "";
            Session[UserMenuStr] = null;
            UserMenuStr =null;
        }
        public void ClearFlush()
        {
            UserMenus = null;
            CurrentUserInfo = null;
            Session[UserMenuStr] = null;
            UserMenuStr = null;
        }
    }




}