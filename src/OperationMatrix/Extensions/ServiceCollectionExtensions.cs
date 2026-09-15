using Microsoft.OpenApi;

namespace OperationMatrix.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "Operation Matrix",
                    Version = "v1",
                    Description = "OperationMatrix API",
                    Contact = new OpenApiContact
                    {
                        Name = "Telegram",
                        Url = new Uri("https://t.me/xpathofneo")
                    }
                };
                return Task.CompletedTask;
            });
        });
        
        return services;
    }
}