using BookShop1Asm.Interfaces;
using BookShop1Asm.Repositories;
using BookShop1Asm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using BookShop1Asm.Utility;

var modelbuilder = WebApplication.CreateBuilder(args);

// Add services to the container.
modelbuilder.Services.AddControllersWithViews();

modelbuilder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(modelbuilder.Configuration.GetConnectionString("DefaultConnection")));

modelbuilder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();

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
