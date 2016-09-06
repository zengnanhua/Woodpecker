using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Admin.Controllers
{
    public class BHomeController : Controller
    {
        //
        // GET: /BHome/

        public ActionResult Index()
        {

            return Content("我是后台");
        }

    }
}
