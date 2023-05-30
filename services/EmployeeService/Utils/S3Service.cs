using Amazon.S3;
using Amazon.S3.Model;

using EmployeeService.Models;

namespace EmployeeService.Utils
{
    public class S3Service : IS3Service
    {
        // The client for interfacing with the storage service
        private readonly IAmazonS3 _s3Client;
        // The name of the bucket to storage objects within
        private readonly string _bucketName;
        // The key prefix to assign to uploaded objects
        private readonly string _objectPrefix;
        
        /// <summary>
        /// Constructor for the S3Service class.
        /// </summary>
        /// <param name="storageConfig">Configuration of the storage service</param>
        public S3Service(StorageConfig storageConfig)
        {
            _s3Client = new AmazonS3Client();
            _bucketName = storageConfig.BucketName;
            _objectPrefix = storageConfig.KeyPrefix;
        }

        /// <summary>
        /// Uploads the specified file to the configured S3 application bucket.
        /// </summary>
        /// <param name="formFile">The file to upload to S3</param>
        /// <param name="employeeId">The identifier of the employee to store
        /// an object of
        /// </param>
        /// <returns>The S3 URI of the uploaded object</returns>
        public async Task<string> UploadFile(IFormFile formFile, string employeeId)
        {
            string objectKey = $"{_objectPrefix}/{employeeId}";
            
            Console.WriteLine(_bucketName);
            Console.WriteLine(objectKey);
            
            using (var stream = formFile.OpenReadStream())
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = objectKey,
                    InputStream = stream,
                    AutoCloseStream = true,
                    ContentType = formFile.ContentType,
                };
                
                await _s3Client.PutObjectAsync(putRequest);
                
                return $"s3://{_bucketName}/{objectKey}";
            }
        }

        /// <summary>
        /// Generates a pre-signed URL for accessing the object.
        /// </summary>
        /// <param name="employeeId">The identifier of the employee to retrieve
        /// an object of
        /// </param>
        /// <returns>The generated pre-signed URL</returns>
        public async Task<string> GetObjectUrl(string employeeId)
        {
            var preSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = $"{_objectPrefix}/{employeeId}",
                Expires = DateTime.Now.AddMinutes(60)
            };

            return _s3Client.GetPreSignedURL(preSignedUrlRequest);
        }
    }
}