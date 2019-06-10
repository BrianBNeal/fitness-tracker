using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class Goal
    {
        public int GoalId { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int Target { get; set; } //user's target for amount of time spent on goal

        public ApplicationUser User { get; set; }
    }
}
