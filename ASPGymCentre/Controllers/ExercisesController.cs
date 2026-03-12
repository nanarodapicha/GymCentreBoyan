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
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Exercises
                .Include(e => e.Instructors)
                .Include(e => e.Plans);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.Instructors)
                .Include(e => e.Plans)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Name");
            ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name");
            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlanId,Day,StartTime,EndTime,InstructorId")] Exercise exercise)
        {
            exercise.RegisteredDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Name", exercise.InstructorId);
            ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name", exercise.PlanId);
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Name", exercise.InstructorId);
            ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name", exercise.PlanId);
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlanId,Day,StartTime,EndTime,InstructorId")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            var existingExercise = await _context.Exercises.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (existingExercise == null)
            {
                return NotFound();
            }

            exercise.RegisteredDate = existingExercise.RegisteredDate;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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

            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Name", exercise.InstructorId);
            ViewData["PlanId"] = new SelectList(_context.Plans, "Id", "Name", exercise.PlanId);
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.Instructors)
                .Include(e => e.Plans)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}