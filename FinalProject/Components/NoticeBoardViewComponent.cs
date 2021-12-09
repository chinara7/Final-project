using FinalProject.AccessDataLayer;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Components
{
    public class NoticeBoardViewComponent:ViewComponent
    {
        private readonly AppDbContext _db;
        public NoticeBoardViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Notice> notices = await _db.Notices.OrderByDescending(x=>x.ID).Where(x => x.IsDeactive == false).ToListAsync();
            Video video = await _db.Videos.Where(x => x.IsDeactive == false).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            HomeVM homeVM = new HomeVM
            {
                Notices = notices,
                Videos = video,
            };
            return View(homeVM); 
        }
    }
}
