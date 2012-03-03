using MvcLibrary.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;

namespace MvcLibrary.Tests.Controllers
{
    /// <summary>
    ///This is a test class for MapControllerTest and is intended
    ///to contain all MapControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MapsControllerTest
    {
        [TestMethod()]
        public void ViewMapsTest()
        {
            // Arrange
            MapsController controller = new MapsController();
            // Act
            ViewResult result = controller.ViewMaps() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
