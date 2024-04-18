using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System;

public class UserViewModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }  
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    [Required]
    [StringLength(1000, ErrorMessage ="The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string Address { get; set; }
    public string UserName { get; set; }
    public string? NationalId { get; set; }
    public string? FullName  => $"{FirstName} {MiddleName} {LastName}";
    public string? RoleId { get; set; }
}
