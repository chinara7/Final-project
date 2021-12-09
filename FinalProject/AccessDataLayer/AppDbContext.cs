using FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.AccessDataLayer
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Slider> Sliders { get; set; }   
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Notice> Notices { get; set; }        
        public DbSet<Event> Events { get; set; }       
        public DbSet<EventDetail> EventDetails { get; set; }       
        public DbSet<EventSpeaker> EventSpeakers { get; set; }               
        public DbSet<EventEventSpeaker> EventEventSpeakers { get; set; }               
        public DbSet<Testimonial> Testimonials { get; set; } 
        public DbSet<Blog> Blogs { get; set; }        
        public DbSet<BlogDetail> BlogDetails { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherDetail> TeacherDetails { get; set; }
        public DbSet<TeacherProfession> TeacherProfessions { get; set; }
        public DbSet<TeacherTeacherProfession> TeacherTeacherProfessions { get; set; }
    }
}
