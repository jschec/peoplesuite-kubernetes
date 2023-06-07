using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

using EmployeeService.Models;
using EmployeeService.Utils;

namespace EmployeeService.Controllers;

[ApiController]
[Route("peoplesuite/apis/employees")]
public class EmployeeController : ControllerBase
{

    private readonly IS3Service _storageContext;
    private readonly IDynamoDBContext _dbContext;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(
        IDynamoDBContext dbContext,
        IS3Service storageContext, 
        ILogger<EmployeeController> logger)
    {
        _dbContext = dbContext;
        _storageContext = storageContext;
        _logger = logger;
    }
    
    [HttpGet("{employeeId}/photo")]
    public async Task<IActionResult> DownloadObject(string employeeId)
    {
        EmployeeRecord record = await _dbContext
            .LoadAsync<EmployeeRecord>(employeeId);
        
        // Escape out if no matching record is current in the Database
        if (record == null) return NotFound();
        
        string objectUrl = await _storageContext.GetObjectUrl(employeeId);
        
        return Ok(new { path = objectUrl });
    }
    
    [HttpPost("{employeeId}/photo")]
    public async Task<IActionResult> UploadObject(
        string employeeId, [FromForm] IFormFile file)
    {
        var record = await _dbContext.LoadAsync<EmployeeRecord>(employeeId);

        // Escape out if no matching record is current in the Database
        if (record == null) return NotFound();
        
        await _storageContext.UploadFile(file, employeeId);

        return Ok(record);
    }
    
    [HttpGet("{employeeId}/profile")]
    public async Task<IActionResult> GetRecordById(string employeeId)
    {
        var record = await _dbContext.LoadAsync<EmployeeRecord>(employeeId);
        
        if (record == null) return NotFound();
        
        return Ok(record);
    }

    [HttpPost("{employeeId}/profile")]
    public async Task<IActionResult> CreateRecord(string employeeId, [FromBody] EmployeeCreate reqBody)
    {
        var record = await _dbContext.LoadAsync<EmployeeRecord>(employeeId);
        
        if (record != null)
        {
            return BadRequest($"Employee with Id {employeeId} Already Exists");
        }

        EmployeeRecord newEmployee = new EmployeeRecord()
        {
            Id = employeeId,
            Country = reqBody.Country,
            DepartmentName = reqBody.DepartmentName,
            FirstName = reqBody.FirstName,
            LastName = reqBody.LastName,
            ManagerId = reqBody.ManagerId,
            StartDate = reqBody.StartDate
        };

        await _dbContext.SaveAsync(newEmployee);
        return Ok(record);
    }
}