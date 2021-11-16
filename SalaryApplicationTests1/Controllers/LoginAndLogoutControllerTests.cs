using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using SalaryApplication.Controllers;
using SalaryApplication.Data;
using System.Linq;

namespace SalaryApplication.Controllers.Tests
{
    [TestFixture()]
    public class LoginAndLogoutControllerTests
    {
        private static readonly ApplicationDBContext _context = new ApplicationDBContext();
        private static readonly LoginAndLogoutController controller = new(_context);

        /// <summary>
        /// Testning the Login function. If succeeded, user id should be added as tempdata. 
        /// </summary>
        [Test()]
        public void LoginVerificationTest()
        {
            //it´s necessary to create TempData for not getting null-error when calling the method.
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            controller.TempData = tempData;

            var user = _context.Accounts.FirstOrDefault();
            var result = controller.LoginVerification(user.PassWord, user.UserName) as ViewResult;
            Assert.AreEqual("LoginSucced", result.ViewName);
            Assert.IsNotEmpty(controller.TempData);


        }

        /// <summary>
        /// If login fails, user should get a loginfailed view. 
        /// </summary>
        [Test()]
        public void LoginFail()
        {
            var result = controller.LoginVerification("", "") as ViewResult;
            Assert.AreEqual("LoginFailed", result.ViewName);
           
        }

        /// <summary>
        /// When logging out, tempdata is cleared and accordingly "empty". TempData have to be empty to 
        /// retrive the logged out view in navbar. Please see the if-statement in Views/_Layoyt.cshtml
        /// </summary>
        [Test()]
        public void Logout()
        {
            //it´s necessary to create TempData for not getting null-error when calling the method. 
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            controller.TempData = tempData;

            var result = controller.Logut() as ViewResult;
            Assert.AreEqual("Logout", result.ViewName);
            Assert.IsEmpty(controller.TempData);

        }

    }
}