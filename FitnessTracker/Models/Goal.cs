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

        [Required(ErrorMessage = "This field is required")]
        [Range(1, 10000, ErrorMessage = "Please enter a value from 1 to 10,000")]
        public int Target { get; set; } //user's target for amount of time spent on goal


        //parameter 'exercises' is collection of exercises that apply to this goal's target
        public bool IsDone(IEnumerable<Exercise> exercises)
        {
            if (EndDate.Date < DateTime.Now || this.TargetWasMet(exercises))
            {
                return true;
            }
            return false;
        }

        //parameter 'exercises' is collection of exercises that apply to this goal's target
        public bool TargetWasMet(IEnumerable<Exercise> exercises)
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
