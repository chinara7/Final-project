using FinalProject.AccessDataLayer;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_db.Blogs.Where(x => x.IsDeactive == false).Count() / 6);
            List<Blog> blogs = await _db.Blogs.Where(x => x.IsDeactive == false).OrderByDescending(x => x.Id).Skip((page - 1) * 6).Take(6).ToListAsync();
            ViewBag.CurrentPage = page;
            return View(blogs);
        }
        #endregion

        #region BlogDetail
        public async Task<IActionResult> BlogDetail(int? id)
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

        #region Search Blogs
        public async Task<IActionResult> SearchBlog(string key)
        {
            List<Blog> blogs = await _db.Blogs.Where(x => x.IsDeactive == false).OrderByDescending(x => x.Id).Where(x => x.Writer.Contains(key) || x.Title.Contains(key) || x.CreateTime.ToString().Contains(key)).ToListAsync();
            return PartialView("_SearchBlogPartialView", blogs);
        }
        #endregion
    }
}
