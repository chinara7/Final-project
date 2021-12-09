using FinalProject.AccessDataLayer;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
           _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _db.Sliders.Where(x=>x.IsDeactive == false).OrderByDescending(x => x.Id).Take(3).ToListAsync();
            List<Course> courses = await _db.Courses.Where(x => x.IsDeactive == false).Take(3).ToListAsync();
            List<Event> events = await _db.Events.Where(x => x.IsDeactive == false).OrderByDescending(x => x.ID).Take(3).ToListAsync();
            List<Testimonial> testimonials = await _db.Testimonials.Where(x => x.IsDeactive == false).ToListAsync();
            List<Blog> blogs = await _db.Blogs.Where(x => x.IsDeactive == false).OrderByDescending(x => x.Id).Take(3).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Sliders = sliders,
                Courses = courses,
                Events = events,
                Testimonials = testimonials,
                Blogs = blogs,
            };
            return View(homeVM);
        }
        #endregion        

        //Home-da search duymesine klikleyende bu sehifeye yonlendirmeli ve orada axtaris etmeli idi. Ancaq biraz cehd etdim alinmadi
        #region Search page
        public IActionResult Search()
        {
            return View();
        }
        #endregion

        #region Search All
        public async Task<IActionResult> SearchAll(string key)
        {
            List<Course> courses = await _db.Courses.Where(x => x.Title.Contains(key)).Take(3).ToListAsync();
            List<Blog> blogs = await _db.Blogs.OrderByDescending(x => x.Id).Where(x => x.Title.Contains(key)).Take(3).ToListAsync();
            List<Event> events = await _db.Events.OrderByDescending(x => x.ID).Where(x => x.Title.Contains(key)).Take(3).ToListAsync();
            List<Teacher> teachers = await _db.Teachers.Where(x => x.FullName.Contains(key)).Take(3).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Courses = courses,
                Events = events,
                Blogs = blogs,
                Teachers = teachers,
            };
            return PartialView("_SearchAllPartialView", homeVM);
        }
        #endregion

        #region Error
        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}
