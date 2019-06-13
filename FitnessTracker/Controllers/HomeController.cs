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

            //total minutes of recent activity
            var currentWeeklyTotal = thisWeeksExercises.Select(e => e.Duration).Sum();

            //current Goal for this user
            var goal = _context.Goals.Where(g => g.User == user && g.EndDate >= DateTime.Today).FirstOrDefault();

            //exercise progress toward current Goal
            var progress = _context.Exercises.Where(e => e.User == user && e.DateLogged >= goal.StartDate).Select(e => e.Duration).Sum();

            HomeViewModel model = new HomeViewModel
            {
                User = user,
                Goal = goal,
                CurrentWeeklyTotal = currentWeeklyTotal,
                GoalProgressMinutes = progress
            };

            model.User.Exercises = userExercises.ToList();

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
    }
}