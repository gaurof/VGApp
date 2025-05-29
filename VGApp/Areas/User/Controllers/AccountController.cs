using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VGApp.Areas.User.Views.Models;
using VGApp.Controllers;
using VGApp.Models;
using VGAppDb.Models;

namespace VGApp.Areas.User.Controllers
{
    [Area(Constants.UserRoleName)]
    public class AccountController : Controller
    {
        private readonly SignInManager<VGAppDb.Models.User> _signInManager;
        private readonly UserManager<VGAppDb.Models.User> _userManager;
        private const string _defaultRedirectUrl = "/Home/Index";
        public AccountController(
            SignInManager<VGAppDb.Models.User> signInManager,
            UserManager<VGAppDb.Models.User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToOrHome(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            return RedirectToOrHome(returnUrl);
        }

        [AllowAnonymous]
        public IActionResult Register(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new VGAppDb.Models.User { UserName = model.Username };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Constants.UserRoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToOrHome(returnUrl);
                }
            }
            return View(model);
        }


        public IActionResult RedirectToOrHome(string? returnUrl)
        {
            if (returnUrl is null)
                return RedirectToAction(_defaultRedirectUrl);
            return RedirectToAction(returnUrl);
        }
    }
}