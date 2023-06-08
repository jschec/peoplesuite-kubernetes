using System.ComponentModel.DataAnnotations;
namespace EmployeeService.Models;

public class EmployeeCreate
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [RegularExpression(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$", 
        ErrorMessage = "Date should be in YYYY-MM-DD format")]
    public String StartDate { get; set; }
    [RegularExpression(@"^[A-Z]{2}$", 
        ErrorMessage = "Country must be 2 digit ISO-3166 code")]
    public string Country { get; set; }
    public string DepartmentId { get; set; }
    public string Title { get; set; }
    public string? ManagerId { get; set; }
    public string? ManagerName { get; set; }
}