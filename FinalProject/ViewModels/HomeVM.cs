using FinalProject.Models;
using System.Collections.Generic;

namespace FinalProject.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders{ get; set; }
        public List<Course> Courses { get; set; }
        public List<Notice> Notices { get; set; }
        public About Abouts { get; set; }
        public List<Event> Events { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Blog> Blogs { get; set; }
        public Video Videos { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
