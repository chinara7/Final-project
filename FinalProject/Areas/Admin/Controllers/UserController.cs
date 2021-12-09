using FinalProject.Extensions;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin, Moderator")]
    public class UserController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<AppUser> appUsers = _userManager.Users.ToList();
            List<UserVM> users = new List<UserVM>();
            foreach (AppUser user in appUsers)
            {
                UserVM userVM = new UserVM
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.UserName,
                    Email = user.Email,
                    Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                };
                users.Add(userVM);
            }
            return View(users);
        }
        #endregion

        //database-de deyisir ancaq ekranda gorunmur
        #region Activation
        public async Task<IActionResult> Activation( string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            if (appUser.IsDeactive) 
            {
                appUser.IsDeactive = false;
            }
            else
            {
                appUser.IsDeactive = true;
            }
            await _userManager.UpdateAsync(appUser);
            return RedirectToAction("Index");
        }
        #endregion


        //nese sehv var  view olan hisse acilir ancaq yes secende error verir
        #region ChangeRole
        public async Task<IActionResult> ChangeRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            ChangeRoleVM changeRoleVM = new ChangeRoleVM()
            {
                Username = appUser.UserName,
                RoleName = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault()
            };
            List<string> roles = new List<string>();
            roles.Add(Helper.Roles.Moderator.ToString());
            roles.Add(Helper.Roles.Admin.ToString());
            roles.Add(Helper.Roles.Member.ToString());
            ViewBag.Roles = roles;
            return View(changeRoleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id, ChangeRoleVM changeRoleVM)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            ChangeRoleVM changeRoleVM2 = new ChangeRoleVM()
            {
                Username = appUser.UserName,
                RoleName = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault()
            };
            List<string> roles = new List<string>();
            roles.Add(Helper.Roles.Moderator.ToString());
            roles.Add(Helper.Roles.Admin.ToString());
            roles.Add(Helper.Roles.Member.ToString());
            ViewBag.Roles = roles;

            IdentityResult addIdentityResult = await _userManager.AddToRoleAsync(appUser, changeRoleVM.RoleName);
            if (!addIdentityResult.Succeeded)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(changeRoleVM2);
            }
            IdentityResult remIdentityResult = await _userManager.RemoveFromRoleAsync(appUser, changeRoleVM2.RoleName);
            if (!remIdentityResult.Succeeded)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(changeRoleVM2);
            }
            await _userManager.UpdateAsync(appUser);
            return RedirectToAction("Index");
        }
        #endregion


        //bos submit etdikde null argument erroru cixarir
        #region ResetPassword
        public async Task<IActionResult> ResetPassword(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id, ResetPasswordVM resetPasswordVM)
        {
            if (id == null)
            {
                return NotFound();
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            IdentityResult ıdentityResult = await _userManager.ResetPasswordAsync(appUser, await _userManager.GeneratePasswordResetTokenAsync(appUser), resetPasswordVM.Password);
           
            if (!ıdentityResult.Succeeded)
            {
                foreach (IdentityError item in ıdentityResult.Errors)
                {
                    ModelState.AddModelError(" ", item.Description);
                }
                return View();
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
