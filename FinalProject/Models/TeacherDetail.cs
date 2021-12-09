using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class TeacherDetail
    {
        public int Id { get; set; }
        public string About { get; set; }
        public string SpecialtyDegree { get; set; }
        public string WorkExperience { get; set; }
        public string Hobbies { get; set; }
        public string Faculty { get; set; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string SkypeName { get; set; }
        public double Language { get; set; }
        public double TeamLeader { get; set; }
        public double Development { get; set; }
        public double Design { get; set; }
        public double Innovation { get; set; }
        public double Communication { get; set; }
        public Teacher Teacher { get; set; }
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
    }
}
