using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaryApplication.Data;
using SalaryApplication.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SalarySystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public UsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            //var id = int.Parse(TempData["user"].ToString());
            //var user = _context.Users.FirstOrDefault(e => e.Id == id);

            var user =  _context.Users.First();
            
            return View(user);
        }
        public async Task<IActionResult> DeleteUser(string username, string password, int? id)
        {
            var user = _context.Users.FirstOrDefault(e => e.Id == id);

            if (username == user.UserName && password == user.PassWord)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

 
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

    }
}
