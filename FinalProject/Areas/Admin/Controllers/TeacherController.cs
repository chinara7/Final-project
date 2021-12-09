using FinalProject.AccessDataLayer;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _db;
        public TeacherController(AppDbContext db)
        {
            _db = db;   
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Teacher> teachers = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).ToListAsync();
            return View(teachers);
        }
        #endregion

        //event-de multiple select ede bilmediyim ucun ve burada da secim etmeli oldugum ucun cehd etmedim
        #region Create

        #endregion

        #region Detail
        public async Task<IActionResult> Info(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teacher teacher = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).Include(x => x.TeacherDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        #endregion

        //burada update ola bilerdi
        #region Update
        public IActionResult Update()
        {
            return View();
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teacher teacher = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
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
            Teacher teacher = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            teacher.DeactivationTime = DateTime.UtcNow.AddHours(4);
            teacher.IsDeactive = true;
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
            Teacher teacher = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
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
            Teacher teacher = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            teacher.DeactivationTime = null;
            teacher.IsDeactive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Active teachers
        public async Task<IActionResult> Actives()
        {
            List<Teacher> teachers = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).Where(x => x.IsDeactive == false).ToListAsync();
            return View(teachers);
        }
        #endregion

        #region Inactive teachers
        public async Task<IActionResult> Inactives()
        {
            List<Teacher> teachers = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).Where(x => x.IsDeactive ).ToListAsync();
            return View(teachers);
        }
        #endregion
    }
}

