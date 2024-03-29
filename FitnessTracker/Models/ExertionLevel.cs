﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class ExertionLevel
    {
        [Key]
        public int ExertionLevelId { get; set; }
        public string Description { get; set; }
        public string SelectListDescription { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
