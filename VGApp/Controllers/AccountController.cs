using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VGApp.ViewModels;
using VGAppDb.Models;

namespace VGApp.Controllers
{
    public class AccountController(SignInManager<User> signInManager,
                                   UserManager<User> userManager) : Controller
    {
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly UserManager<User> _userManager = userManager;

        [AllowAnonymous]
        public async Task<IActionResult> Info(string? userId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                if (User.Identity?.IsAuthenticated ?? false)
                    userId = _userManager.GetUserId(User);
                else
                    return RedirectToAction("Login", "Account", new { returnUrl = $"/Account/Info" });
            }

            var user = await _userManager.FindByIdAsync(userId!);

            if (user is null) 
                return NotFound();

            var userWithData = await _userManager.Users
                .Include(u => u.Reviews)
                    .ThenInclude(r => r.Game)
                .Include(u => u.GamesPlayed)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (userWithData is null)
                return NotFound();

            userWithData.Reviews = userWithData.Reviews?
                .OrderByDescending(r => r.PublicationTime)
                .ToList() ?? [];

            return View(userWithData);
        }





        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/Home/Index")
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var returnUrl = (TempData["ReturnUrl"] ?? "/Home/Index").ToString();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Redirect(returnUrl ?? "/Home/Index");
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
            if (returnUrl is not null &&
                returnUrl.TrimStart('/').StartsWith("Admin"))
                returnUrl = null;
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl ?? "/Home/Index");
        }

        [AllowAnonymous]
        public IActionResult Register(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl ?? string.Empty;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? "/Home/Index";
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username };
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
    }
}