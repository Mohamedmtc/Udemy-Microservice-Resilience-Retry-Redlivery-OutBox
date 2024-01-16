using CardService;
using CardService.Consumer;
using Core.RabbitMq.ApiConfiguration;
using Core.RabbitMq.BusConfiguration;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using MassTransit.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


//builder.Services.AddProblemDetails().AddExceptionHandler<GlobalExceptionHandler>();


#region MassTransit
builder.Services.AddMassTransit(cfg =>
{
    // Add delayed message scheduler
    cfg.AddDelayedMessageScheduler();

    // Automatically discover and add consumers from the specified namespace
    cfg.AddConsumersFromNamespaceContaining<IConsumerRegiter>();

    // Retrieve RabbitMQ settings from configuration
    var rabbitMqSetting = builder.Configuration.GetSection(RabbitMqConfiguration.SectionName).Get<RabbitMqConfiguration>();

    // Configure the bus with RabbitMQ
    cfg.AddBus(provider => RabbitMqBus.ConfigureBusWebApi(provider, builder.Configuration));
});

//builder.Services.AddMassTransitHostedService();
#endregion


var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseStatusCodePages();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
