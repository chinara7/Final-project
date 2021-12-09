using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Student name is required")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "Course name is required")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Response { get; set; }
        public string Image { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
