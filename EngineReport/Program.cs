global using Microsoft.EntityFrameworkCore;

global using EngineReport.Models;
global using EngineReport.Data;
using EngineReport.Messaging;
using EngineReport.Services.ProcessReportService;
using EngineReport.Services.PdfGeneratorService;
using EngineReport.Services.QuoteService;
using Amazon.S3;
using EngineReport.Services.UploadService;
using EngineReport.Services.FileService;
using EngineReport.Builders;
using EngineReport.Builders.PdfTemplateBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(ServiceLifetime.Transient);
builder.Services.AddHostedService<ConsumerProcessReportService>();
builder.Services.AddScoped<IProcessReportService, ProcessReportService>();
builder.Services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IUploadService, UploadAWSService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPdfTemplateBuilder, PdfTemplateBuilder>();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
