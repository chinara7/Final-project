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
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin")]
    public class TestimonialController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public TestimonialController(AppDbContext db,
                                        IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Testimonial> testimonials = await _db.Testimonials.ToListAsync();
            return View(testimonials);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial testimonial)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (testimonial.Response == null)
            {
                ModelState.AddModelError("Response", "This field is required");
                return View(testimonial);
            }
            if (testimonial.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You must choose photo!");
                return View(testimonial);
            }
            if (!testimonial.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Choose image type file!");
                return View(testimonial);
            }
            if (testimonial.ImageFile.IsMaxLength(3000))
            {
                ModelState.AddModelError("ImageFile", "Your image's length is very big!");
                return View(testimonial);
            }
            string folder = Path.Combine(_env.WebRootPath, "img", "testimonial");
            testimonial.Image = await testimonial.ImageFile.SaveImageAsync(folder);
            await _db.Testimonials.AddAsync(testimonial);
            await _db.SaveChangesAsync();
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
            Testimonial testimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }
            return View(testimonial);
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
            Testimonial testimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }
            testimonial.DeactivationTime = DateTime.UtcNow.AddHours(4);
            testimonial.IsDeactive = true;
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
            Testimonial testimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }
            return View(testimonial);
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
            Testimonial testimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }
            testimonial.DeactivationTime = null;
            testimonial.IsDeactive = false;
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
            Testimonial testimonial = await _db.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }
            return View(testimonial);
        }
        #endregion

    }
}
