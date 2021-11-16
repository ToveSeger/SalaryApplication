using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalaryApplication.Data;
using SalaryApplication.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public UsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: user view if logged in.
        /// </summary>
        /// <returns>view if logged in, 404 page if not.</returns>
        public IActionResult Index()
        {


            if (TempData != null && TempData.ContainsKey("user"))
            {
                int id = int.Parse(TempData["user"].ToString());
                var admin = _context.Admins.FirstOrDefault(e => e.Id == id);
                if (admin != null)
                {
                    return View("AdminProfile", admin);
                }
                else
                {
                    var user = _context.Users.FirstOrDefault(e => e.Id == id);
                    return View(user);
                }
            }
            
            else
            {
                return NotFound();
            }

            
        }

        /// <summary>
        /// Delete-check if input of password and username is correct. if yes, delete user from DB.
        /// </summary>
        /// <param name="username">user input.</param>
        /// <param name="password">user input.</param>
        /// <param name="id">primarykey in DB.</param>
        /// <returns>Redirects to Index view. </returns>
        public async Task<IActionResult> ConfirmDelete(string username, string password, int? id)
        {
            var user = _context.Users.FirstOrDefault(e => e.Id == id);

            if (username == user?.UserName && password == user?.PassWord)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Get details view for the user that is logged in.
        /// </summary>
        /// <param name="id">primarykey of user in DB.</param>
        /// <returns>Details view if exists otherwise 404.</returns>
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

        /// <summary>
        /// Get Delete view for the user that is logged in. 
        /// </summary>
        /// <param name="id">primarykey of user in DB.</param>
        /// <returns>The deleteview if exsiting otherwise 404.</returns>
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
