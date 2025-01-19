using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Widely.Models;

namespace Widely.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly DB_ATMContext _context;
       
        public UserInfoController(DB_ATMContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            if (Global_Variables.LoginID == 3008)
            {
                return View(await _context.tbl_User.ToListAsync());
            }
            else
            {
                return View("AccessDenied");
            }

        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tbl_User = await _context.tbl_User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (Tbl_User == null)
            {
                return NotFound();
            }

            return View(Tbl_User);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Phone,DOB,LoginPassword,Balance")] tbl_User Tbl_User)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Tbl_User);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Tbl_User);
        }

        // GET: Employee/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tbl_User = await _context.tbl_User.FindAsync(id);
            if (Tbl_User == null)
            {
                return NotFound();
            }
            return View(Tbl_User);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, tbl_User Tbl_User)
        {
            if (id != Tbl_User.UserID)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Tbl_User);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tbl_UserExists(Tbl_User.UserID))
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
            return View(Tbl_User);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tbl_User = await _context.tbl_User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (Tbl_User == null)
            {
                return NotFound();
            }

            return View(Tbl_User);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Tbl_User = await _context.tbl_User.FindAsync(id);
            _context.tbl_User.Remove(Tbl_User);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tbl_UserExists(int id)
        {
            return _context.tbl_User.Any(e => e.UserID == id);
        }
    }
}

