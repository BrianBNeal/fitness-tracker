﻿using System;
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

            var exercises = _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.Location)
                .Include(e => e.ExertionLevel)
                .Include(e => e.EnjoymentLevel)
                .Where(e => e.UserId == user.Id)
                .OrderByDescending(e => e.DateLogged);

            return View(await exercises.ToListAsync());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();

            var exercise = await _context.Exercises
                .Include(e => e.ExerciseType)
                .Include(e => e.Location)
                .Include(e => e.ExertionLevel)
                .Include(e => e.EnjoymentLevel)
                .Where(e => e.UserId == user.Id && e.ExerciseId == id)
                .FirstOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();

            ViewData["ExertionLevelId"] = new SelectList(_context.ExertionLevels.OrderByDescending(l => l.SelectListDescription), "ExertionLevelId", "SelectListDescription");
            ViewData["EnjoymentLevelId"] = new SelectList(_context.EnjoymentLevels.OrderByDescending(l => l.SelectListDescription), "EnjoymentLevelId", "SelectListDescription");
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes.Where(e => e.UserId == user.Id), "ExerciseTypeId", "Type");
            ViewData["LocationId"] = new SelectList(_context.Locations.Where(e => e.UserId == user.Id), "LocationId", "Name");
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
            var user = await GetCurrentUserAsync();
            if (ModelState.IsValid)
            {
                exercise.UserId = user.Id;
                exercise.DateLogged = DateTime.Now;
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["ExertionLevelId"] = new SelectList(_context.ExertionLevels, "ExertionLevelId", "SelectListDescription", exercise.ExertionLevelId);
            ViewData["EnjoymentLevelId"] = new SelectList(_context.EnjoymentLevels, "EnjoymentLevelId", "SelectListDescription", exercise.EnjoymentLevelId);
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes.Where(e => e.UserId == user.Id), "ExerciseTypeId", "Type", exercise.ExerciseTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations.Where(e => e.UserId == user.Id), "LocationId", "Name", exercise.LocationId);
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
            var user = await GetCurrentUserAsync();
            ViewData["ExertionLevelId"] = new SelectList(_context.ExertionLevels, "ExertionLevelId", "SelectListDescription");
            ViewData["EnjoymentLevelId"] = new SelectList(_context.EnjoymentLevels, "EnjoymentLevelId", "SelectListDescription");
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes.Where(e => e.UserId == user.Id), "ExerciseTypeId", "Type", exercise.ExerciseTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations.Where(e => e.UserId == user.Id), "LocationId", "Name", exercise.LocationId);
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,UserId,LocationId,ExerciseTypeId,Duration,EnjoymentLevelId,ExertionLevelId,Comments, DateLogged")] Exercise exercise)
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
            var user = await GetCurrentUserAsync();
            ViewData["ExertionLevelId"] = new SelectList(_context.ExertionLevels, "ExertionLevelId", "SelectListDescription", exercise.ExertionLevelId);
            ViewData["EnjoymentLevelId"] = new SelectList(_context.EnjoymentLevels, "EnjoymentLevelId", "SelectListDescription", exercise.EnjoymentLevelId);
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes.Where(e => e.UserId == user.Id), "ExerciseTypeId", "Type", exercise.ExerciseTypeId);
            ViewData["LocationId"] = new SelectList(_context.Locations.Where(e => e.UserId == user.Id), "LocationId", "Name", exercise.LocationId);
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
