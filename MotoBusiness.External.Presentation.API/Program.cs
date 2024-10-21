using MotoBusiness.External.Infrastructure.IoC;
using MotoBusiness.External.Presentation.API.Middleares.Swagger;
using MotoBusiness.External.Presentation.API.Middleares.Versioning;

namespace MotoBusiness.External.Presentation.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddLogging();
        builder.Services.AddSwaggerDocs();
        builder.Services.AddApiVersioningConfig();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddDependences(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerDocs();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

