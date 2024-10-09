using Microsoft.EntityFrameworkCore;
using WebApp.Application.Services;
using WebApp.Core.Interfaces.Repositories;
using WebApp.Core.Interfaces.Services;
using WebApp.DataAccess;
using WebApp.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                            .SetIsOriginAllowed(origin => true)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                      });
});

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserStatisticsRepository, UserStatisticsRepository>();
builder.Services.AddScoped<IUserStatisticsService, UserStatisticsService>();
builder.Services.AddScoped<IProcessingService, ProcessingService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddMemoryCache();
builder.Services.AddHostedService<StartupProcessingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
