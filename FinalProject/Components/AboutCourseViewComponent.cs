using FinalProject.AccessDataLayer;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Components
{
    public class AboutCourseViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public AboutCourseViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            About about = await _db.Abouts.FirstOrDefaultAsync();
            HomeVM homeVM = new HomeVM
            {
                Abouts = about,
            };
            return View(homeVM);
        }
    }
}
