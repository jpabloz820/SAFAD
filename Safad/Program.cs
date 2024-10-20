using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Safad.Repositories;
using Safad.Interfaces;
using Safad.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services SQL
builder.Services.AddDbContext<SafadDBContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});
// Add services of repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserCoachRepository, UserCoachRepository>();


builder.Services.AddScoped<IUserAthleteRepository, UserAthleteRepository>();

builder.Services.AddScoped<IProfesionalRepository, RepositoryProfesional>();

// Add services of cookies for authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
