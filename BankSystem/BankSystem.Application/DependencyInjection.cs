using BankSystem.Application.Common.Behaviors;
using BankSystem.Application.Common.Interfaces.Services;
using BankSystem.Application.Common.Services;
using BankSystem.Application.Features.Auth.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BankSystem.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}