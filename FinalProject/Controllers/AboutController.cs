using FinalProject.AccessDataLayer;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        public AboutController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _db.Teachers.Where(x => x.IsDeactive == false).Include(x=>x.teacherTeacherProfessions).ThenInclude(x=>x.TeacherProfession).Take(4).ToListAsync();
            List<Testimonial> testimonials = await _db.Testimonials.Where(x => x.IsDeactive == false).ToListAsync();
            AboutVM aboutVM = new AboutVM
            {
                Teachers = teachers,
                Testimonials = testimonials
            };
            return View(aboutVM); ;
        }
        #endregion
    }
}
