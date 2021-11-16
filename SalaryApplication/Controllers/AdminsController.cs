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
            var user = _context.Accounts.FirstOrDefault(u => u.Id==id);
            if (user != null && user.IsAdmin)
            {
                return View("Index", await _context.Accounts.ToListAsync());
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

            var admin = await _context.Accounts
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
        public async Task<IActionResult> Create([Bind("IsAdmin,Salary,Role,Id,EmployeeNumber,FirstName,LastName,UserName,PassWord")] User user)
        {
            if (ModelState.IsValid)
            { 
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Accounts.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IsAdmin,Salary,Role,Id,EmployeeNumber,FirstName,LastName,UserName,PassWord")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(user.Id))
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
            return View(user);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool AdminExists(int id)
        {
            return  _context.Accounts.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteUser(string username, string password, int? id)
        {
            var user = _context.Accounts.FirstOrDefault(e => e.Id == id);

            if (username == user.UserName && password == user.PassWord)
            {
                _context.Accounts.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
