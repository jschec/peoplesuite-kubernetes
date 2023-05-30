using Amazon.DynamoDBv2.DataModel;

namespace DepartmentService.Models;

[DynamoDBTable("department")]
public class Department
{
    [DynamoDBHashKey("id")]
    public string DepartmentID { get; set; }
    [DynamoDBProperty("cost_center")]
    public int CostCenter { get; set; }
    [DynamoDBProperty("parent_department_id")]
    public string ParentDepartmentId { get; set; }
}