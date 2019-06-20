﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models.ViewModels
{
    public class HomeViewModel
    {
        public ApplicationUser User { get; set; }

        public Goal Goal { get; set; } //current Goal
        public List<Exercise> Exercises { get; set; } //exercises during current Goal

        [Display(Name = "Progress")]
        public int? GoalProgressMinutes => Exercises.Sum(e => e.Duration);

        public double GoalProgressPercentage => Math.Round(((double)GoalProgressMinutes / (double)Goal.Target) * 100);

        public string ProgressColor
        {
            get
            {
                if (GoalProgressPercentage < 40)
                {
                    return "bg-danger";
                }
                else if (GoalProgressPercentage < 70)
                {
                    return "bg-warning";
                }
                else if (GoalProgressPercentage < 90)
                {
                    return "bg-info";
                }
                else if (GoalProgressPercentage < 100)
                {
                    return "bg-success";
                }
                else
                {
                    return "bg-success progress-bar-striped progress-bar-animated";
                }
            }
        }

        //total minutes for current week
        [Display(Name = "Recent Activity")]
        public int CurrentWeeklyTotal { get; set; }

        public List<Exercise> RecentExercises { get; set; } // 3 most recent exercises




    }
}
