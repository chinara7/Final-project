using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class TeacherProfession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeacherTeacherProfession> teacherTeacherProfessions { get; set; }
    }
}
