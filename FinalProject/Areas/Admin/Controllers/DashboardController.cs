using FinalProject.AccessDataLayer;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        private readonly SignInManager<AppUser> _signInManager;
        public DashboardController(SignInManager<AppUser> signInManager,
                                    AppDbContext db)
        {
            _db = db;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //admin panelde logout etmek isteyirdim

        //public async Task<IActionResult> LogOut()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
