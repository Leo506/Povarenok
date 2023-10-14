using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace DemoExam.Blazor.Server.Extensions;

public static class SwaggerExtensions
{
    private const string DefinitionName = "PovarenokAPI";
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc(DefinitionName, new OpenApiInfo
                {
                    Title = DefinitionName,
                    Version = "v1"
                });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
    }

    public static WebApplication ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint($"{DefinitionName}/swagger.json", DefinitionName));
        return app;
    }
}