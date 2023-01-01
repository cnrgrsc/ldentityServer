using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using SampleIdentityServer.Client1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IApiResourceHttpClient, ApiResourceHttpClient>();


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies"; //bu þemalar uygulamada birden fazla cookie mekanizmasý olabilir. örneðpik bir kullanýcý bir bayi gibi. bunlarý ayýrmak içinn default schme kullanýlýr.
    opt.DefaultChallengeScheme = "oidc";

}).AddCookie("Cookies", opts =>
{
    opts.AccessDeniedPath = "/Home/AccessDenied"; //yetki yoks açýalcka hata sayfasý. home da yapýlma sebebi ileride user bir attribute ile role verilirse ve yetki yok ise karþýdaki kiþi onuda göremez görmesi için home da yapýldý.
}).AddOpenIdConnect("oidc",opts => //addcokie schem vardik çünkü benden schme bekliyor overloadlarýnda mevcut 
{
    opts.SignInScheme = "Cookies";
    opts.Authority = "https://localhost:5001"; //tokeni daðatan kim
    opts.ClientId= "Client1-Mvc";
    opts.ClientSecret = "secret";
    opts.ResponseType = "code id_token";
    opts.GetClaimsFromUserInfoEndpoint= true; //bu prop userinfo sýnýfýna set olacak ve bize kullanýcýnýn adý syoadý gibi bilgileri verecek
    opts.SaveTokens = true; //authentication bilgilerini cookie üzerinde kaydeder 
    opts.Scope.Add("api1.read"); //ben bu isteði yaptýðýmda api.read varsa verecek
    opts.Scope.Add("offline_access");
    opts.Scope.Add("CountryAndCity");
    opts.Scope.Add("Roles");

    opts.ClaimActions.MapUniqueJsonKey("country", "country");
    opts.ClaimActions.MapUniqueJsonKey("city", "city");
    opts.ClaimActions.MapUniqueJsonKey("role", "role");

    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        RoleClaimType = "role"
    };
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
