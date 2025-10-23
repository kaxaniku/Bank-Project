using BankSystem.API.Extensions;
using BankSystem.Application;
using BankSystem.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddPresentation();

var app = builder.Build();

app.UseSwaggerUI(app.Environment);
app.UsePresentation();

await app.RunAsync();
