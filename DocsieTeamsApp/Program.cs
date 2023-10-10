using Docsie.Application.Services;
using Docsie.Data.Core.Repositories;
using Docsie.Data.Core;
using Docsie.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();

var azuresqlConnectionString = builder.Configuration.GetSection("AzureSQL:ConnectionString").Value;
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(azuresqlConnectionString, x => x.EnableRetryOnFailure()));


builder.Services.AddHttpClient<IDocsieService, DocsieService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(x => x.AddPolicy("CorsPolicy", build => { build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
