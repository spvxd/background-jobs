using background_jobs;
using background_jobs.Repository;
using background_jobs.Services;
using Hangfire;
using Hangfire.PostgreSql;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(connectionString)
);
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddRepositories();
builder.Services.AddHangfireServer();
builder.Services.AddCarService();
builder.Services.AddSingleton<IParserService, ParserService>(); 
builder.Services.AddHostedService<BackgroundJobService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.MapControllers();

app.Run();