using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class EnjoymentLevel
    {
        [Key]
        public int EnjoymentLevelId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
