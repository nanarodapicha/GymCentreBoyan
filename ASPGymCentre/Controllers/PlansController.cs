using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPGymCentre.Data;
using ASPGymCentre.Models;

namespace ASPGymCentre.Controllers
{
    public class PlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Plans
                .Include(p => p.PlansCategories);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Plans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plans
                .Include(p => p.PlansCategories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // GET: Plans/Create
        public IActionResult Create()
        {
            ViewData["PlanCategoryID"] = new SelectList(_context.PlanCategory, "Id", "Name");
            return View();
        }

        // POST: Plans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PlanCategoryID,Description,Image,PriceSingleWorkout")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                plan.RegisteredDate = DateTime.Now;

                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PlanCategoryID"] = new SelectList(_context.PlanCategory, "Id", "Name", plan.PlanCategoryID);
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            ViewData["PlanCategoryID"] = new SelectList(_context.PlanCategory, "Id", "Name", plan.PlanCategoryID);
            return View(plan);
        }

        // POST: Plans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PlanCategoryID,Description,Image,PriceSingleWorkout")] Plan plan)
        {
            if (id != plan.Id)
            {
                return NotFound();
            }

            var existingPlan = await _context.Plans
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingPlan == null)
            {
                return NotFound();
            }

            plan.RegisteredDate = existingPlan.RegisteredDate;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.Id))
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

            ViewData["PlanCategoryID"] = new SelectList(_context.PlanCategory, "Id", "Name", plan.PlanCategoryID);
            return View(plan);
        }

        // GET: Plans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plans
                .Include(p => p.PlansCategories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanExists(int id)
        {
            return _context.Plans.Any(e => e.Id == id);
        }
    }
}