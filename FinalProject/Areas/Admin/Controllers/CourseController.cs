using FinalProject.AccessDataLayer;
using FinalProject.Extensions;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles="Admin, Moderator")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CourseController(AppDbContext db,
                                IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Course> courses = await _db.Courses.ToListAsync();
            return View(courses);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.Courses.AnyAsync(x => x.Title == course.Title);
            if (isExist)
            {
                ModelState.AddModelError("Title", "A course under this name has already been created");
                return View(course);
            }
            if (course.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You must choose photo!");
                return View(course);
            }
            if (!course.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Choose image type file!");
                return View(course);
            }
            if (course.ImageFile.IsMaxLength(3000))
            {
                ModelState.AddModelError("ImageFile", "Your image's length is very big!");
                return View(course);
            }
            if (course.CourseDetail.About == null)
            {
                ModelState.AddModelError("CourseDetail.About", "About section is required");
                return View(course);
            }
            if (course.CourseDetail.Apply == null)
            {
                ModelState.AddModelError("CourseDetail.Apply", "Apply section is required");
                return View(course);
            }
            if (course.CourseDetail.Certification == null)
            {
                ModelState.AddModelError("CourseDetail.Certification", "Certification section is required");
                return View(course);
            }
            if (course.CourseDetail.Header == null)
            {
                ModelState.AddModelError("CourseDetail.Header", "Description section is required");
                return View(course);
            }

            string folder = Path.Combine(_env.WebRootPath, "img", "course");
            course.Image = await course.ImageFile.SaveImageAsync(folder);
            await _db.Courses.AddAsync(course);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Info(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            Course course = await _db.Courses.Include(x=>x.CourseDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        #endregion

        //duzgun yazmamisam 
        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }
            Course course = await _db.Courses.Include(x=>x.CourseDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Course course)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course dbcourse = await _db.Courses.Include(x => x.CourseDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (dbcourse == null)
            {
                return NotFound();
            }
            bool isExist = await _db.Courses.AnyAsync(x => x.Title==course.Title && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("", "The course was created earlier.");
                return View(dbcourse);
            }
            if (dbcourse.CourseDetail.About == null)
            {
                ModelState.AddModelError("CourseDetail.About", "About section is required");
                return View(dbcourse);
            }
            if (dbcourse.CourseDetail.Apply == null)
            {
                ModelState.AddModelError("CourseDetail.Apply", "Apply section is required");
                return View(dbcourse);
            }
            if (dbcourse.CourseDetail.Certification == null)
            {
                ModelState.AddModelError("CourseDetail.Certification", "Certification section is required");
                return View(dbcourse);
            }
            if (dbcourse.CourseDetail.Header == null)
            {
                ModelState.AddModelError("CourseDetail.Header", "Description section is required");
                return View(dbcourse);
            }
            if (dbcourse.ImageFile != null)
            {
                if (!dbcourse.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose image type file!");
                    return View(dbcourse);
                }
                if (dbcourse.ImageFile.IsMaxLength(3000))
                {
                    ModelState.AddModelError("ImageFile", "Your image's length is very big!");
                    return View(dbcourse);
                }
                string folder = Path.Combine(_env.WebRootPath, "img", "course");
                dbcourse.Image = await dbcourse.ImageFile.SaveImageAsync(folder);
            }
            else
            {                
                await _db.Courses.AddAsync(dbcourse);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            course.DeactivationTime = DateTime.UtcNow.AddHours(4);
            course.IsDeactive = true;            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Activate
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Activate))]
        public async Task<IActionResult> ActivatePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            course.DeactivationTime = null;
            course.IsDeactive = false;            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Active courses
        public async Task<IActionResult> Actives()
        {
            List<Course> courses = await _db.Courses.Where(x => x.IsDeactive == false).ToListAsync();
            return View(courses);
        }
        #endregion

        #region Deactive courses
        public async Task<IActionResult> Inactives()
        {
            List<Course> courses = await _db.Courses.Where(x => x.IsDeactive).ToListAsync();
            return View(courses);
        }
        #endregion
    }
}
