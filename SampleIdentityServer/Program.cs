using Microsoft.EntityFrameworkCore;
using SampleIdentityServer.UI;
using SampleIdentityServer.UI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
});

builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(Config.GetApiResource())
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddTestUsers(Config.GetUsers().ToList())
    .AddDeveloperSigningCredential(); //bu kod public key ile privet key oluþturur. eðerki production çýkacaksan da addsigningCredantial kullanýlýr.


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
app.UseIdentityServer(); //miidlewier olarakta eklememiz gerek.
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
