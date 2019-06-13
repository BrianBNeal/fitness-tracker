using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models.ViewModels
{
    public class HomeViewModel
    {
        public ApplicationUser User { get; set; }
        public Goal Goal { get; set; }

        [Display(Name = "Progress")]
        public int GoalProgressMinutes { get; set; }
        public double GoalProgressPercentage => ((double)GoalProgressMinutes / (double)Goal.Target) * 100;
        public string ProgressColor
        {
            get
            {
                if (GoalProgressPercentage < 40)
                {
                    return "bg-danger";
                }
                else if (GoalProgressPercentage < 75)
                {
                    return "bg-warning";
                }
                else
                {
                    return "bg-success";
                }
            }
        }

        //total minutes for current week
        [Display(Name = "Recent Activity")]
        public int CurrentWeeklyTotal { get; set; }




    }
}
