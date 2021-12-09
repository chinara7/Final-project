using FinalProject.AccessDataLayer;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Components
{
    public class BlogPartViewComponent: ViewComponent
    {
        private readonly AppDbContext _db;
        public BlogPartViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Blog> blogs = await _db.Blogs.Where(x => x.IsDeactive == false).OrderByDescending(x => x.Id).Take(3).ToListAsync();
            return View(blogs);
        }
    }
}
