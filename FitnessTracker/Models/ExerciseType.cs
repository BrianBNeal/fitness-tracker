using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class ExerciseType
    {
        public int ExerciseTypeId { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
