namespace EmployeeService.Models;

public class EmployeeCreate
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime StartDate { get; set; }
    public string Country { get; set; }
    public string DepartmentName { get; set; }
    public string ManagerId { get; set; }
    public string ManagerName { get; set; }
}