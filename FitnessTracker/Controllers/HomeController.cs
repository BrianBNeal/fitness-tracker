using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FitnessTracker.Models;
using FitnessTracker.Data;
using Microsoft.AspNetCore.Identity;
using FitnessTracker.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context,
                                   UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var userExercises = _context.Exercises.Where(e => e.User == user);
            //exercises by this user during current week
            var thisWeeksExercises = userExercises.Where(e => e.IsThisWeeksActivity());
            //total minutes of this week's activity
            int currentWeeklyTotal = 0;
            if (thisWeeksExercises != null)
            {
                currentWeeklyTotal = thisWeeksExercises.Sum(e => e.Duration);
            }

            var userGoals = _context.Goals.Where(g => g.UserId == user.Id);

            HomeViewModel model = new HomeViewModel
            {
                User = user,
                CurrentWeeklyTotal = currentWeeklyTotal
            };

            //most recent Goal for this user
            if (userGoals != null)
            {
                model.MostRecentGoal = _context.Goals.Where(g => g.UserId == user.Id).OrderByDescending(g => g.StartDate).FirstOrDefault();
            }

            //exercises during MostRecentGoal
            if (model.MostRecentGoal != null)
            {
                model.GoalExercises = await userExercises
                    .Include(e => e.ExerciseType)
                    .Include(e => e.Location)
                    .Include(e => e.EnjoymentLevel)
                    .Include(e => e.ExertionLevel)
                    .OrderByDescending(e => e.DateLogged)
                    .Where(e => e.DateLogged >= model.MostRecentGoal.StartDate && e.DateLogged <= model.MostRecentGoal.EndDate)
                    .ToListAsync();
            }

            model.User.Exercises = userExercises.ToList();

            model.RecentExercises = await userExercises
                .OrderByDescending(e => e.DateLogged)
                .Take(3)
                .Include(e => e.ExerciseType)
                    .Include(e => e.Location)
                    .Include(e => e.EnjoymentLevel)
                    .Include(e => e.ExertionLevel)
                .ToListAsync();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Settings()
        {
            var user = await GetCurrentUserAsync();
            var goals = await _context.Goals.OrderByDescending(g => g.StartDate).Where(g => g.User == user).ToListAsync();
            var exercises = await _context.Exercises.Where(e => e.User == user).ToListAsync();
            var exerciseTypes = await _context.ExerciseTypes.Where(et => et.User == user).ToListAsync();
            var locations = await _context.Locations.Where(l => l.User == user).ToListAsync();

            user.Goals = goals;
            user.Exercises = exercises;
            user.ExerciseTypes = exerciseTypes;
            user.Locations = locations;

            Goal mostRecentGoal = goals.FirstOrDefault();

            SettingsViewModel model = new SettingsViewModel
            {
                User = user,
                MostRecentGoal = mostRecentGoal
            };

            if (model.MostRecentGoal != null)
            {
                model.GoalExercises = await _context.Exercises
                    .Where(e => e.User == user)
                    .Include(e => e.ExerciseType)
                    .Include(e => e.Location)
                    .Include(e => e.EnjoymentLevel)
                    .Include(e => e.ExertionLevel)
                    .OrderByDescending(e => e.DateLogged)
                    .Where(e => e.DateLogged >= model.MostRecentGoal.StartDate && e.DateLogged <= model.MostRecentGoal.EndDate)
                    .ToListAsync();
            }

            return View(model);
        }
    }
}