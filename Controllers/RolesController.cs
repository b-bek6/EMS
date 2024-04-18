using Employee_Management_System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System;

public class RolesController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    public readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _context;
    public RolesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _context.Roles.ToListAsync();
        return View(roles);
    }
    [HttpGet]
    public async Task<ActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(RolesViewModel model)
    {
        IdentityRole role = new IdentityRole();
        role.Name = model.RoleName;


        var result = await _roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {
        // auto mapper
        var role = new RolesViewModel();
        var result = await _roleManager.FindByIdAsync(id);
        role.RoleName = result.Name;
        role.Id = result.Id;
        return View(role);
    }

    [HttpPost]
    public async Task<ActionResult> Edit(string id, RolesViewModel model)
    {
        var checkifexist = await _roleManager.RoleExistAsync(model.RoleName);
        if (!checkifexist)
        {
            var result = await _roleManager.FindByIdAsync(id);
            result.Name = model.RoleName;


            var finalResult = await _roleManager.UpdateAsync(result);

            if (finalResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        return View(model);
    }

}
