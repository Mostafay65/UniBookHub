using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniBookHub.Data;
using UniBookHub.Models;

namespace UniBookHub.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context = new AppDbContext();

    public HomeController(ILogger<HomeController> logger,UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("Admin"))
        {
            TempData["Students"] = _userManager.GetUsersInRoleAsync("Student").Result.Count;
            TempData["Colleges"] = _context.Colleges.Count();
            TempData["Courses"] = _context.Courses.Count();
        }
        if (User.IsInRole("IT Administrator"))
        {
            ApplicationUser curUser =
                _context.Users.FirstOrDefault(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            TempData["Students"] = _userManager.GetUsersInRoleAsync("Student").Result
                .Where(s => s.CollegeId == curUser.CollegeId).ToList().Count;
            TempData["Courses"] = _context.Courses.Where(c => c.CollegeId == curUser.CollegeId).Count();
        }
        return View("dashboard");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
