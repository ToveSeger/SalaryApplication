using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using SalaryApplication.Controllers;
using SalaryApplication.Data;
using SalaryApplication.Models;
using System.Linq;

namespace SalaryApplication.Controllers.Tests
{
    [TestFixture()]
    public class UsersControllerTests
    {
        private static readonly ApplicationDBContext _context = new ApplicationDBContext();
        private static readonly UsersController controller = new UsersController(_context);

        /// <summary>
        /// Testing the userpage, for user with key 1 in DB. 
        /// </summary>
        [Test()]
        public void UserOneFound()
        {
            //it´s necessary to create TempData for not getting null-error when calling the method. 
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());

            controller.TempData = tempData;
            controller.TempData["user"] = 1;
           
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        /// <summary>
        /// if user dosen´t exist, view isn´t returned.
        /// </summary>
        [Test()]
        public void UserNotFound()
        {
            var result = controller.Index() as ViewResult;
            Assert.IsNull(result);
        }
        /// <summary>
        /// Integration test where a user first creates(admin function) and then are deleted by the user it self. 
        /// </summary>
        [Test()]
        public void DeleteYourSelf()
        {
            AdminsController adminC = new(_context);
            var user = createAdminObject();
            System.Threading.Tasks.Task<IActionResult> createAdmin = adminC.Create(user);
            user = _context.Admins.Where(e => e.UserName == user.UserName && e.PassWord == user.PassWord).FirstOrDefault();
            Assert.AreEqual(user.FirstName, "Mikael");

            var result = controller.ConfirmDelete(username: user.UserName, password: user.PassWord, user.Id);
            user = _context.Admins.Where(e => e.UserName == user.UserName && e.PassWord == user.PassWord).FirstOrDefault(); 
            Assert.IsNull(user);
        }

        // help-method to create an object. 
        internal Admin createAdminObject()
        {
            Admin admin = new();
            admin.Salary = 25000;
            admin.Role = "dev";
            admin.EmployeeNumber = 20362;
            admin.FirstName = "Mikael";
            admin.LastName = "Persbrant";
            admin.UserName = "MikPer";
            admin.PassWord = "Mikael89";

            return admin;
        }
    }
}