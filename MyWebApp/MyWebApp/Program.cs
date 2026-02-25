using MyWebApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);// congigures the web application builder with the specified command-line arguments. This sets up the necessary services and configurations for the application to run.

// Add services to the container.
builder.Services.AddControllersWithViews();// registers the MVC services in the dependency injection container, enabling support for controllers and views in the application.

// Configure Entity Framework Core to use SQL Server as the database provider. It retrieves the connection string named "DefaultConnection" from the application's configuration settings and uses it to establish a connection to the SQL Server database.
builder.Services.AddDbContext<MyAppContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();// builds the web application based on the configured services and settings. This creates an instance of the application that can be run to handle incoming HTTP requests.

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
