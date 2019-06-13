using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }


        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
