
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Course
    {
        public int Id { get; set; }        
        public string Image { get; set; }
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public CourseDetail CourseDetail { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        [NotMapped]
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}
