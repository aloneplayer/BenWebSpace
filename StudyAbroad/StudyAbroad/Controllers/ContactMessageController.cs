using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyAbroad.Attributes;

namespace StudyAbroad.Controllers
{
    [AllowAnonymous]
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
