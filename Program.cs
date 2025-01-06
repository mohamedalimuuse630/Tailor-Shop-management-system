using Microsoft.EntityFrameworkCore;
using Tailor_shop.Data;
using Microsoft.AspNetCore.Identity;
using System;
using Tailor_shop.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection1")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<WebDbContext>();
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(op =>
{
    op.LoginPath = "/Account/Login";
});

builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapStaticAssets(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "Templete",
    pattern: "{controller=Home}/{action=Indexx}/{id?}")
    .WithStaticAssets();


app.MapRazorPages();
app.Run();
