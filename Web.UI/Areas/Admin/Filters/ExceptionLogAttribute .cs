using NanHuaDDD.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Admin.Filters
{
    public class ExceptionLogAttribute : HandleErrorAttribute
    {
        private Web.Admin.Help.OperationContext opeCur = new Web.Admin.Help.OperationContext();
        public override void OnException(ExceptionContext filterContext)
        {
            string message = string.Format("消息类型：{0}<br>消息内容：{1}<br>引发异常的方法：{2}<br>引发异常的对象：{3}<br>异常目录：{4}<br>异常文件：{5}"
                 , filterContext.Exception.GetType().Name
                 , filterContext.Exception.Message
                 , filterContext.Exception.TargetSite
                 , filterContext.Exception.Source
                 , filterContext.RouteData.GetRequiredString("controller")
                , filterContext.RouteData.GetRequiredString("action"));
            LoggeQueueOutPut.WriteLog(message);

            //设置异常消息
            //设置异常消息
            filterContext.Result = SendMsg(filterContext.Exception, "");///admin/login/index
            //告诉asp.net mvc框架 异常已经被处理了，不需要再由框架 生成 错误页面了！
            filterContext.ExceptionHandled = true;
           
        }
        #region 3.0 根据是否为异步请求 返回不同的消息 +ActionResult SendMsg(string strMsg, string strBackUrl)
        /// <summary>
        /// 3.0 根据是否为异步请求 返回不同的消息
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="strBackUrl"></param>
        /// <returns></returns>
        System.Web.Mvc.ActionResult SendMsg(Exception ex, string strBackUrl)
        {
            //1.判断请求报文中是否包含 X-Requested-With: XMLHttpRequest
            //1.1如果包含，则代表是 浏览器端通过 jquery异步方法 创建的 异步对象请求的
            if (opeCur.Request.Headers.AllKeys.Contains("X-Requested-With"))
            {
                return opeCur.AjaxMsgError(ex, strBackUrl);
            }
            //1.2如果不包含，则代表是浏览器直接请求的
            else
            {
                return opeCur.JsMsg(ex.Message, strBackUrl);
            }
        }
        #endregion
    }
}