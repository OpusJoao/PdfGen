global using Microsoft.EntityFrameworkCore;

global using Singulare.Models;
global using Singulare.Data;

using Singulare.Messaging;
using Singulare.Services.ProcessReportService;
using Singulare.Services.QuoteService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageBusService, RabbitMqService>();
builder.Services.AddScoped<IProcessReportService, ProcessReportService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddDbContext<DataContext>(ServiceLifetime.Transient);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
