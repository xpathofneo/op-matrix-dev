namespace OperationMatrix.Extensions;

public static class WebApplicationExtensions
{ 
    public static WebApplication UseApplicationPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "API v1");
            });
        }

        app.MapControllers();
        
        return app;
    }
}