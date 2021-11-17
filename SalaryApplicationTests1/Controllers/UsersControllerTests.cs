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
            var userDB = _context.Accounts.Where(e => e.UserName == user.UserName && e.PassWord == user.PassWord).FirstOrDefault();
            Assert.AreEqual(userDB.FirstName, "Mikael");

            controller.ConfirmDelete(username: userDB.UserName, password: userDB.PassWord, userDB.Id);
            userDB = _context.Accounts.Where(e => e.UserName == user.UserName && e.PassWord == user.PassWord).FirstOrDefault(); 
            Assert.IsNull(userDB);
        }

        // help-method to create an object. 
        internal User createAdminObject()
        {
           User user = new();
            user.Salary = 25000;
            user.Role = "dev";
            user.EmployeeNumber = 20362;
            user.FirstName = "Mikael";
            user.LastName = "Persbrant";
            user.UserName = "MikPer";
            user.PassWord = "Mikael89";

            return user;
        }
    }
}