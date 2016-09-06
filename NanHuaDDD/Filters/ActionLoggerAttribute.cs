/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 10:57:41

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace NanHuaDDD.Filters
{
    /// <summary>
    ///  API页面加载过程的进行日志记录
    /// </summary>
    public class ActionLoggerAttribute : ActionFilterAttribute, IFilter
    {
        Stopwatch sw;
        StringBuilder message = new StringBuilder();
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {


            sw = new Stopwatch();
            sw.Start();
            message.Append("页面被访问，Uri:" + actionContext.Request.RequestUri.AbsoluteUri);
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            sw.Stop();
            message.Append("，页面加载完成，所用的时间：" + sw.ElapsedMilliseconds + "(ms)，当前时间：" + DateTime.Now);
            Logger.LoggerFactory.Instance.Logger_Debug(message.ToString());
            base.OnActionExecuted(actionExecutedContext);
        }

    }
    /// <summary>
    /// Mvc页面加载过程的进行日志记录
    /// </summary>
    public class MvcActionLoggerAttribute : System.Web.Mvc.ActionFilterAttribute
    {

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            //Cat上下文存储到当前请求的ViewBag里
            filterContext.Controller.ViewBag.CatContext = "";
            sw = new Stopwatch();
            sw.Start();
            message.Append("页面被访问，Uri:" + filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri);
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            sw.Stop();
            message.Append("，页面加载完成，所用的时间：" + sw.ElapsedMilliseconds + "(ms)，当前时间：" + DateTime.Now);
            Logger.LoggerFactory.Instance.Logger_Debug(message.ToString());
            base.OnActionExecuted(filterContext);
        }
        Stopwatch sw;
        StringBuilder message = new StringBuilder();



    }

}
