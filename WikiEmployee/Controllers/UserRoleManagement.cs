using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiEmployee.Data;
using WikiEmployee.Models;

namespace WikiEmployee.Controllers
{
    [Authorize]
    public class UserRoleManagement : Controller
    {
        private readonly WikiEmployeeContext _context;

        public UserRoleManagement(WikiEmployeeContext context)
        {
            _context = context;
        }

        // GET: UserRole
        public async Task<IActionResult> Index()
        {
            var wikiEmployeeContext = _context.AspNetRoleClaims.Include(a => a.Role);
            return View(await wikiEmployeeContext.ToListAsync());
        }

        // GET: UserRole/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoleClaims = await _context.AspNetRoleClaims
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRoleClaims == null)
            {
                return NotFound();
            }

            return View(aspNetRoleClaims);
        }

        // GET: UserRole/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id");
            return View();
        }

        // POST: UserRole/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleId,ClaimType,ClaimValue")] AspNetRoleClaims aspNetRoleClaims)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetRoleClaims);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetRoleClaims.RoleId);
            return View(aspNetRoleClaims);
        }

    

        // GET: UserRole/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoleClaims = await _context.AspNetRoleClaims.FindAsync(id);
            if (aspNetRoleClaims == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetRoleClaims.RoleId);
            return View(aspNetRoleClaims);
        }

        // POST: UserRole/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,ClaimType,ClaimValue")] AspNetRoleClaims aspNetRoleClaims)
        {
            if (id != aspNetRoleClaims.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetRoleClaims);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetRoleClaimsExists(aspNetRoleClaims.Id))
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
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Id", aspNetRoleClaims.RoleId);
            return View(aspNetRoleClaims);
        }

        // GET: UserRole/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRoleClaims = await _context.AspNetRoleClaims
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRoleClaims == null)
            {
                return NotFound();
            }

            return View(aspNetRoleClaims);
        }

        // POST: UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aspNetRoleClaims = await _context.AspNetRoleClaims.FindAsync(id);
            _context.AspNetRoleClaims.Remove(aspNetRoleClaims);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetRoleClaimsExists(int id)
        {
            return _context.AspNetRoleClaims.Any(e => e.Id == id);
        }
    }
}
