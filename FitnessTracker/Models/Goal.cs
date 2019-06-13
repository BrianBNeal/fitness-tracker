using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime),
            DisplayFormat(DataFormatString = "{0:M/d/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        public int Target { get; set; } //user's target for amount of time spent on goal

    }
}
