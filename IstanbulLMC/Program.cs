using IstanbulLMC.Models;
using IstanbulLMC.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<AppUser, IdentityRole>()
        .AddEntityFrameworkStores<lmcTourismContext>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);
builder.Services.AddDbContext<lmcTourismContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConn"].ToString());
    options.EnableSensitiveDataLogging();
});

builder.Services.AddAuthentication("admin")
        .AddCookie("admin", options =>
        {
            options.LoginPath = "/Admin/Admin/Login/"; // Set the login path

        });

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
            name: "Admin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
        );

    endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//Seed database
AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

app.Run();
