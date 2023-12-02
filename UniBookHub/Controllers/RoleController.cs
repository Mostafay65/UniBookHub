using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniBookHub.Models;
using UniBookHub.ViewModels;
namespace UniBookHub.Controllers;

public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleController(RoleManager<IdentityRole> _roleManager,UserManager<ApplicationUser> userManager)
    {
        this._roleManager = _roleManager;
        _userManager = userManager;
    }
    // GET
    public IActionResult Index()
    {
        return View(_roleManager.Roles.ToList());
    }
    
    
    public async Task<IActionResult> UsersInRole(string Role)
    {
        TempData["User"] = Role;
        var allUsers = await _userManager.Users.Include(u=>u.College).ToListAsync(); // Fetch all users from the database
        
        var usersWithRole = allUsers
            .Where(u => _userManager.IsInRoleAsync(u, Role).Result)
            .ToList();
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


    public async Task<IActionResult> Delete(string Id, string OldRole)
    {
        await _userManager.DeleteAsync(await _userManager.FindByIdAsync(Id));
        return RedirectToAction("UsersInRole", new { Role = OldRole });
    }

    #endregion

}