using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models;

namespace MvcLibrary.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return RedirectToAction("Index");
            }
            Product product = new Product()
            {
                ID = 1,
                Name = "Test"
            };

            return View("Details", product);
        }
    }
}
