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
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext db,
                              IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _db.Blogs.OrderByDescending(x => x.Id).ToListAsync();
            return View(blogs);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.Courses.AnyAsync(x => x.Title == blog.Title);
            if (isExist)
            {
                ModelState.AddModelError("Title", "A course under this name has already been created");
                return View(blog);
            }
            if (blog.Title == null) 
            {
                ModelState.AddModelError("Title", "You must write blog title");
                return View(blog);
            }
            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You must choose photo!");
                return View(blog);
            }
            if (!blog.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Choose image type file!");
                return View(blog);
            }
            if (blog.ImageFile.IsMaxLength(3000))
            {
                ModelState.AddModelError("ImageFile", "Your image's length is very big!");
                return View(blog);
            }
            if (blog.Writer == null)
            {
                blog.Writer = "No name";
            }

            string folder = Path.Combine(_env.WebRootPath, "img", "blog");
            blog.Image = await blog.ImageFile.SaveImageAsync(folder);
            await _db.Blogs.AddAsync(blog);
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
            Blog blog = await _db.Blogs.Include(x => x.BlogDetail).FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
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
            Blog blog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
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
            Blog blog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            blog.DeactivationTime = DateTime.UtcNow.AddHours(4);
            blog.IsDeactive = true;
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
            Blog blog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
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
            Blog blog = await _db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            blog.DeactivationTime = null;
            blog.IsDeactive = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Active blogs
        public async Task<IActionResult> Actives()
        {
            List<Blog> blogs = await _db.Blogs.Where(x => x.IsDeactive == false).ToListAsync();
            return View(blogs);
        }
        #endregion

        #region Deactive blogs
        public async Task<IActionResult> Inactives()
        {
            List<Blog> blogs = await _db.Blogs.Where(x => x.IsDeactive).ToListAsync();
            return View(blogs);
        }
        #endregion
    }
}
