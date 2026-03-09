using ClassLibDbFirstApproch.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication.Repository;
using WebApplication.Services;
var builder = global::Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentPortalDbContext>(options =>
    options.UseSqlServer("Data Source=localhost;Initial Catalog=StudentPortalDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"));
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<IStudentService, StudentService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
