using FinalProject.AccessDataLayer;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static FinalProject.Extensions.Helper;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class NoticeController : Controller
    {
        private readonly AppDbContext _db;
        public NoticeController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Notice> notices = await _db.Notices.ToListAsync();
            return View(notices);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice notice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (notice.Description == null)
            {
                ModelState.AddModelError("Description", "Description section is required");
                return View(notice);
            }
            await _db.Notices.AddAsync(notice);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        //sehv 
        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Notice notice = await _db.Notices.FirstOrDefaultAsync(x => x.ID == id);
            if (notice == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Notice notice)
        {
            if (id == null)
            {
                return NotFound();
            }
            Notice dbnotice = await _db.Notices.FirstOrDefaultAsync(x => x.ID == id);
            if (dbnotice == null)
            {
                return NotFound();
            }            
            if (dbnotice.Description == null)
            {
                ModelState.AddModelError("Description", "Description section is required");
                return View(dbnotice);
            }
            
            await _db.Notices.AddAsync(dbnotice);
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
            Notice notice  = await _db.Notices.FirstOrDefaultAsync(x => x.ID == id);
            if (notice == null)
            {
                return NotFound();
            }
            return View(notice);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Notice notice = await _db.Notices.FirstOrDefaultAsync(x => x.ID == id);
            if (notice == null)
            {
                return NotFound();
            }
            return View(notice);
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
            Notice notice = await _db.Notices.FirstOrDefaultAsync(x => x.ID == id);
            if (notice == null)
            {
                return NotFound();
            }
            notice.DeactivationTime = DateTime.UtcNow.AddHours(4);
            notice.IsDeactive = true;
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
            Notice notice = await _db.Notices.FirstOrDefaultAsync(x => x.ID == id);
            if (notice == null)
            {
                return NotFound();
            }
            return View(notice);
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
            Notice notice = await _db.Notices.FirstOrDefaultAsync(x => x.ID == id);
            if (notice == null)
            {
                return NotFound();
            }
            notice.DeactivationTime = null;
            notice.IsDeactive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
