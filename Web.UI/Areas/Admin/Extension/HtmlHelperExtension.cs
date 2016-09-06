using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Web.Admin.Extension
{
    public static class HtmlHelperExtension
    {
        public static System.Web.Mvc.MvcHtmlString GetSonBtnJs(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            System.Text.StringBuilder sbBtnJs = new System.Text.StringBuilder(1000);
            var menus = htmlHelper.ViewBag.sonBtns as List<Web.EFEntity.Menu>;
            
            if (menus != null)
            {
                for (var i = 0; i < menus.Count();i++ )
                {
                    sbBtnJs.Append("{");
                    sbBtnJs.Append("iconCls:'" + menus[i].ToolIco + "',");
                    sbBtnJs.Append("text:'" + menus[i].MenuName + "',");
                    sbBtnJs.Append("handler:function(){" + menus[i].ActionName + "();}");
                    sbBtnJs.Append("}");
                    if (i != menus.Count() - 1)
                    {
                        sbBtnJs.Append(",'-',");
                    }
                }
              
            }
          
            System.Web.Mvc.MvcHtmlString mvcStr = new System.Web.Mvc.MvcHtmlString(sbBtnJs.ToString());
            return mvcStr;
        }
    }
}