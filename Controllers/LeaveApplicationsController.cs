using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee_Management_System;
using Employee_Management_System.Data;
using System.Security.Claims;

namespace Employee_Management_System.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            // add awaiting status table in only index html
            var applicationDbContext = _context.LeaveApplications.Include(l => l.Duration).Include(l => l.Employee).Include(l => l.LeaveType).Include(l => l.Status);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ApprovedLeaveApplications()
        {
            var approvedStatus =  _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code =="Approved").FirstOrDefault();
            var applicationDbContext = _context.LeaveApplications
            .Include(l => l.Duration)
            .Include(l => l.Employee)
            .Include(l => l.LeaveType)
            .Include(l => l.Status)
            .Where(l => l.StatusId == approvedStatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> RejectedLeaveApplications()
        {
            var rejectedStatus =  _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code =="Rejected").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
            .Include(l => l.Duration)
            .Include(l => l.Employee)
            .Include(l => l.LeaveType)
            .Include(l => l.Status)
            .Where(l => l.StatusId == rejectedStatus!.Id);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }
        [HttpGet]
        public async Task<IActionResult> ApproveLeave(int? id)
        {
            var leaveApplication = await _context.LeaveApplications
                .Include(l=>l.Duration)
                .Include(l=> l.Employee)
                .Include(l=>l.LeaveType)
                .Include(l=>l.Status)
                .FirstOrDefaultAsync(m=>m.Id == id);

            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Duration", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }
        [HttpGet]
        public async Task<IActionResult> RejectLeave(int? id)
        {
            var leaveApplication = await _context.LeaveApplications
                .Include(l=>l.Duration)
                .Include(l=> l.Employee)
                .Include(l=>l.LeaveType)
                .Include(l=>l.Status)
                .FirstOrDefaultAsync(m=>m.Id == id);

            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Duration", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveLeave(int? id,LeaveApplication leave)
        {
            var approvedStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Approved").FirstOrDefault();
            var leaveApplication = await _context.LeaveApplications
                .Include(l=>l.Duration)
                .Include(l=> l.Employee)
                .Include(l=>l.LeaveType)
                .Include(l=>l.Status)
                .FirstOrDefaultAsync(m=>m.Id == leave.Id);

            if (leaveApplication == null)
            {
                return NotFound();
            }

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = "Bibek Ghimire";
            leaveApplication.StatusId = approvedStatus.Id;
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            _context.Update(leaveApplication);
            await _context.SaveChangesAsync(UserId);
            return RedirectToAction(nameof(Index));


            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Duration", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }
        [HttpPost]
        public async Task<IActionResult> RejectLeave(int? id,LeaveApplication leave)
        {
            var rejectStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovalStatus" && y.Code == "Rejected").FirstOrDefault();

            var leaveApplication = await _context.LeaveApplications
                .Include(l=>l.Duration)
                .Include(l=> l.Employee)
                .Include(l=>l.LeaveType)
                .Include(l=>l.Status)
                .FirstOrDefaultAsync(m=>m.Id == leave.Id);

            if (leaveApplication == null)
            {
                return NotFound();
            }

            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = "Bibek Ghimire";
            leaveApplication.StatusId = rejectStatus!.Id;
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            _context.Update(leaveApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Duration", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public IActionResult Create()
        {
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description");
            return View();
        }

        // POST: LeaveApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplication leaveApplication)
        {
            var pendingStatus = await _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefaultAsync();

            leaveApplication.CreatedOn = DateTime.Now;
            leaveApplication.CreatedById = "Bibek Ghimire";
            leaveApplication.StatusId = pendingStatus.Id;
            _context.Add(leaveApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Duration", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,NoOfDays,StartDate,EndDate,DurationId,LeaveTypeId,Attachment,Description,StatusId,ApprovedById,ApprovedOn,CreatedById,CreatedOn,ModifiedById,ModifiedOn")] LeaveApplication leaveApplication)
        {
             if (id != leaveApplication.Id)
            {
                return NotFound();
            }
                var pendingStatus = await _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Pending" && y.SystemCode.Code == "LeaveApprovalStatus").FirstOrDefaultAsync();
                try
                {
                    leaveApplication.ModifiedOn = DateTime.Now;
                    leaveApplication.ModifiedById = "Bibek Ghimire";
                    leaveApplication.StatusId = leaveApplication.Id;
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Duration", leaveApplication.DurationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveApplication.EmployeeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }


    }
}
