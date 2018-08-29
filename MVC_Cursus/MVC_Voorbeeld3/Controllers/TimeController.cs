using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Voorbeeld3.Controllers
{
    public class TimeController : Controller
    {
        // GET: Time
        [ChildActionOnly]
        public PartialViewResult ShowTime()
        {
            DateTime time = DateTime.Now;
            return PartialView(time);
        }
    }
}