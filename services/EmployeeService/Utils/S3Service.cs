using Amazon.S3;
using Amazon.S3.Model;

using EmployeeService.Models;

namespace EmployeeService.Utils
{
    public class S3Service : IS3Service
    {
        private IAmazonS3 _s3Client;
        private readonly string _bucketName;
        private readonly string _objectPrefix;
        
        public S3Service(StorageConfig storageConfig)
        {
            _s3Client = new AmazonS3Client();;
            _bucketName = storageConfig.BucketName;
            _objectPrefix = storageConfig.KeyPrefix;
        }

        /// <summary>
        /// Uploads the specified file to the configured S3 application bucket.
        /// </summary>
        /// <param name="formFile">The file to upload to S3</param>
        /// <returns>The S3 URI of the uploaded object</returns>
        public async Task<string> UploadFile(IFormFile formFile)
        {
            string objectKey = $"{_objectPrefix}/{formFile.FileName}";
            
            using (var stream = formFile.OpenReadStream())
            {
                var putRequest = new PutObjectRequest
                {
                    Key = objectKey,
                    BucketName = _bucketName,
                    InputStream = stream,
                    AutoCloseStream = true,
                    ContentType = formFile.ContentType
                };
                
                await _s3Client.PutObjectAsync(putRequest);
                
                return $"s3://{_bucketName}/{objectKey}";
            }
        }

        public async Task<string> GetObjectUrl(string objectBucket, string objectKey)
        {
            var preSignedUrlRequest = new GetPreSignedUrlRequest
            {
                BucketName = objectBucket,
                Key = objectKey,
                Expires = DateTime.Now.AddMinutes(60)
            };

            return _s3Client.GetPreSignedURL(preSignedUrlRequest);
        }
    }
}