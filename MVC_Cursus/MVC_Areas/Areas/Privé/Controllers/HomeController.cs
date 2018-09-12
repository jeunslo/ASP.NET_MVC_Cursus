using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Areas.Areas.Privé.Controllers
{
    public class HomeController : Controller
    {
        // GET: Privé/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}