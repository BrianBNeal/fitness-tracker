using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Data;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace FitnessTracker.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExercisesController(ApplicationDbContext context,
                                   UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.Location)
                .Include(e => e.ExertionLevel)
                .Include(e => e.EnjoymentLevel)
                .Where(e => e.User == user);

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
                .Include(e => e.ExerciseType)
                .Include(e => e.Location)
                .Include(e => e.ExertionLevel)
                .Include(e => e.EnjoymentLevel)
                .FirstOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            ViewData["ExertionLevelId"] = new SelectList(_context.ExertionLevels, "ExertionLevelId", "SelectListDescription");
            ViewData["EnjoymentLevelId"] = new SelectList(_context.EnjoymentLevels, "EnjoymentLevelId", "SelectListDescription");
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "ExerciseTypeId", "Type");
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId,UserId,LocationId,ExerciseTypeId,Duration,EnjoymentLevelId,ExertionLevelId,Comments")] Exercise exercise)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("DateLogged");
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                exercise.UserId = user.Id;
                exercise.DateLogged = DateTime.UtcNow;
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["ExertionLevelId"] = new SelectList(_context.ExertionLevels, "ExertionLevelId", "SelectListDescription");
            ViewData["EnjoymentLevelId"] = new SelectList(_context.EnjoymentLevels, "EnjoymentLevelId", "SelectListDescription");
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "ExerciseTypeId", "Type", exercise.ExerciseTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", exercise.LocationId);
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
            ViewData["ExertionLevelId"] = new SelectList(_context.ExertionLevels, "ExertionLevelId", "SelectListDescription");
            ViewData["EnjoymentLevelId"] = new SelectList(_context.EnjoymentLevels, "EnjoymentLevelId", "SelectListDescription");
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "ExerciseTypeId", "Type", exercise.ExerciseTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", exercise.LocationId);
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,UserId,LocationId,ExerciseTypeId,Duration,EnjoymentLevelId,ExertionLevelId,Comments")] Exercise exercise)
        {
            if (id != exercise.ExerciseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ExerciseId))
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
            ViewData["ExertionLevelId"] = new SelectList(_context.ExertionLevels, "ExertionLevelId", "SelectListDescription");
            ViewData["EnjoymentLevelId"] = new SelectList(_context.EnjoymentLevels, "EnjoymentLevelId", "SelectListDescription");
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "ExerciseTypeId", "Type", exercise.ExerciseTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", exercise.LocationId);
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
                .Include(e => e.ExerciseType)
                .Include(e => e.Location)
                .Include(e => e.ExertionLevel)
                .Include(e => e.EnjoymentLevel)
                .FirstOrDefaultAsync(m => m.ExerciseId == id);
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
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}
