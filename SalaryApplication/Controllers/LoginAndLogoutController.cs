namespace SalaryApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SalaryApplication.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LoginAndLogoutController : Controller
    {
        private readonly ApplicationDBContext _context;
        public LoginAndLogoutController(ApplicationDBContext context)
        {
            _context = context;
        }

       /// <summary>
       /// GET login view. 
       /// </summary>
       /// <returns>Login view.</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Login controller. Checks if user exists through username and password.
        /// sets the tempData to user id to be able to view logged in navbar.
        /// </summary>
        /// <param name="password">input user</param>
        /// <param name="username">input user</param>
        /// <returns>succedview or failview.</returns>
        public ActionResult LoginVerification(string password, string username)
        {
            
            var user = _context.Accounts.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
            if (user != null)
            {
                TempData["user"] = user.Id; 
                return View("LoginSucced", user);
            }

            return View("LoginFailed");
        }

        public ActionResult Logut()
        {
            TempData.Clear();
            return View("Logout");
        }

    }
}

