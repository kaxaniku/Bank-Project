using BankSystem.API.Middleware;

namespace BankSystem.API.Extensions;

public static class ApplicationPipelineExtensions
{
    public static IApplicationBuilder UsePresentation(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    public static IApplicationBuilder UseSwaggerUI(this WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankSystem API v1");
                c.RoutePrefix = "swagger";
            });
        }

        return app;
    }
}
