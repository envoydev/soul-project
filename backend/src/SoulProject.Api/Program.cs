using Carter;
using SoulProject.Api;
using SoulProject.Api.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPresentation();

    var app = builder.Build();

    var applicationSettings = app.Configuration.GetApplicationSettings();
    
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<IApplicationDbContextInitializer>();
        await initializer.InitialiseAsync();
        await initializer.SeedAsync();   
    }
        
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in app.DescribeApiVersions())
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                options.SwaggerEndpoint(url, description.GroupName.ToUpperInvariant());
            }
        });
    }

    app.UseHttpsRedirection();
    
    app.UseGlobalExceptionHandler();
    
    if (app.Environment.IsProduction())
    {
        app.UseCors(corsBuilder =>
        {
            corsBuilder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithOrigins(applicationSettings.RequestConfig.Cors)
                       .AllowCredentials();
        });   
    }
    
    app.UseJwtBearerAuthorization();
    
    app.MapCarter();

    app.Run();
    
    Environment.Exit(0);
}
catch (Exception exception)
{
    var message = $"{exception.Message}{Environment.NewLine}{Environment.NewLine}{exception.StackTrace ?? string.Empty}";
    Console.WriteLine(message);
    
    Environment.Exit(-1);
}