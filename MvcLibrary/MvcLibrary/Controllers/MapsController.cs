using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    /// <summary>
    /// This controller is used for unit testing
    /// </summary>
    public class MapsController : Controller
    {
        public ActionResult ViewMaps()
        {
            return View();
        }

    }
}
