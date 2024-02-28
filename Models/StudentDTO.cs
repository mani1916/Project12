using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Project1.Validators;

namespace Project1.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string? StudentName { get; set; } = null;
        [EmailAddress(ErrorMessage = "Enter correct email")]
        public string? Email { get; set; } = null;
        [Required]
        public string Address { get; set; }
        [Required]
        public string DOB { get; set; }

     
    }
}