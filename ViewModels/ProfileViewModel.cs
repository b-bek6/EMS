using System.ComponentModel;

namespace Employee_Management_System;

public class ProfileViewModel
{
    public ICollection<SystemProfile> Profiles { get; set; }
    public ICollection<int> RolesProfilesIds { get; set; }
    public int[] Ids { get; set; }
    [DisplayName("Role")]
    public string RoleId { get; set; }
    [DisplayName("System Task")]
    public int TaskId { get; set; }
}
