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

        [Test()]
        public void IndexTest()
        {
            
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}