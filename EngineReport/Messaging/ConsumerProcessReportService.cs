using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using EngineReport.DTOs;
using EngineReport.Services.ProcessReportService;
using System.Text;
using EngineReport.Services.PdfGeneratorService;
using EngineReport.Services.QuoteService;
using Microsoft.Extensions.Logging;
using EngineReport.Services.UploadService;
using EngineReport.Builders;

namespace EngineReport.Messaging
{
    public class ConsumerProcessReportService : BackgroundService
    {
        private IConnection _connection;

        private IModel _channel;

        private const string _exchange = "process-report";

        private const string _queue = "process-report";

        private const string _routingKeySubscribe = "process-report";

        private const int PENDING_STATUS = 1;
        private const int STARTED_STATUS = 2;
        private const int DONE_STATUS = 3;

        public IServiceProvider ServiceProvider { get; set; }
        public ConsumerProcessReportService(IServiceProvider serviceProvider)
        {

            ServiceProvider = serviceProvider;

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("quote-report-subscriber");
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_queue, true, false, false);
            _channel.ExchangeDeclare(_exchange, "direct", true, false);
            _channel.QueueBind(_queue, _exchange, _routingKeySubscribe);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                try
                {
                    var contentArray = args.Body.ToArray();
                    var contentString = Encoding.UTF8.GetString(contentArray);
                    var @event = JsonConvert.DeserializeObject<ProcessReports>(contentString);

                    Console.WriteLine($"Message ProcessReports received {contentString}");

                    if (@event is not null)
                    {
                        Process(@event).Wait();
                        _channel.BasicAck(args.DeliveryTag, false);
                    }
                    else
                    {
                        _channel.BasicNack(args.DeliveryTag, false, true);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    _channel.BasicNack(args.DeliveryTag, false, true);
                }

            };

            _channel.BasicConsume(_queue, false, consumer);

            return Task.CompletedTask;
        }

        public async Task<bool> Process(ProcessReports @event)
        {
            var scope = ServiceProvider.CreateScope();
            
            var processReportService = scope.ServiceProvider.GetService<IProcessReportService>();

            if(processReportService is null)
            {
                throw new Exception("Process ReportService was not found.");
            }

            await processReportService.UpdateProcessReportStatus(@event.Id, STARTED_STATUS);

            var filePath = await GenerateReport(@event);

            if(filePath is null)
            {
                throw new Exception("Report was not registered");
            }

            //var link = await UploadReport(filePath);

            var updatedLink = await processReportService.UpdateProcessReportLink(@event.Id, filePath);
            if (updatedLink is null)
                throw new Exception("Report was not updated");

            await processReportService.UpdateProcessReportStatus(@event.Id, DONE_STATUS);

            return true;
        }


        public async Task<string?> GenerateReport(ProcessReports processReport)
        {
            try
            {
                var scope = ServiceProvider.CreateScope();

                var pdfGenerator = scope.ServiceProvider.GetService<IPdfGeneratorService>();
                var quoteService = scope.ServiceProvider.GetService<IQuoteService>();
                var pdfTemplateBuilder = scope.ServiceProvider.GetService<IPdfTemplateBuilder>();

                if (pdfGenerator is null || quoteService is null)
                {
                    throw new Exception("Service was not found.");
                }

                var quotes = await quoteService.GetQuotes(processReport.StartDate, processReport.FinalDate);

                pdfTemplateBuilder.BuildAsHtml();
                pdfTemplateBuilder.BuildHeaderHtml();
                pdfTemplateBuilder.BuildImageLogoHtml("https://singulare.com.br/wp-content/uploads/2020/09/Singulare_priorita%CC%81rio-horizontal-1-e1601045949637.png");
                pdfTemplateBuilder.BuildBodyHtml(quotes);
                pdfTemplateBuilder.BuildFooterHtml();

                var html = pdfTemplateBuilder.Generate();

                var pathSaved = pdfGenerator.Generate(processReport.Id, html);

                return pathSaved;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<string?> UploadReport(string filePath)
        {
            try
            {
                var scope = ServiceProvider.CreateScope();

                var uploadAWSService = scope.ServiceProvider.GetService<IUploadService>();

                var fileS3 = await uploadAWSService.UploadFileAsync(filePath,null);

                return fileS3.PresignedUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
