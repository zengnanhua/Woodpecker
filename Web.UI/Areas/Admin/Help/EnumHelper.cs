using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Admin.Help
{
    public static class IconClassName
    {
        static List<System.Web.Mvc.SelectListItem> _ddlData = null;
        public static List<System.Web.Mvc.SelectListItem> DDLData
        {
            get
            {
                if (_ddlData == null)
                    _ddlData = new List<System.Web.Mvc.SelectListItem>() { 
                       new System.Web.Mvc.SelectListItem(){Value="icon-add",Text="icon-add" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-edit",Text="icon-edit" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-remove",Text="icon-remove" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-cut",Text="icon-cut" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-save",Text="icon-save" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-ok",Text="icon-ok" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-no",Text="icon-no" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-cancel",Text="icon-cancel" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-search",Text="icon-search" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-tip",Text="icon-tip" },
                       new System.Web.Mvc.SelectListItem(){Value="icon-filter",Text="icon-filter" }
                    };
                return _ddlData;
            }
        }
    }
}