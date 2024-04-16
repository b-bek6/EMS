namespace Employee_Management_System;

public class UserAcitvity
{
    public string? CreatedById { get; set; }
    public DateTime CreatedOn { get; set;}
    public string? ModifiedById { get; set;}
    public DateTime ModifiedOn { get; set; }
}
public class ApprovalAcitvity : UserAcitvity
{
    public string? ApprovedById { get; set; }
    public DateTime ApprovedOn { get; set;}
}

