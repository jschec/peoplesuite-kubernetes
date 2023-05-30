using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

using EmployeeService.Models;
using EmployeeService.Utils;

namespace EmployeeService.Controllers;

[ApiController]
[Route("employees")]
public class PhotoController : ControllerBase
{

    private readonly IS3Service _storageContext;
    private readonly IDynamoDBContext _dbContext;
    private readonly ILogger<PhotoController> _logger;

    public PhotoController(
        IDynamoDBContext dbContext,
        IS3Service storageContext, 
        ILogger<PhotoController> logger)
    {
        _dbContext = dbContext;
        _storageContext = storageContext;
        _logger = logger;
    }
    
    [HttpGet("{employeeId}/photo")]
    public async Task<IActionResult> DownloadObject(string employeeId)
    {
        Employee record = await _dbContext.LoadAsync<Employee>(employeeId);
        
        // Escape out if no matching record is current in the Database
        if (record == null) return NotFound();
        
        string objectUrl = await _storageContext.GetObjectUrl(employeeId);
        
        return Ok(new { path = objectUrl });
    }
    
    [HttpPost("{employeeId}/photo")]
    public async Task<IActionResult> UploadObject(
        string employeeId, [FromForm] IFormFile file)
    {
        var record = await _dbContext.LoadAsync<Employee>(employeeId);

        // Escape out if no matching record is current in the Database
        if (record == null) return NotFound();
        
        await _storageContext.UploadFile(file, employeeId);

        return Ok(record);
    }
}