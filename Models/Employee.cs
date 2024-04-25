namespace Employee_Management_System;

public class Employee : UserAcitvity
{
    public int Id { get; set; }
    public string EmpNo { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {MiddleName} {LastName}";
    public int PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int DesignationId { get; set; }
    public Designation Designation { get; set; }
}
