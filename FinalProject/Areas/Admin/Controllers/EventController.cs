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
    [Authorize(Roles = "Admin")]
    public class EventController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public EventController(AppDbContext _db,
                                IWebHostEnvironment env)
        {
            this._db = _db;
            _env = env;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _db.Events.OrderByDescending(x => x.ID).ToListAsync();
            return View(events);
        }
        #endregion

        //sehv var : speakers-i select ile cagirmaq istedim ancaq ede bilmedim Ona gore yazmadim
        #region Create
        public IActionResult Create()
        {
            return View();
        }
      
        #endregion

        #region Detail
        public async Task<IActionResult> Info(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Event event_ = await _db.Events.Include(x => x.eventEventSpeakers).ThenInclude(x => x.EventSpeaker).Include(x => x.EventDetail).FirstOrDefaultAsync(x => x.ID == id);
            if (event_ == null)
            {
                return NotFound();
            }
            return View(event_);
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
            Event event_ = await _db.Events.FirstOrDefaultAsync(x => x.ID == id);
            if (event_ == null)
            {
                return NotFound();
            }
            return View(event_);
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
            Event event_ = await _db.Events.FirstOrDefaultAsync(x => x.ID == id);
            if (event_ == null)
            {
                return NotFound();
            }
            event_.DeactivationTime = DateTime.UtcNow.AddHours(4);
            event_.IsDeactive = true;
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
            Event event_ = await _db.Events.FirstOrDefaultAsync(x => x.ID == id);
            if (event_ == null)
            {
                return NotFound();
            }
            return View(event_);
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
            Event event_ = await _db.Events.FirstOrDefaultAsync(x => x.ID == id);
            if (event_ == null)
            {
                return NotFound();
            }
            event_.DeactivationTime = null;
            event_.IsDeactive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Active events
        public async Task<IActionResult> Actives()
        {
            List<Event> event_ = await _db.Events.Where(x => x.IsDeactive == false).ToListAsync();
            return View(event_);
        }
        #endregion

        #region Inactive events
        public async Task<IActionResult> Inactives()
        {
            List<Event> event_ = await _db.Events.Where(x => x.IsDeactive).ToListAsync();
            return View(event_);
        }
        #endregion
    }
}

