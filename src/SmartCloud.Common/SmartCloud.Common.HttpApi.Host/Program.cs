using SmartCloud.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ReplaceConfiguration(builder.Configuration);
builder.Host.UseAutofac();

builder.Services.AddApplication<CommonHttpApiHostModule>();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.InitializeApplication();
app.Run();
