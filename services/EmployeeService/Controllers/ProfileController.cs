using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

using EmployeeService.Models;

namespace EmployeeService.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{

    private readonly IDynamoDBContext _context;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(
        IDynamoDBContext context, ILogger<EmployeeController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetRecordById(string employeeId)
    {
        var student = await _context.LoadAsync<Employee>(employeeId);
        if (student == null) return NotFound();
        return Ok(student);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRecords()
    {
        var record = await _context.ScanAsync<Employee>(default)
            .GetRemainingAsync();
        
        return Ok(record);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRecord([FromBody] Employee employee)
    {
        var record = await _context.LoadAsync<Employee>(employee.Id);
        
        if (record != null)
        {
            return BadRequest($"Employee with Id {employee.Id} Already Exists");
        }
        
        await _context.SaveAsync(employee);
        return Ok(employee);
    }
}