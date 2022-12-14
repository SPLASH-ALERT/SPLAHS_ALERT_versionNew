using Microsoft.EntityFrameworkCore;
using splash_alert.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using splash_alert.Servicio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext  <splash_dataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));
//builder.Services.AddDbContext<splash_dataContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/LoginController1/Index";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    option.AccessDeniedPath = "/Home/Privacy";
});


builder.Services.AddScoped<IServicioApi, Servicio_Apis>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginController1}/{action=Index}/{id?}");

app.Run();
