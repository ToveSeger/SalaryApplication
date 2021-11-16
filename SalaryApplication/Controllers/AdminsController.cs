using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalaryApplication.Data;
using SalaryApplication.Models;

namespace SalaryApplication.Controllers
{
    /// <summary>
    /// Controls admin functionality
    /// </summary>
    public class AdminsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AdminsController(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the admin profile view if user is logged in as admin. 
        /// If user is logged in as non admin, NotAdmin is presented.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The correct view depending on above mentioned parameters </returns>
        public async Task<IActionResult> Index(int? id)
        {
            if (TempData.ContainsKey("user"))
            {
                id = int.Parse(TempData["user"].ToString());
                TempData["user"] = id;
            }
            var user = _context.Admins.FirstOrDefault(u => u.Id==id);
            if (user != null && user.IsAdmin)
            {
                return View("Index", await _context.Admins.ToListAsync());
            }
            else return View("NotAdmin");
        }


        /// <summary>
        /// Gets the details of the specified id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View with details of specified id if user exists, otherwhise NotFound view</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        /// <summary>
        /// Gets creation view
        /// </summary>
        /// <returns>Creation view</returns>
        public IActionResult Create()
        {
            return View("Create");
        }

        /// <summary>
        /// Creates a new user/admin with the specified bind parameters if modelstate is valid.  
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>Index view of admin</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsAdmin,Salary,Role,Id,EmployeeNumber,FirstName,LastName,UserName,PassWord")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(admin.FirstName);
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        /// <summary>
        /// Presents edit view.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit view as long as user is admin, otherwhise NotFound view</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        /// <summary>
        /// Makes it possible to edit a specified user as long as id is found. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="admin"></param>
        /// <returns>Admin index view if action is carried out, otherwhise NotFound view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IsAdmin,Salary,Role,Id,EmployeeNumber,FirstName,LastName,UserName,PassWord")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        /// <summary>
        /// Presents deletion view as long as user is admin.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletion view as long as user is admin, otherwhise NotFound view</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }


        /// <summary>
        /// Makes it possible to delete it a specified admin user as long as id is found. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Admin index view if action is carried out, otherwhise NotFound view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Checks whether an admin exists or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>false if admin doesn't exist, true if it exists</returns>
        public bool AdminExists(int id)
        {
            return  _context.Admins.Any(e => e.Id == id);
        }

        /// <summary>
        /// Controls that the username & password is correct for the specified user.If so: delete user.
        /// </summary>
        /// <param name = "username" ></ param >
        /// < param name= "password" ></ param >
        /// < param name= "id" ></ param >
        /// < returns > Index view of admin</returns>
        public async Task<IActionResult> DeleteUser(string username, string password, int? id)
        {
            var user = _context.Admins.FirstOrDefault(e => e.Id == id);

            if (username == user.UserName && password == user.PassWord)
            {
                _context.Admins.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
            }
        }
}
