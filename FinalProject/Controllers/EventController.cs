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
    public class EventController : Controller
    {
        private readonly AppDbContext _db;
        public EventController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Event> events = await _db.Events.Where(x => x.IsDeactive == false).OrderByDescending(x => x.ID).ToListAsync();
            return View(events);
        }
        #endregion

        #region EventDetail
        public async Task<IActionResult> EventDetail(int? id)
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

        #region Search Events
        public async Task<IActionResult> SearchEvent(string key)
        {
            List<Event> events = await _db.Events.Where(x => x.IsDeactive == false).OrderByDescending(x => x.ID).Where(x => x.Title.Contains(key) || x.Venue.Contains(key) || x.Date.ToString().Contains(key) || x.StartTime.ToString().Contains(key) || x.EndTime.ToString().Contains(key)).ToListAsync();
            return PartialView("_SearchEventPartialView", events);
        }
        #endregion
    }
}
