using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SalaryApplication.Controllers;
using SalaryApplication.Data;

namespace SalaryApplication.Controllers.Tests
{
    [TestFixture()]
    public class HomeControllerTests
    {
        private static ILogger<HomeController> _logger;

        readonly HomeController controller = new HomeController(_logger);

        /// <summary>
        /// Tests that Index view is returned as expected
        /// </summary>
        [Test()]
        public void IndexTest()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        /// <summary>
        /// Tests that Privacy view is returned as expected
        /// </summary>
        [Test()]
        public void PrivacyTest()
        {
            var result = controller.Privacy() as ViewResult;
            Assert.AreEqual("Privacy", result.ViewName);
        }
    }
}