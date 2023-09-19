namespace EngineReport.Services.PdfGeneratorService
{
    public interface IPdfGeneratorService
    {
        string? Generate(int IdProcessReport, string template);
    }
}
