using SalaryApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SalaryApplication.Data;
using SalaryApplication.Models;
using System.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace SalaryApplication.Controllers.Tests
{
    [TestFixture()]
    public class AdminsControllerTests
    {
        private ApplicationDBContext _context;

        [Test()]
        public void CreateTest()
        {
            AdminsController controller = new AdminsController(_context);
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }
      
        internal Admin createAdminObject()
        {
            Admin admin = new Admin();
            admin.IsAdmin = true;
            admin.Salary = 25000;
            admin.Role = "dev";
            admin.EmployeeNumber = 20365;
            admin.FirstName = "Wendela";
            admin.LastName = "Bengtsson";
            admin.UserName = "WenBen";
            admin.PassWord = "Wendela89";

            return admin;
        }


        [Test()] //Integration test
        public void CreateTestShouldAddAdmin()
        {
            _context = new ApplicationDBContext();
            AdminsController controller = new AdminsController(_context);
            var admin = createAdminObject();
            System.Threading.Tasks.Task<IActionResult> createAdmin = controller.Create(admin);
            Thread.Sleep(2000);
            var addedUser = controller.AdminExists(admin.Id);
            Assert.IsTrue(addedUser);
        }

        [Test()] //Integration test
        public void DeleteTestShouldDeleteAdmin()
        {
            _context = new ApplicationDBContext();
            AdminsController controller = new AdminsController(_context);
            var admin = createAdminObject();
            System.Threading.Tasks.Task<IActionResult> deleteAdmin = controller.DeleteConfirmed(admin.Id);
            Thread.Sleep(2000);
            var addedUser = controller.AdminExists(admin.Id);
            Assert.IsFalse(addedUser);
        }

    }
}