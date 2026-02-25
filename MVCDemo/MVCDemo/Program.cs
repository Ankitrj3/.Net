var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();//this line congiure the use of static files in the application, allowing it to serve static assets such as images, CSS files, and JavaScript files from the wwwroot folder or other specified locations. 
app.UseExceptionHandler("/Home/Error");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
