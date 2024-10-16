using CodeCampus.Infrastructure.Contexts;
using CodeCampus.Infrastructure.Entities;
using CodeCampus_WebApi.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterSwagger();

var connectionString = builder.Configuration["ConnectionStrings:SqlServer"];
var jwtSecret = builder.Configuration["Jwt:Secret"];
var apiKey = builder.Configuration["ApiKey"];
var adminApiKey = builder.Configuration["AdminApiKey"];

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<UserEntity, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
builder.Services.RegisterServices(builder.Configuration);

builder.Services.ConfigureJwtAuthentication(builder.Configuration);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Campus v1"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
