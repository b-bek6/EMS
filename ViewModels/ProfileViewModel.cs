using System.ComponentModel;

namespace Employee_Management_System;

public class ProfileViewModel
{
    public ICollection<SystemProfile> Profiles { get; set; }
    [DisplayName("Role")]
    public string RoleId { get; set; }
}
