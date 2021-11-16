using SalaryApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SalaryApplication.Data;
using SalaryApplication.Models;
using System.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Microsoft.AspNetCore.Http;

namespace SalaryApplication.Controllers.Tests
{
    /// <summary>
    /// Tests methods in AdminsController class
    /// </summary>
    [TestFixture()]
    public class AdminsControllerTests
    {
        private ApplicationDBContext _context;

        /// <summary>
        /// Makes sure admins are routed admin-view when clicking admin profile
        /// </summary>
        [Test()]
        public void IndexTestForAdmin()
        {
            _context = new ApplicationDBContext();
            AdminsController controller = new AdminsController(_context);
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            controller.TempData = tempData;
            var user = _context.Admins.FirstOrDefault(u => u.IsAdmin == true);
            var result = controller.Index(user.Id).Result as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        /// <summary>
        /// Makes sure non admins are routed no non admin-view when trying to access admin view
        /// </summary>
        [Test()]
        public void IndexTestForNonAdmin()
        {
            _context = new ApplicationDBContext();
            AdminsController controller = new AdminsController(_context);
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            controller.TempData = tempData;
            var user = _context.Admins.FirstOrDefault(u => u.IsAdmin == false);
            var result = controller.Index(user.Id).Result as ViewResult;
            Assert.AreEqual("NotAdmin", result.ViewName);
        }

        /// <summary>
        /// Makes sure create-view is presented when pressing create button
        /// </summary>
        [Test()]
        public void CreateTest()
        {
            AdminsController controller = new AdminsController(_context);
            var result = controller.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        /// <summary>
        /// Creates admin object to be used in internal test methods. 
        /// </summary>
        /// <returns>Admin object</returns>
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

        /// <summary>
        /// **Integration test**
        /// Makes sure adding admin works, as well as finding the admin when it's created. 
        /// </summary>
        [Test()] 
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

        /// <summary>
        /// **Integration test**
        /// Makes sure deletion of admin works, as well as searching for admin (and not finding it) when deletion is carried out.
        /// </summary>
        [Test()]
        public void DeleteTestShouldDeleteAdmin()
        {
            _context = new ApplicationDBContext();
            AdminsController controller = new AdminsController(_context);
            var admin = createAdminObject();
            admin = _context.Admins.FirstOrDefault(e => e.UserName == admin.UserName && e.PassWord == admin.PassWord);
            System.Console.WriteLine(admin.Id);
            System.Threading.Tasks.Task<IActionResult> deleteAdmin = controller.DeleteUser(admin.UserName, admin.PassWord, admin.Id);
            Thread.Sleep(2000);
            var addedUser = controller.AdminExists(admin.Id);
            Assert.IsFalse(addedUser);
        }

    }
}