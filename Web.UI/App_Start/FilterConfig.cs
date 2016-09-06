using System.Web;
using System.Web.Mvc;

namespace Web.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //注册全局 权限检查 过滤器
            filters.Add(new Web.Admin.Filters.CheckPermissionAttribute());
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new Web.Admin.Filters.ExceptionLogAttribute());
        }
    }
}