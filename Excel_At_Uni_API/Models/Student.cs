using Excel_At_Uni_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Excel_At_Uni_API.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Id Number is required.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Invalid ID Number.")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Contact Number.")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Preferred Method of Contact is required.")]
        public string PreferredMethodOfContact { get; set; }

        [Required(ErrorMessage = "Profile Image is required.")]
        [AllowedExtensions(ErrorMessage = "Only JPG files with a maximum size of 2MB are allowed.")]
        public byte[] ProfileImageFile { get; set; }

        public ICollection<Enrolment> Enrolments { get; set; }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        //This line of code declares a private read-only string array called _extensions and initializes it with the value ".jpg".
        private readonly string[] _extensions = { ".jpg" };

        /// <summary>
        /// Validates a file to ensure it is of the correct type and size.
        /// </summary>
        /// <returns>ValidationResult.Success if the file is valid, otherwise a ValidationResult with an error message.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var file = value as IFormFile;

            if (file == null)
            {
                return new ValidationResult("Invalid file type.");
            }

            var extension = Path.GetExtension(file.FileName);

            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult($"Only files with extensions {_extensions} are allowed.");
            }

            if (file.Length > 2 * 1024 * 1024)
            {
                return new ValidationResult("File size should not exceed 2MB.");
            }

            return ValidationResult.Success;
        }
    }

}
