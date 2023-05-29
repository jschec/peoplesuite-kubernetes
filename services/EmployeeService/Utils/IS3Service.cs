namespace EmployeeService.Utils;

public interface IS3Service
{
    public Task<string> UploadFile(IFormFile formFile);
    public Task<string> GetObjectUrl(string objectBucket, string objectKey);
}
