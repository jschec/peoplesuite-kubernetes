using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;

using DepartmentService.Models;

namespace DepartmentService.Controllers;

[ApiController]
[Route("peoplesuite/apis/departments")]
public class DepartmentController : ControllerBase
{

    private readonly IDynamoDBContext _dbContext;
    private readonly ILogger<DepartmentController> _logger;

    public DepartmentController(
        IDynamoDBContext dbContext, 
        ILogger<DepartmentController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRecords()
    {
        IEnumerable<Department> records = await _dbContext
            .ScanAsync<Department>(default)
            .GetRemainingAsync();
        
        return Ok(records);
    }

    [HttpGet("{departmentId}/employees")]
    public async Task<IActionResult> CreateRecord(string departmentId)
    {
        IEnumerable<ScanCondition> conditions = new[]
        {
            new ScanCondition("DepartmentId", ScanOperator.Equal, departmentId)
        };
        
        IEnumerable<Employee> results = await _dbContext
            .ScanAsync<Employee>(conditions)
            .GetRemainingAsync();

        List<EmployeeMinified> processedResults = new List<EmployeeMinified>();

        foreach (var result in results)
        {
            processedResults.Add(new EmployeeMinified()
            {
                EmployeeID = result.Id,
                EmployeeName = result.FirstName + " " + result.LastName,
                EmployeeProfileURL = $"/peoplesuite/apis/employees/{result.Id}/profile"
            });
        }

        return Ok(processedResults);
    }
}