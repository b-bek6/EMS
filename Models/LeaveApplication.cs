using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System;

public class LeaveApplication : ApprovalAcitvity
{
    public int Id { get; set; }
    [Display(Name="Employee Name")]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    [Display(Name="No Of Leave Days")]
    public int NoOfDays { get; set; }
    [Display(Name="Start Date")]
    public DateTime StartDate { get; set; }
    [Display(Name="End Date")]
    public DateTime EndDate { get; set; }
    [Display(Name="Leave Duration")]
    public int DurationId { get; set; }
    public SystemCodeDetail Duration { get; set; }
    [Display(Name="Leave Type")]
    public int LeaveTypeId { get; set; }
    public LeaveType LeaveType { get; set; }
    public string? Attachment {get; set; }
    [Display(Name="Notes")]
    public string Description {get; set; }
    [Display(Name="Status")] 
    public int StatusId { get; set; }
    public SystemCodeDetail Status { get; set; }
    [Display(Name="Approval Notes")]
    public string? ApprovalNotes {get; set; }

}
