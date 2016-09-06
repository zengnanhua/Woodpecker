/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 9:55:15

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NanHuaDDD.Authorization.MVC
{
    /// <summary>
    /// 关于SSO单点登陆的实现网站需要使用它
    /// </summary>
    public class SSOActionFilter : System.Web.Mvc.ActionFilterAttribute
    {

        private readonly HttpServerUtility Server = System.Web.HttpContext.Current.Server;
        string passPortUri = System.Configuration.ConfigurationManager.AppSettings["getssoUrl"] ?? "http://www.passport.com/Home/Login?BackURL=";
        string apiUri = System.Configuration.ConfigurationManager.AppSettings["getApiUrl"] ?? "http://www.passport.com/Home/TokenGetCredence?tokenValue=";
        string getTokenUri = System.Configuration.ConfigurationManager.AppSettings["getTokenUri"] ?? "http://www.passport.com/Home/GetTokenUrl?BackURL=";

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var Request = filterContext.HttpContext.Request;
            var Response = filterContext.HttpContext.Response;
            var Session = filterContext.HttpContext.Session;
            if (Session["Token"] != null)
            {
                //分站凭证存在
                Response.Write("恭喜，分站凭证存在，您被授权访问该页面！");
            }
            else
            {
                //令牌验证结果
                if (Request.QueryString["Token"] != null)
                {
                    if (Request.QueryString["Token"] != "$Token$")
                    {
                        //持有令牌
                        string tokenValue = Request.QueryString["Token"];
                        //调用WebService获取主站凭证

                        object o = new WebClient().DownloadString(apiUri + tokenValue);
                        if (o != null)
                        {
                            //凭证正确
                            Session["Token"] = o;
                            //序列化用户的其它相关信息
                            Response.Write("恭喜，令牌存在，您被授权访问该页面！");
                        }
                        else
                        {
                            //凭证错误
                            filterContext.Result = new RedirectResult(this.replaceToken());
                        }
                    }
                    else
                    {
                        //未持有令牌
                        filterContext.Result = new RedirectResult(this.replaceToken());
                    }
                }
                //未进行令牌验证，去主站验证
                else
                {
                    filterContext.Result = new RedirectResult(this.getTokenURL());
                }
            }
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// 获取带令牌请求的URL
        /// 在当前URL中附加上令牌请求参数
        /// </summary>
        /// <returns></returns>
        private string getTokenURL()
        {
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            Regex reg = new Regex(@"^.*\?.+=.+$");
            if (reg.IsMatch(url))
                url += "&Token=$Token$";
            else
                url += "?Token=$Token$";
            return getTokenUri + Server.UrlEncode(url);
        }
        /// <summary>
        /// 去掉URL中的令牌
        /// 在当前URL中去掉令牌参数
        /// </summary>
        /// <returns></returns>
        private string replaceToken()
        {
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            url = Regex.Replace(url, @"(\?|&)Token=.*", "", RegexOptions.IgnoreCase);
            return passPortUri + Server.UrlEncode(url);
        }
    }
}
