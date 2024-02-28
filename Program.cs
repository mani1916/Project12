using Microsoft.EntityFrameworkCore;
using Project1.Configurations;
using Project1.Data;
using Project1.Models;
using Serilog;
using AutoMapper;
using Project1.Data.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
// Log.Logger = new LoggerConfiguration()
// .MinimumLevel.Information()
// .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
// .CreateLogger();

// builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<CollegeDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSwaggerGen();
// builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddTransient<IStudentRepository,StudentRepository>();
builder.Services.AddTransient<ICollegeRepository, CollegeRepository>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
