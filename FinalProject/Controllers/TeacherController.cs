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
            List<Teacher> teachers = await _db.Teachers.Where(x => x.IsDeactive == false).Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).ToListAsync();
            return View(teachers);
        }
        #endregion

        #region Teacher Detail
        public async Task<IActionResult> TeacherDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Teacher teacher = await _db.Teachers.Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).Include(x => x.TeacherDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
            {
                return View();
            }
            return View(teacher);
        }
        #endregion

        #region Search Teachers
        public async Task<IActionResult> SearchTeacher(string key)
        {
            List<Teacher> teachers = await _db.Teachers.Where(x => x.IsDeactive == false).Include(x => x.teacherTeacherProfessions).ThenInclude(x => x.TeacherProfession).Where(x=>x.FullName.Contains(key) ).ToListAsync();
            return PartialView("_SearchTeacherPartialView", teachers);
        }
        #endregion
    }
}
