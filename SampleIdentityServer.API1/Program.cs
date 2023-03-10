using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
    opts => {
        opts.Authority = "https://localhost:5001"; //Accestoken? yay?nayalan kim yetkili kim ? privet key ile ?ifrelenmi? tokeni publiy key ile ??zecek
        opts.Audience = "resource_api1"; //bana bir token geldi?i zaman mutlaka bu alan olmal? olmaz ise gelen iste?i kabul etmem. bu ekstra bir g?venlik i?in kullan?l?yor.
}); //burada bir ?ema verece?iz bu authenticationu tutacak. istersem kendim de ""bear veririm istersem jwtberare kullanabiliri.

builder.Services.AddAuthorization(options => //Add policiy ekleyerek scope i?erisine girdik ve oradaki durumlar i?in yetki verdik art?k gelen token i?erisinde scope k?sm?nda yazan ney ise onu almas?n? istedik olu?turdu?umuz ?art ile bu i?i sa?lad?k.
{
    options.AddPolicy("ReadProduct", policy =>
    {
        policy.RequireClaim("scope", "api1.read");
    });
    options.AddPolicy("UpdateOrCreate", policy =>
    {
        policy.RequireClaim("scope", new[] { "api1.update", "api1.create" });
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
