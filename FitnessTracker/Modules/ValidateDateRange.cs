using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Modules
{
    public class ValidateDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime now = DateTime.Now;
            DateTime expiration = (DateTime)value;

            // your validation logic
            if (expiration >= now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The end date must be after the date you started the goal.");
            }
        }
    }
}
