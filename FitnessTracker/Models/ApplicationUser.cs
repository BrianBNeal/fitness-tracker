using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FitnessTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
        public virtual ICollection<ExerciseType> ExerciseTypes { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }

    }
}