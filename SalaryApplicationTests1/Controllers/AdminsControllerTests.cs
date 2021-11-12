using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SalaryApplication.Controllers;
using SalaryApplication.Data;

namespace SalarySystem.Controllers.Tests
{
    [TestFixture()]
    public class AdminsControllerTests
    {
        private static readonly ApplicationDBContext _context;

        AdminsController controller = new AdminsController(_context);

        [Test()]
        public void CreateTest()
        {
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }
    }
}