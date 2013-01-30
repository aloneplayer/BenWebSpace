using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyAbroad.Controllers
{
    public class SubpageController : Controller
    {
        //
        // GET: /Subpage/

        public ActionResult About()
        {
            return View();
        }

        public ActionResult StudyInUS()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
      
        public ActionResult Resources()
        {
            return View();
        }
    }
}
