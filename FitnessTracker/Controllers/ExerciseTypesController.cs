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
    public class ExerciseTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ExerciseTypesController(ApplicationDbContext context,
                                               UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        //// GET: ExerciseTypes
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.ExerciseTypes.Include(e => e.User);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //// GET: ExerciseTypes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var exerciseType = await _context.ExerciseTypes
        //        .Include(e => e.User)
        //        .FirstOrDefaultAsync(m => m.ExerciseTypeId == id);
        //    if (exerciseType == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(exerciseType);
        //}

        // GET: ExerciseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExerciseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseTypeId,Type,UserId")] ExerciseType exerciseType)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                exerciseType.UserId = user.Id;
                _context.Add(exerciseType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Settings","Home");
            }
            return View(exerciseType);
        }

        // GET: ExerciseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseType = await _context.ExerciseTypes.FindAsync(id);
            if (exerciseType == null)
            {
                return NotFound();
            }
            return View(exerciseType);
        }

        // POST: ExerciseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseTypeId,Type,UserId")] ExerciseType exerciseType)
        {
            if (id != exerciseType.ExerciseTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseTypeExists(exerciseType.ExerciseTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Settings","Home");
            }
            return View(exerciseType);
        }

        // GET: ExerciseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseType = await _context.ExerciseTypes
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ExerciseTypeId == id);
            if (exerciseType == null)
            {
                return NotFound();
            }

            return View(exerciseType);
        }

        // POST: ExerciseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exerciseType = await _context.ExerciseTypes.FindAsync(id);
            var exercises = await _context.Exercises.Where(e => e.ExerciseTypeId == id).ToListAsync();

            foreach (var item in exercises)
            {
                _context.Exercises.Remove(item);
            }

            _context.ExerciseTypes.Remove(exerciseType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Settings", "Home");
        }

        private bool ExerciseTypeExists(int id)
        {
            return _context.ExerciseTypes.Any(e => e.ExerciseTypeId == id);
        }
    }
}
