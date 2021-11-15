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
    public class AdminsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AdminsController(ApplicationDBContext context)
        {
            _context = context;
        }


        // GET: Admins
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
                return View(await _context.Admins.ToListAsync());
            }
            else return View("NotAdmin");
        }


        // GET: Admins/Details/5
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

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Admins/Edit/5
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

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Admins/Delete/5
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

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool AdminExists(int id)
        {
            return  _context.Admins.Any(e => e.Id == id);
        }

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
