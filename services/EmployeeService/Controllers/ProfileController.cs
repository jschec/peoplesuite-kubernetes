using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

using EmployeeService.Models;

namespace EmployeeService.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{

    private readonly IDynamoDBContext _dbContext;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(
        IDynamoDBContext dbContext, 
        ILogger<EmployeeController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    [HttpGet("{employeeId}/profile")]
    public async Task<IActionResult> GetRecordById(string employeeId)
    {
        var record = await _dbContext.LoadAsync<Employee>(employeeId);
        
        if (record == null) return NotFound();
        
        return Ok(record);
    }

    [HttpPost("{employeeId}/profile")]
    public async Task<IActionResult> CreateRecord([FromBody] Employee reqBody)
    {
        var record = await _dbContext.LoadAsync<Employee>(reqBody.Id);
        
        if (record != null)
        {
            return BadRequest($"Employee with Id {reqBody.Id} Already Exists");
        }
        
        await _dbContext.SaveAsync(reqBody);
        return Ok(record);
    }
}