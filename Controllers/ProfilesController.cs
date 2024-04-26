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
        var roles = await _context.Roles.OrderBy(x => x).ToListAsync();
        ViewBag.Roles = new SelectList(roles, "Id", "Name");
        var systemTasks = await _context.SystemProfiles
            .Include("Children.Children.Children")
            .OrderBy(x => x.Order)
            // .Where(x => x.ProfileId == null)
            .ToListAsync();

        ViewBag.Tasks = new SelectList(systemTasks, "Id", "Name");

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
    [HttpGet]
    public async Task<IActionResult> UserRights(string id)
    {
        var tasks = new ProfileViewModel();
        tasks.RoleId = id;
        tasks.Profiles = await _context.SystemProfiles
            .Include(s => s.Profile)
            .Include("Children.Children.Children")
            .OrderBy(x => x.Order)
            .ToListAsync();

        tasks.RolesRightsIds = await _context.RoleProfiles.Where(x => x.RoleId == id).Select(r => r.TaskId).ToListAsync();
        return View(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> UserGroupRights(string id, ProfileViewModel vm)
    {
        var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var allrights = await _context.RoleProfiles.Where(x => x.RoleId == id).ToListAsync();
        
        _context.RoleProfiles.RemoveRange(allrights);
        await _context.SaveChangesAsync(UserId);

        foreach (var taskId in vm.Ids)
        {
            var roles = new RoleProfile
            {
                TaskId = taskId,
                RoleId = id,
            };
            _context.RoleProfiles.Add(roles);
            await _context.SaveChangesAsync(UserId);
        }
        return RedirectToAction("Index");
    }
}
