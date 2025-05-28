using Microsoft.AspNetCore.Mvc;

namespace VGApp.Areas.User.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
