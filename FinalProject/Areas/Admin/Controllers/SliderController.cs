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
    public class SliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext db,
                                IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _db.Sliders.ToListAsync();
            return View(sliders);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slider.Title == null)
            {
                ModelState.AddModelError("Title", "Title section is required");
                return View(slider);
            }
            if (slider.Description == null)
            {
                ModelState.AddModelError("Description", "Description section is required");
                return View(slider);
            }
            //hal-hazirda slider-de olan sekli yeniden yuklememesi ucun ne etmek olar bilmedim. Adina gore yoxlamaq duz olmaz deye dusunurem cunki eyni sekli basqa adla da saxlamaq olar
            if (slider.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You must choose photo!");
                return View(slider);
            }
            if (!slider.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Choose image type file!");
                return View(slider);
            }
            if (slider.ImageFile.IsMaxLength(3000))
            {
                ModelState.AddModelError("ImageFile", "Your image's length is very big!");
                return View(slider);
            }
            if (slider.SliderImageFile == null)
            {
                ModelState.AddModelError("SliderImageFile", "You must choose photo!");
                return View(slider);
            }
            if (!slider.SliderImageFile.IsImage())
            {
                ModelState.AddModelError("SliderImageFile", "Choose image type file!");
                return View(slider);
            }
            if (slider.SliderImageFile.IsMaxLength(3000))
            {
                ModelState.AddModelError("SliderImageFile", "Your image's length is very big!");
                return View(slider);
            }

            string folder1 = Path.Combine(_env.WebRootPath, "img", "slider");
            slider.Image = await slider.ImageFile.SaveImageAsync(folder1);
            string folder2 = Path.Combine(_env.WebRootPath, "img", "slider");
            slider.SliderImage = await slider.SliderImageFile.SaveImageAsync(folder2);
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        //sehvdir
        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbslider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbslider == null)
            {
                return NotFound();
            }
            if (dbslider.Title == null)
            {
                ModelState.AddModelError("Title", "Title section is required");
                return View(dbslider);
            }
            if (dbslider.Description == null)
            {
                ModelState.AddModelError("Description", "Description section is required");
                return View(dbslider);
            }
            if (dbslider.ImageFile != null)
            {
                if (!dbslider.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Choose image type file!");
                    return View(dbslider);
                }
                if (dbslider.ImageFile.IsMaxLength(3000))
                {
                    ModelState.AddModelError("ImageFile", "Your image's length is very big!");
                    return View(dbslider);
                }
                string folder1 = Path.Combine(_env.WebRootPath, "img", "slider");
                dbslider.Image = await dbslider.ImageFile.SaveImageAsync(folder1);
            }
            if (dbslider.SliderImageFile != null)
            {
                if (!dbslider.SliderImageFile.IsImage())
                {
                    ModelState.AddModelError("SliderImageFile", "Choose image type file!");
                    return View(dbslider);
                }
                if (dbslider.SliderImageFile.IsMaxLength(3000))
                {
                    ModelState.AddModelError("SliderImageFile", "Your image's length is very big!");
                    return View(dbslider);
                }
                string folder2 = Path.Combine(_env.WebRootPath, "img", "slider");
                dbslider.SliderImage = await dbslider.SliderImageFile.SaveImageAsync(folder2);
            }           
            
            //await _db.Sliders.AddAsync(dbslider);
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
            Slider slider  = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
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
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            slider.DeactivationTime = DateTime.UtcNow.AddHours(4);
            slider.IsDeactive = true;
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
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
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
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            slider.DeactivationTime = null;
            slider.IsDeactive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
