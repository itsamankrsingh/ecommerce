using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.Interface;
using ECommerce.DataAccess.Repository;
using ECommerce.Identity.Common;
using ECommerce.Identity.Data;
using ECommerce.Identity.Interface;
using ECommerce.Identity.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddRazorPages();

// Connection string for PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=yourpassword";

var identityConnString = builder.Configuration.GetConnectionString("IdentityDbConnection")
                      ?? "Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=yourpassword";

// Register ApplicationDbContext with Npgsql provider
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<IdentityAppDbContext>(options =>
    options.UseNpgsql(identityConnString));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityAppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = $"/Identity/Account/Login";
    opt.LogoutPath = $"/Identity/Account/Logout";
    opt.AccessDeniedPath = $"/Identity/Account/AccessDenied";
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
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
