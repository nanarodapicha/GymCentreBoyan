using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPGymCentre.Data;
using ASPGymCentre.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ASPGymCentre.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<Client> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Exercises);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Exercises)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            var clients = _context.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Text = (u.Name ?? "") + " " + (u.FamilyName ?? "")
                })
                .ToList();

            var exercises = _context.Exercises
                .Select(e => new
                {
                    Id = e.Id,
                    Text = e.Day + " " + e.StartTime + " - " + e.EndTime
                })
                .ToList();

            ViewBag.ClientId = new SelectList(clients, "Id", "Text");
            ViewBag.ExerciseId = new SelectList(exercises, "Id", "Text");

            return View();
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId")] Reservation reservation)
        {
            reservation.RegisteredDate = DateTime.Now;
            reservation.ClientId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(_context.Users.ToList(), "Id", "FirstName", reservation.ClientId);
            ViewData["ExerciseId"] = new SelectList(_context.Exercises.ToList(), "Id", "Day", reservation.ExerciseId);

            return View(reservation);
        }


        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var clients = _context.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Text = (u.Name ?? "") + " " + (u.FamilyName ?? "")
                })
                .ToList();

            var exercises = _context.Exercises
                .Select(e => new
                {
                    Id = e.Id,
                    Text = e.Day + " " + e.StartTime + " - " + e.EndTime
                })
                .ToList();

            ViewBag.ClientId = new SelectList(clients, "Id", "Text", reservation.ClientId);
            ViewBag.ExerciseId = new SelectList(exercises, "Id", "Text", reservation.ExerciseId);

            return View(reservation);
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ExerciseId,")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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

            var clients = _context.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Text = (u.Name ?? "") + " " + (u.FamilyName ?? "")
                })
                .ToList();

            var exercises = _context.Exercises
                .Select(e => new
                {
                    Id = e.Id,
                    Text = e.Day + " " + e.StartTime + " - " + e.EndTime
                })
                .ToList();

            ViewBag.ClientId = new SelectList(clients, "Id", "Text", reservation.ClientId);
            ViewBag.ExerciseId = new SelectList(exercises, "Id", "Text", reservation.ExerciseId);

            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Exercises)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}