using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public AccountController(
            SignInManager<VGAppDb.Models.User> signInManager,
            UserManager<VGAppDb.Models.User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login(string? returnUrl = null)
        {
            TempData["ReturnUrl"] = returnUrl ?? "";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            returnUrl = TempData["ReturnUrl"]!.ToString();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return ReturnBack(returnUrl);
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
            return Redirect(returnUrl ?? "/Home/Index");
        }

        [HttpPost]
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
                    return Redirect(returnUrl ?? "/Home/Index");
                }
            }
            return View(model);
        }
        IActionResult ReturnBack(string? returnUrl)
        {
            if (!returnUrl.IsNullOrEmpty())
            {
                List<string> splitUrl = returnUrl.Split('/').ToList();
                splitUrl.Insert(0, "");
                var length = splitUrl.Count;
                return RedirectToAction(splitUrl[length - 1], splitUrl[length - 2], new { area = splitUrl[length - 3] });
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}