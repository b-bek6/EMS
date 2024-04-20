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
        tasks.Profiles = await _context.SystemProfiles
            .Include("Children.Children.Children")
            .OrderBy(x => x.Order)
            .Where(x => x.ProfileId == null)
            .ToListAsync();
        return View(tasks);
        



    }
}
