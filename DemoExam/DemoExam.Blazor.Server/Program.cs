using DemoExam.Blazor.Server.Extensions;
using DemoExam.Blazor.Server.Mapping;
using DemoExam.Blazor.Server.Middlewares;
using DemoExam.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();
    

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddJwtAuthentication()
    .AddInfrastructure()
    .AddServices()
    .AddAutoMapper(typeof(AutoMapperProfile))
    .AddCors(options => options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyHeader();
    }));

builder.Host.UseSerilog();

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseGlobalExceptionCatcher();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.ConfigureSwagger();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();