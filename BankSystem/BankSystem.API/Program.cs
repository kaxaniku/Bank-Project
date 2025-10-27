using BankSystem.API.Extensions;
using BankSystem.Application;
using BankSystem.Application.MappingProfiles;
using BankSystem.Infrastructure;
using BankSystem.Infrastructure.MappingProfiles;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog();

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddPresentation();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<InfrastructureMappingProfile>();
    cfg.AddProfile<ApplicationMappingProfile>();
});

var app = builder.Build();

app.UseSwaggerUI(app.Environment);
app.UsePresentation();

await app.RunAsync();
