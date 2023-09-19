
namespace EngineReport.Services.PdfGeneratorService
{
    public class PdfGeneratorService: IPdfGeneratorService
    {

        public string? Generate (int IdProcessReport, string template)
        {
            try
            {
                ChromePdfRenderer renderer = new ChromePdfRenderer();
                PdfDocument pdf = renderer.RenderHtmlAsPdf(template);
                var now = (DateTimeOffset)DateTime.UtcNow;
                string filename = $".\\Temp\\{IdProcessReport}_{now.ToString("yyyyMMddHHmmssfff")}.pdf";
                pdf.SaveAs(filename);
                return filename;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
