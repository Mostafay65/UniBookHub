using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBookHub.Data;
using UniBookHub.Models;
using UniBookHub.ViewModels;
namespace UniBookHub.Controllers;

public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public RoleController(RoleManager<IdentityRole> _roleManager,UserManager<ApplicationUser> userManager,AppDbContext _context)
    {
        this._roleManager = _roleManager;
        _userManager = userManager;
        this._context = _context;
    }
    // GET
    public IActionResult Index()
    {
        return View(_roleManager.Roles.ToList());
    }
    
    
    [SuppressMessage("ReSharper.DPA", "DPA0006: Large number of DB commands", MessageId = "count: 53")]
    public async Task<IActionResult> UsersInRole(string Role, int? CollegeId)
    {
        TempData["User"] = Role;
        var allUsers = await _userManager.Users.Include(u=>u.College).ToListAsync(); // Fetch all users from the database
        
        var usersWithRole = allUsers
            .Where(u => _userManager.IsInRoleAsync(u, Role).Result)
            .ToList();
        if (CollegeId is not null && CollegeId > 0)
        {
            usersWithRole = usersWithRole.Where(c => c.CollegeId == CollegeId).ToList();
        }
        return View(usersWithRole);
    }
    
    #region Add Role

    [HttpGet]
    public IActionResult AddRole()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddRole(RoleViewModel Role)
    {
        if (ModelState.IsValid)
        {
            var role = new IdentityRole();
            role.Name = Role.Name;
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        }

        return View(Role);
    }


    [HttpGet]
    public async Task<IActionResult> Delete(string Id, string OldRole)
    {
        TempData["Role"] = OldRole;
        return View(await _userManager.FindByIdAsync(Id));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(ApplicationUser u, string OldRole)
    {
        _context.Enrollments.RemoveRange(_context.Enrollments.Where(e => e.StudentId == u.Id).ToList());
        await _userManager.DeleteAsync(await _userManager.FindByIdAsync(u.Id));
        return RedirectToAction("UsersInRole", new { Role = OldRole });
    }

    #endregion

}