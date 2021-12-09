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
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
        public CourseController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _db.Courses.Where(x => x.IsDeactive == false).ToListAsync();
            return View(courses);
        }
        #endregion

        #region CourseDetail
        public async Task<IActionResult> CourseDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = await _db.Courses.Include(x => x.CourseDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        #endregion

        #region Search Courses
        public async Task<IActionResult> SearchCourse(string key)
        {
            List<Course> courses = await _db.Courses.Where(x => x.IsDeactive == false).Where(x => x.Title.Contains(key) || x.SubTitle.Contains(key)).ToListAsync();
            return PartialView("_SearchCoursePartialView", courses);
        }
        #endregion
    }
}
