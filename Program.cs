var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Basic HttpClient factory and a simple service registration
builder.Services.AddHttpClient();
builder.Services.AddScoped<WeatherDashboard.Services.WeatherService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Weather}/{action=Index}/{id?}");

app.Run();
