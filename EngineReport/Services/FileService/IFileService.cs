namespace EngineReport.Services.FileService
{
    public interface IFileService
    {
        Task<IFormFile?> ReadFile(string filePath);
    }
}
