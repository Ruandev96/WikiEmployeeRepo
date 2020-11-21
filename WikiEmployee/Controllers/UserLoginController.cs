using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiEmployee.Data;
using WikiEmployee.Models;

namespace WikiEmployee.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly WikiEmployeeContext _context;

        public UserLoginController(WikiEmployeeContext context)
        {
            _context = context;
        }

        // GET: UserLogin
        public async Task<IActionResult> Index()
        {
            var wikiEmployeeContext = _context.AspNetUserLogins.Include(a => a.User);
            return View(await wikiEmployeeContext.ToListAsync());
        }

        // GET: UserLogin/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserLogins = await _context.AspNetUserLogins
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.LoginProvider == id);
            if (aspNetUserLogins == null)
            {
                return NotFound();
            }

            return View(aspNetUserLogins);
        }

        // GET: UserLogin/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: UserLogin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoginProvider,ProviderKey,ProviderDisplayName,UserId")] AspNetUserLogins aspNetUserLogins)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUserLogins);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserLogins.UserId);
            return View(aspNetUserLogins);
        }

        // GET: UserLogin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserLogins = await _context.AspNetUserLogins.FindAsync(id);
            if (aspNetUserLogins == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserLogins.UserId);
            return View(aspNetUserLogins);
        }

        // POST: UserLogin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LoginProvider,ProviderKey,ProviderDisplayName,UserId")] AspNetUserLogins aspNetUserLogins)
        {
            if (id != aspNetUserLogins.LoginProvider)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUserLogins);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserLoginsExists(aspNetUserLogins.LoginProvider))
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
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", aspNetUserLogins.UserId);
            return View(aspNetUserLogins);
        }

        // GET: UserLogin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUserLogins = await _context.AspNetUserLogins
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.LoginProvider == id);
            if (aspNetUserLogins == null)
            {
                return NotFound();
            }

            return View(aspNetUserLogins);
        }

        // POST: UserLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUserLogins = await _context.AspNetUserLogins.FindAsync(id);
            _context.AspNetUserLogins.Remove(aspNetUserLogins);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserLoginsExists(string id)
        {
            return _context.AspNetUserLogins.Any(e => e.LoginProvider == id);
        }
    }
}
