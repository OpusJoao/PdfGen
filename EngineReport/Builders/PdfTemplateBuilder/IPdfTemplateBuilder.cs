using Microsoft.AspNetCore.Routing.Template;
using System.Text;

namespace EngineReport.Builders
{
    public interface IPdfTemplateBuilder
    {


        void BuildAsHtml();

        string Generate();

        void BuildBodyHtml(List<Quote> data);

        void BuildHeaderHtml();

        void BuildFooterHtml();

        void BuildImageLogoHtml(string url);
    }
}
