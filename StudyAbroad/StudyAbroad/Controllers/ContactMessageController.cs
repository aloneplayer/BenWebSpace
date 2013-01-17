using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyAbroad.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactMessageController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
    }
}
