using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniBookHub.Data;
using UniBookHub.Models;
using UniBookHub.ViewModels;

namespace UniBookHub.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment webHostEnvironment;

    public AccountController(UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager,
        RoleManager<IdentityRole> _roleManager,
        AppDbContext _context)
    {
        this._userManager = _userManager;
        this._signInManager = _signInManager;
        this._roleManager = _roleManager;
        this._context = _context;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginViewModel userLogin)
    {
        if (ModelState.IsValid)
        {
            var NewUser = await _userManager.FindByNameAsync(userLogin.UserName);
            if (NewUser is not null)
            {
                var result = await _userManager.CheckPasswordAsync(NewUser, userLogin.Password);
                if (result)
                {
                    if (!NewUser.Accessed)
                    {
                        NewUser.Accessed = true;
                        await _userManager.UpdateAsync(NewUser);
                    }
                    await _signInManager.SignInAsync(NewUser, userLogin.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Icorrect Password Or UserName");
        }

        return View(userLogin);
    }


    [HttpGet]
    public IActionResult Register(string Role)
    {
        TempData["User"] = Role;
        TempData["Colleges"] = new SelectList(_context.Colleges.ToList(), "ID", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegisterViewModel userRegister, string Role)
    {
        if (ModelState.IsValid && userRegister.CollegeId!=0)
        {
            var NewUser = new ApplicationUser();
            NewUser.Name = userRegister.Name;
            NewUser.UserName = userRegister.UserName;
            NewUser.Password = userRegister.Password;
            NewUser.CollegeId = userRegister.CollegeId;
            var result = await _userManager.CreateAsync(NewUser, userRegister.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(NewUser, Role);
                return RedirectToAction("UsersInRole", "Role",new { Role = Role });
            }

            foreach (var Error in result.Errors)
                ModelState.AddModelError("", Error.Description);
        }

        if (userRegister.CollegeId==0)
        {
            ModelState.AddModelError("CollegeId","Please Select College");
        }
        TempData["User"] = Role;
        TempData["Colleges"] = new SelectList(_context.Colleges.ToList(), "ID", "Name");
        return View(userRegister);
    }

    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
}