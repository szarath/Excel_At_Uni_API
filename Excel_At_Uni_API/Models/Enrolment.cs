using Excel_At_Uni_API.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excel_At_Uni_API.Models
{
    public class Enrolment
    {
        [Key]
        public int EnrolmentId { get; set; }

        [Required(ErrorMessage = "Institution is required.")]
        public string Institution { get; set; }

        [Required(ErrorMessage = "Qualification is required.")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Qualification Type is required.")]
        public string QualificationType { get; set; }

        [Required(ErrorMessage = "Date Registered is required.")]
        public DateTime DateRegistered { get; set; }

        [Required(ErrorMessage = "Graduation Date is required.")]
        public DateTime GraduationDate { get; set; }

        [Required(ErrorMessage = "Average to Date is required.")]
        [Range(0, 100, ErrorMessage = "Average to Date should be between 0 and 100.")]
        public decimal AverageToDate { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
