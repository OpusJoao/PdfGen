using Amazon.S3;
using Amazon.S3.Model;
using EngineReport.DTOs;
using EngineReport.Services.FileService;
using Microsoft.AspNetCore.Mvc;

namespace EngineReport.Services.UploadService
{
    public class UploadAWSService: IUploadService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IFileService _fileService;
        private const string _bucketName = "jofdev";
        public UploadAWSService(IAmazonS3 s3Client, IFileService fileService)
        {
            _s3Client = s3Client;
            _fileService = fileService;
        }

        public async Task<S3ObjectDto?> UploadFileAsync(string filePath, string? prefix)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(_bucketName);
            if (!bucketExists) return null;

            //upload
            var file = await _fileService.ReadFile(filePath);
            var request = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);

            //get
            var requestGet = new ListObjectsV2Request()
            {
                BucketName = _bucketName,
                Prefix = prefix
            };
            var result = await _s3Client.ListObjectsV2Async(requestGet);
            var s3Objects = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = _bucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };
                return new S3ObjectDto()
                {
                    Name = s.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                };
            });

            return (S3ObjectDto?) s3Objects;
        }

    }
}
