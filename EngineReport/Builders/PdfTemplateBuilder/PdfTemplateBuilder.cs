using Microsoft.AspNetCore.Routing.Template;
using System.Text;

namespace EngineReport.Builders.PdfTemplateBuilder
{
    public class PdfTemplateBuilder: IPdfTemplateBuilder
    {
        private string? template;
        private StringBuilder response;
        public PdfTemplateBuilder() { 
            response = new StringBuilder();
        }

        public void BuildAsHtml()
        {
            template = "html";
        }

        public string Generate()
        {
            return response.ToString();
        }

        public void BuildImageLogoHtml(string url)
        {
            response.Append($"<img src=\"{url}\"></img>");
        }

        public void BuildBodyHtml(List<Quote> data)
        {
            if (data.Count == 0)
            {
                response.Append("<p>No data available</p>");
            }
            else
            {
                response.Append("<table style=\"border-collapse: collapse; width: 100%;margin-top: 16px;\">"); // Added CSS styling here

                response.Append("<tr style=\"background-color: #f2f2f2;\">"); // Added background color
                foreach (var propertyInfo in typeof(Quote).GetProperties())
                {
                    if (propertyInfo.Name != "Id")
                        response.Append("<th style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">" + propertyInfo.Name + "</th>"); // Added CSS styling here
                }
                response.Append("</tr>");

                foreach (var quote in data)
                {
                    response.Append("<tr>");
                    foreach (var propertyInfo in typeof(Quote).GetProperties())
                    {
                        var value = propertyInfo.GetValue(quote);
                        if (propertyInfo.Name != "Id")
                            response.Append("<td style=\"border: 1px solid #dddddd; text-align: left; padding: 8px;\">" + value + "</td>"); // Added CSS styling here
                    }
                    response.Append("</tr>");
                }

                response.Append("</table>");
            }
        }


        public void BuildHeaderHtml()
        {
            response.Append($"<!DOCTYPE html><html lang=\"pt-br\"><head><title>Report</title><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"></head><body>");
        }

        public void BuildFooterHtml()
        {
            response.Append("</body></html>");
        }
    }
}
