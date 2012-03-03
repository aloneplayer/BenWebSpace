using MvcLibrary.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using MvcLibrary.Models;

namespace MvcLibrary.Tests.Controllers
{
    [TestClass()]
    public class ProductControllerTest
    {

        [TestMethod()]
        public void DetailsViewTest()
        {
            ProductController controller = new ProductController(); // TODO: Initialize to an appropriate value
            ViewResult result = controller.Details(2) as ViewResult;

            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod()]
        public void TestDetailsViewData()
        {
            ProductController controller = new ProductController(); // TODO: Initialize to an appropriate value
            ViewResult result = controller.Details(2) as ViewResult;
            Product product = result.ViewData.Model as Product;
            Assert.AreEqual("Test", product.Name);
        }

        [TestMethod()]
        public void TestDetailsViewRedirect()
        {
            ProductController controller = new ProductController(); // TODO: Initialize to an appropriate value
            RedirectToRouteResult result = controller.Details(-1) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
