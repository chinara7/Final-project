using FinalProject.Extensions;
using FinalProject.Models;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<AppUser> signInManager,
                                 ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = await _userManager.FindByNameAsync(loginVM.Username);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View();
            }
            if (appUser.IsDeactive)
            {
                ModelState.AddModelError("", "This account was deleted!");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "You have entered an incorrect code too many times and your account is temporarily locked. Please try again later.");
                return View();
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                UserName = registerVM.Username,
                Email = registerVM.Email,
            };
            IdentityResult ıdentityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!ıdentityResult.Succeeded)
            {
                foreach (IdentityError ıdentityError in ıdentityResult.Errors)
                {
                    ModelState.AddModelError("", ıdentityError.Description);
                }
                return View();
            }
            else
            {
                await _userManager.AddToRoleAsync(appUser, Helper.Roles.Member.ToString());
                await _signInManager.SignInAsync(appUser, true);
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region LogOut
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region CreateRole
        //public async Task CreateRole()
        //{
        //    if (!(await _roleManager.RoleExistsAsync(Helper.Roles.Admin.ToString())))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Admin.ToString() });
        //    }
        //    if (!(await _roleManager.RoleExistsAsync(Helper.Roles.Moderator.ToString())))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Moderator.ToString() });
        //    }
        //    if (!(await _roleManager.RoleExistsAsync(Helper.Roles.Member.ToString())))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helper.Roles.Member.ToString() });
        //    }
        //}
        #endregion

    }
}
//burada Confirm Email yaratmisdim Ancaq haradasa sehvim oldugu ucun mail gelmirdi Ancaq sql-de user yaranirdi

//#region Login
//public IActionResult Login()
//{
//    return View();
//}
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Login(LoginVM loginVM)
//{
//    if (!ModelState.IsValid)
//    {
//        return View();
//    }
//    AppUser appUser = await _userManager.FindByNameAsync(loginVM.Username);
//    if (appUser == null)
//    {
//        ModelState.AddModelError("", "Username or password is invalid!");
//        return View();
//    }
//    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, true);
//    if (signInResult.IsLockedOut)
//    {
//        ModelState.AddModelError("", "You have entered an incorrect code too many times and your account is temporarily locked. Please try again later.");
//        return View();
//    }
//    if (!signInResult.Succeeded)
//    {
//        ModelState.AddModelError("", "Username or password is invalid!");
//        return View();
//    }
//    return RedirectToAction("Index", "Home");
//}
//#endregion

//#region Register
//        public IActionResult Register()
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                return NotFound();
//            }
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(RegisterVM registerVM, string returnUrl)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View();
//            }
//            AppUser appUser = new AppUser
//            {
//                Name = registerVM.Name,
//                Surname = registerVM.Surname,
//                UserName = registerVM.Username,
//                Email = registerVM.Email,
//            };
//            //Startup-dan gelen Errors yoxlayir
//            IdentityResult ıdentityResult = await _userManager.CreateAsync(appUser, registerVM.Password);
//            if (ıdentityResult.Succeeded)
//            {
//                string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
//                string confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = appUser.Id, token  }, Request.Scheme);
//                _logger.Log(LogLevel.Warning, confirmationLink);

//                ViewBag.ErrorTitle = "Registration successful";
//                ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
//                        "email, by clicking on the confirmation link we have emailed you";
//                return View("Error");
//            }
//            else
//            {
//                foreach (IdentityError ıdentityError in ıdentityResult.Errors)
//                {
//                    ModelState.AddModelError("", ıdentityError.Description);
//                }
//            }          

//            return View(registerVM);
//        }
//        #endregion




//#region ConfirmEmail
//[AllowAnonymous]
//public async Task<IActionResult> ConfirmEmail(string userId, string token)
//{
//    if (userId == null || token == null)
//    {
//        return RedirectToAction("index", "home");
//    }
//    AppUser appUser = await _userManager.FindByIdAsync(userId);
//    if (appUser == null)
//    {
//        return NotFound();
//    }
//    IdentityResult ıdentityResult = await _userManager.ConfirmEmailAsync(appUser, token);
//    if (!ıdentityResult.Succeeded)
//    {
//        ViewBag.ErrorTitle = "Email cannot be confirmed";
//        return View("Error");
//    }
//    await _userManager.AddToRoleAsync(appUser, Helper.Roles.Member.ToString());
//    await _signInManager.SignInAsync(appUser, true);

//    return RedirectToAction("Login", "Account");
//}
//#endregion