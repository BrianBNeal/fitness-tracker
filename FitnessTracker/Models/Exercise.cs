using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        public int ExerciseTypeId { get; set; }

        [Required]
        public ExerciseType ExerciseType { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int EnjoymentRating { get; set; }

        [Required]
        public int ExertionLevel { get; set; }

        [Required]
        public DateTime DateLogged { get; set; }

        public string Comments { get; set; }


        public bool IsThisWeeksActivity()
        {
            // Gets the Calendar instance associated with a CultureInfo.
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            //check to see if the DateLogged property is in the same calendar week as today
            if (myCal.GetWeekOfYear(DateLogged, myCWR, myFirstDOW) == myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW))
            {
                return true;
            }
            return false;
        }

    }
}
