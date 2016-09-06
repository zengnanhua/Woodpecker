/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 9:50:46

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NanHuaDDD.Authorization.MVC
{
    /// <summary>
    /// 授权过滤器
    /// Function:MVC模式下使用
    /// Author:Lind.zhang
    /// </summary>
    public class AuthorizationLoginFilter : AuthorizeAttribute
    {

        /// <summary>
        /// 验证失败后所指向的控制器和action
        /// 可以在使用特性时为它进行赋值
        /// </summary>
        public AuthorizationLoginFilter(string failControllerName = "Home", string failActionName = "Login")
        {
            _failControllerName = failControllerName;
            _failActionName = failActionName;
        }
        public string _failControllerName, _failActionName;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //被添加AllowAnonymousAttribute特性的过滤器将不参加AuthorizationLoginFilter的验证
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            //为登陆页添加例外，其它页都自动在global.asax里添加到全局过滤器中，MVC3及以后版本支持它
            if (!skipAuthorization)
            {
                //策略1:app登陆，分发sessionId给客户端，之后每次通讯向服务端发这个id
                var sessionId = filterContext.RequestContext.HttpContext.Request.QueryString["SessionId"];
                if (!string.IsNullOrWhiteSpace(sessionId))
                {
                    if (filterContext.RequestContext.HttpContext.Session.SessionID == sessionId)
                    {
                        //app在客户端登陆了,通过sessionid拿到当前的session

                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary { 
                         { "Action",_failActionName },
                         { "Controller", _failControllerName}, 
                         { "returnUrl", HttpContext.Current.Request.Url.ToString() } });
                    }
                }
                //策略2:web登陆，登陆后保存为CurrentUser
                if (true)
                {
                    filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary { 
                     { "Action",_failActionName },
                     { "Controller", _failControllerName}, 
                     { "returnUrl", HttpContext.Current.Request.Url.ToString() } });
                }
            }
        }
    }
}
