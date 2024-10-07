using Application.Mappers;
using DotNetEnv;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Load environment variables from .env file
Env.Load();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//Application
builder.Services.AddServicesAndRepositories();
builder.Services.AddAutoMapper(typeof(DtoMappers));
//Security
builder.Services.AddSecurityServices(builder.Configuration);
//Db
builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
       sqlOptions => sqlOptions.MigrationsAssembly("Infrastructure")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
