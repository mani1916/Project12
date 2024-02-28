using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Validators
{
    //  [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class DateCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if (date < DateTime.Now)
                return new ValidationResult("The date must be greater than current Date");

            return ValidationResult.Success;
        }
    }
}