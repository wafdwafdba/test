using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_EasyUI_2016.Core;

namespace CRM_EasyUI_2016.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewData["UserId"] = this.UserId;
            return View();
        }

    }
}
