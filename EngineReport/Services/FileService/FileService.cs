namespace EngineReport.Services.FileService
{
    public class FileService: IFileService
    {
        public async Task<IFormFile?> ReadFile(string filePath)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                if (File.Exists(filePath))
                {
                    // Read the file into a byte array
                    var fileBytes = await File.ReadAllBytesAsync(filePath);

                    // Create a Stream from the byte array
                    MemoryStream fileStream = new MemoryStream(fileBytes);

                    // Create an IFormFile instance
                    IFormFile file = new FormFile(fileStream, 0, fileStream.Length, "file", Path.GetFileName(filePath));

                    return file;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
