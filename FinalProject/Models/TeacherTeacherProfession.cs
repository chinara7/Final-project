namespace FinalProject.Models
{
    public class TeacherTeacherProfession
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherProfessionId { get; set; }
        public TeacherProfession TeacherProfession { get; set; }
    }
}
