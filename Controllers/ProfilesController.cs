using System.Security.Claims;
using Employee_Management_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System;

public class ProfilesController : Controller
{
    private readonly ApplicationDbContext _context;
    public ProfilesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = new ProfileViewModel();
        var roles = await _context.Roles.OrderBy(x=>x).ToListAsync();
        ViewBag.Roles = new SelectList(roles,"Id","Name");
        ViewBag.Tasks = await _context.SystemProfiles
            .Include("Children.Children.Children")
            .OrderBy(x => x.Order)
            .Where(x => x.ProfileId == null)
            .ToListAsync();
        return View(tasks);
    }

    public async Task<ActionResult> AssignRights(ProfileViewModel vm)
    {
        var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        var roles = new RoleProfile
        {
            TaskId = vm.TaskId,
            RoleId = vm.RoleId
        };
        _context.RoleProfiles.Add(roles);
        await _context.SaveChangesAsync(UserId);
        return RedirectToAction("Index");
    }
}
