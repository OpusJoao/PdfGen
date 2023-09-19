using EngineReport.DTOs;

namespace EngineReport.Services.UploadService
{
    public interface IUploadService
    {
        Task<S3ObjectDto?> UploadFileAsync(string filePath, string? prefix);
    }
}
