using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SynthenticFinancialManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Manage the bank transactions";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Technical test using ASP.Net v4 with razor.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Fredy Vasquez";

            return View();
        }
    }
}
