using Business.DependencyResolvers.Microsoft;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebUI.BackgroundServices;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.Cookie.Name = "cryptodash.auth";
        options.ReturnUrlParameter = "returnUrl";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddScoped<IAuthApiService, AuthApiService>();


builder.Services.AddHostedService<CryptoWorker>();

builder.Host.UseSerilog();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
CheckDbContextMigrations();
app.Run();


//Veritabanýnýn migrationlarýný kontrol eder ve yoksa oluþturur.
void CheckDbContextMigrations()
{
    using var scope = app.Services.CreateScope();

    var hasMigration = scope.ServiceProvider.GetRequiredService<CryptoDashContext>().Database.GetPendingMigrations().Any();
    if (hasMigration == true)
    {
        scope.ServiceProvider.GetRequiredService<CryptoDashContext>().Database.Migrate();
    }
}