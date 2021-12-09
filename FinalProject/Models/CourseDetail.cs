using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class CourseDetail
    {
        public int Id { get; set; }   
        public string Header { get; set; }       
        public string About { get; set; }        
        public string Apply { get; set; }  
        public string Certification { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Starts { get; set; }
        public double Duration { get; set; }
        public double ClassDuration { get; set; }
        [Required]
        public string SkillLevel { get; set; }
        [Required]
        public string Language { get; set; }
        [Range(1, 50, ErrorMessage = "Count must between 1 to 50")]
        public int StudentCount { get; set; }
        [Required]
        public string Assessments { get; set; }
        
        public double CourseFee { get; set; }
        public Course Course { get; set; }
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        
    }
}
