using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SalaryApplication.Controllers;

namespace SalaryApplication.Controllers.Tests
{
    [TestFixture()]
    public class HomeControllerTests
    {
        HomeController controller = new();
        [Test()]
        public void IndexTest()
        {
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}