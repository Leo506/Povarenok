using DemoExam.Blazor.Server.Extensions;
using DemoExam.Blazor.Server.Mapping;
using DemoExam.Blazor.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddJwtAuthentication()
    .AddRepositories()
    .AddServices()
    .AddAutoMapper(typeof(AutoMapperProfile))
    .AddCors(options => options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyHeader();
    }));


var app = builder.Build();
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