using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string FacebookLink { get; set; }
        public string PinterestLink { get; set; }
        public string VimeoLink { get; set; }
        public string TwitterLink { get; set; }
        public TeacherDetail TeacherDetail { get; set; }
        public List<TeacherTeacherProfession> teacherTeacherProfessions { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime? DeactivationTime { get; set; }
        public DateTime? CreatedTime { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
