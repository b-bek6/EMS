using Microsoft.AspNetCore.Identity;

namespace Employee_Management_System;

public class RoleProfile
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public SystemProfile Task { get; set; }
    public string RoleId { get; set; }
    public IdentityRole Role { get; set; }


}
