using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class Motivation
    {
        public int MotivationId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
