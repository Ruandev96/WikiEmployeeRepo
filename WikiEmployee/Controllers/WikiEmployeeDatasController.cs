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
    public class WikiEmployeeDatasController : Controller
    {
        private readonly WikiEmployeeContext _context;

        public WikiEmployeeDatasController(WikiEmployeeContext context)
        {
            _context = context;
        }

        // GET: WikiEmployeeDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.WikiEmployeeData.ToListAsync());
        }

        // GET: WikiEmployeeDatas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiEmployeeData = await _context.WikiEmployeeData
                .FirstOrDefaultAsync(m => m.EmployeeNumber == id);
            if (wikiEmployeeData == null)
            {
                return NotFound();
            }

            return View(wikiEmployeeData);
        }

        // GET: WikiEmployeeDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WikiEmployeeDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Age,Attrition,BusinessTravel,DailyRate,Department,DistanceFromHome,Education,EducationField,EmployeeCount,EmployeeNumber,EnvironmentSatisfaction,Gender,HourlyRate,JobInvolvement,JobLevel,JobRole,JobSatisfaction,MaritalStatus,MonthlyIncome,MonthlyRate,NumCompaniesWorked,Over18,OverTime,PercentSalaryHike,PerformanceRating,RelationshipSatisfaction,StandardHours,StockOptionLevel,TotalWorkingYears,TrainingTimesLastYear,WorkLifeBalance,YearsAtCompany,YearsInCurrentRole,YearsSinceLastPromotion,YearsWithCurrManager")] WikiEmployeeData wikiEmployeeData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wikiEmployeeData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wikiEmployeeData);
        }

        // GET: WikiEmployeeDatas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiEmployeeData = await _context.WikiEmployeeData.FindAsync(id);
            if (wikiEmployeeData == null)
            {
                return NotFound();
            }
            return View(wikiEmployeeData);
        }

        // POST: WikiEmployeeDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,
                                             [Bind("Age,Attrition,BusinessTravel,DailyRate,Department,DistanceFromHome," +
                                                   "Education,EducationField,EmployeeCount,EmployeeNumber,EnvironmentSatisfaction,Gender" +
                                                   ",HourlyRate,JobInvolvement,JobLevel,JobRole,JobSatisfaction,MaritalStatus,MonthlyIncome" +
                                                   ",MonthlyRate,NumCompaniesWorked,Over18,OverTime,PercentSalaryHike,PerformanceRating,RelationshipSatisfaction" +
                                                   ",StandardHours,StockOptionLevel,TotalWorkingYears,TrainingTimesLastYear,WorkLifeBalance,YearsAtCompany,YearsInCurrentRole" +
                                                   ",YearsSinceLastPromotion,YearsWithCurrManager")]
                                             WikiEmployeeData wikiEmployeeData)
        {
            if (id != wikiEmployeeData.EmployeeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wikiEmployeeData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WikiEmployeeDataExists(wikiEmployeeData.EmployeeNumber))
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
            return View(wikiEmployeeData);
        }

        // GET: WikiEmployeeDatas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wikiEmployeeData = await _context.WikiEmployeeData
                .FirstOrDefaultAsync(m => m.EmployeeNumber == id);
            if (wikiEmployeeData == null)
            {
                return NotFound();
            }

            return View(wikiEmployeeData);
        }

        // POST: WikiEmployeeDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var wikiEmployeeData = await _context.WikiEmployeeData.FindAsync(id);
            _context.WikiEmployeeData.Remove(wikiEmployeeData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WikiEmployeeDataExists(string id)
        {
            return _context.WikiEmployeeData.Any(e => e.EmployeeNumber == id);
        }
    }
}
