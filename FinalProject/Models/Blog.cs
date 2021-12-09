using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Writer { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        public DateTime CreateTime { get; set; }
        public int CommentCount { get; set; }
        public BlogDetail BlogDetail { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
