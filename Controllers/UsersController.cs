using Employee_Management_System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    public readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;
    public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;

    }
    public async Task<ActionResult> Index()
    {
        var users = await _context.Users.Include(x=>x.Role).ToListAsync();
        return View(users);
    }

    [HttpGet]
    public async Task<ActionResult> Create()
    {
        ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Create(UserViewModel model)
    {
        ApplicationUser user = new ApplicationUser();
        user.UserName = model.UserName;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.LastName = model.LastName;
        user.NationalId = model.NationalId;
        user.NormalizedEmail = model.UserName;
        user.Email = model.Email;
        user.EmailConfirmed = true;
        user.PhoneNumber = model.PhoneNumber;
        user.PhoneNumberConfirmed = true;
        user.CreatedById ="Bibek Ghimire";
        user.RoleId = model.RoleId;
        user.CreatedOn = DateTime.Now;
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View(model);
        }
             ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", model.RoleId);
    }
}
