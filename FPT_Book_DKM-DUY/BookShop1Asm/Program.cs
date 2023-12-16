using BookShop1Asm.Interfaces;
using BookShop1Asm.Repositories;
using BookShop1Asm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using BookShop1Asm.Utility;
using BookShop1Asm.Models;

var modelbuilder = WebApplication.CreateBuilder(args);

// Add services to the container.
modelbuilder.Services.AddControllersWithViews();

modelbuilder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(modelbuilder.Configuration.GetConnectionString("DefaultConnection")));

modelbuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();

//builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
modelbuilder.Services.AddTransient<IUnitOfWork, UnitOfWorkRepository>();
modelbuilder.Services.AddScoped<IEmailSender, EmailSender>();

modelbuilder.Services.AddRazorPages();

modelbuilder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = $"/Identity/Account/Login";
    option.LogoutPath = $"/Identity/Account/Logout";
    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

var app = modelbuilder.Build();
// Seeding the database with roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<AppDBContext>(); // Ensure this matches your actual DbContext class
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedRolesAsync(userManager, roleManager); // Ensure this method is implemented correctly
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

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

app.UseAuthentication();;
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
