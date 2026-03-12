using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPGymCentre.Data;
using ASPGymCentre.Models;

namespace ASPGymCentre.Controllers
{
    public class PlanCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlanCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlanCategory.ToListAsync());
        }

        // GET: PlanCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planCategory = await _context.PlanCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planCategory == null)
            {
                return NotFound();
            }

            return View(planCategory);
        }

        // GET: PlanCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] PlanCategory planCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planCategory);
        }

        // GET: PlanCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planCategory = await _context.PlanCategory.FindAsync(id);
            if (planCategory == null)
            {
                return NotFound();
            }
            return View(planCategory);
        }

        // POST: PlanCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] PlanCategory planCategory)
        {
            if (id != planCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanCategoryExists(planCategory.Id))
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
            return View(planCategory);
        }

        // GET: PlanCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planCategory = await _context.PlanCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planCategory == null)
            {
                return NotFound();
            }

            return View(planCategory);
        }

        // POST: PlanCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planCategory = await _context.PlanCategory.FindAsync(id);
            if (planCategory != null)
            {
                _context.PlanCategory.Remove(planCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanCategoryExists(int id)
        {
            return _context.PlanCategory.Any(e => e.Id == id);
        }
    }
}
