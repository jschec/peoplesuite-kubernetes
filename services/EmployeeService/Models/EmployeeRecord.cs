using Amazon.DynamoDBv2.DataModel;

namespace EmployeeService.Models;

[DynamoDBTable("employee")]
public class EmployeeRecord
{
    [DynamoDBHashKey("id")]
    public string Id { get; set; }
    [DynamoDBProperty("first_name")]
    public string FirstName { get; set; }
    [DynamoDBProperty("last_name")]
    public string LastName { get; set; }
    [DynamoDBProperty("start_date")]
    public string StartDate { get; set; }
    [DynamoDBProperty("country")]
    public string Country { get; set; }
    [DynamoDBProperty("title")]
    public string Title { get; set; }
    [DynamoDBProperty("department_name")]
    public string? DepartmentName { get; set; }
    [DynamoDBProperty("manager_id")]
    public string ManagerId { get; set; }
    [DynamoDBProperty("manager_name")]
    public string? ManagerName { get; set; }
}