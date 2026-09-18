using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using OperationMatrix.Features.Products;
using OperationMatrix.Infrastructure.Postgres;

namespace OperationMatrix.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddScoped<CreateProductHandler>();                                                                                                                                                                                                                      
        services.AddScoped<GetProductByIdHandler>();
        services.AddScoped<UpdateProductHandler>();
        services.AddScoped<DeleteProductHandler>(); 

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

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(AppDbContext)));
        });

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
        
        return services;
    }
}