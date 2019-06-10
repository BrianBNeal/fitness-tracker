using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public int ExerciseTypeId { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int EnjoymentRating { get; set; }

        [Required]
        public int ExertionLevel { get; set; }

        public string Comments { get; set; }

        public ApplicationUser User { get; set; }
        public Location Location { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
