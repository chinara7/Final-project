using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string SliderImage { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        [Required(ErrorMessage ="You must add slider image")]
        [NotMapped]
        public IFormFile SliderImageFile { get; set; }
        [Required(ErrorMessage = "You must add image")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
