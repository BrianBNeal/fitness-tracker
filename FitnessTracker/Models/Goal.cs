using FitnessTracker.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date),
            Display(Name = "End Date"),
            DisplayFormat(DataFormatString = "{0:M/d/yyyy}")]
        [ValidateDateRange]
        public DateTime EndDate { get; set; }

        [Required]
        public int Target { get; set; } //user's target for amount of time spent on goal


        public bool IsDone()
        {
            if (EndDate.Date < DateTime.Now.Date)
            {
                return true;
            }
            return false;
        }

        public bool WasASuccess(List<Exercise> exercises)
        {
            int progress = exercises.Select(e => e.Duration).Sum();
            if (progress >= Target)
            {
                return true;
            }
            return false;
        }
    }
}
