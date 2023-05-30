using Amazon.DynamoDBv2.DataModel;

namespace DepartmentService.Models;

[DynamoDBTable("employee")]
public class Employee
{
    [DynamoDBHashKey("id")]
    public string EmployeeID { get; set; }
    [DynamoDBProperty("first_name")]
    public string FirstName { get; set; }
    [DynamoDBProperty("last_name")]
    public string LastName { get; set; }
}