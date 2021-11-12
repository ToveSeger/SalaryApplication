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

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginVerification(string password, string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
            if (user != null)
            {
                TempData["user"] = user.Id;
                TempData.Keep();

                return View("LoginSucced", user);
            }

            var user1 = _context.Admins.FirstOrDefault(u => u.UserName == username && u.PassWord == password);
            if (user1 != null)
            {
                TempData["user"] = user1.Id;
                TempData.Keep();

                return View("LoginSucced", user1);
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

