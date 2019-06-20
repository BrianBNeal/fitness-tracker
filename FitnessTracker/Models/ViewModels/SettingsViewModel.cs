using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models.ViewModels
{
    public class SettingsViewModel
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Goal MostRecentGoal { get; set; }
        public List<Exercise> GoalExercises { get; set; } //exercises that apply to MostRecentGoal
    }
}
