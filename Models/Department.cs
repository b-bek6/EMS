using System.ComponentModel.DataAnnotations;

namespace Employee_Management_System;

public class Department : UserAcitvity
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

}
