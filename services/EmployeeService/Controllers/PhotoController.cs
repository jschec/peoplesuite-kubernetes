using EmployeeService.Utils;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotoController : ControllerBase
{

    private readonly IS3Service _context;
    private readonly ILogger<PhotoController> _logger;

    public PhotoController(IS3Service context, ILogger<PhotoController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        var result = await _context.UploadFile(uploadFile.File);
        return Ok(new
        {
            path = result
        });
    }
}