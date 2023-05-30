namespace EmployeeService.Utils;

public interface IS3Service
{
    public Task<string> UploadFile(IFormFile formFile, string employeeId);
    public Task<string> GetObjectUrl(string employeeId);
}
