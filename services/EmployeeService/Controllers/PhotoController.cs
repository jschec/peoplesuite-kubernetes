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
        Employee record = await _dbContext
            .LoadAsync<Employee>(employeeId);
        
        if (record == null) return NotFound();
        
        string objectUrl = await _storageContext.GetObjectUrl(
            record.PhotoObjectBucket, record.PhotoObjectKey);
        
        return Ok(new { path = objectUrl });
    }
    
    [HttpPost("{employeeId}/photo")]
    public async Task<IActionResult> UploadObject(
        string employeeId, [FromForm] IFormFile file)
    {
        var record = await _dbContext.LoadAsync<Employee>(employeeId);

        if (record == null)
        {
            return NotFound();
        }
        
        string objectUri = await _storageContext.UploadFile(file);
        // Remove S3 URI prefix and split by '/'
        string[] parsedUri = objectUri[4..].Split('/');
        
        record.PhotoObjectBucket = parsedUri[0];
        record.PhotoObjectKey = string.Join("/", parsedUri[1..]);

        await _dbContext.SaveAsync(record);

        return Ok(record);
    }
}