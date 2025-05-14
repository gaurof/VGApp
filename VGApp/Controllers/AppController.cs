using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VGAppDb;

namespace VGApp.Controllers;

public class AppController : Controller
{
    private readonly VGAppDbContext _context;
    public AppController(VGAppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var apps = await _context.Apps.ToListAsync();
        return View(apps);
    }
}
