using Chirp.Core.Interfaces;
using Chirp.Infrastructure.Data;
using Chirp.Infrastructure.Entities;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Chirp.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using DotNetEnv;

// Load OAuth secrets
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Load database connection via configuration - from slides session 6
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

// Add EF Core Identity to the app
builder.Services.AddDefaultIdentity<Author>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false; // disables special character requirement
        options.Password.RequiredLength = 6;
    }
).AddEntityFrameworkStores<ChirpDBContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();

// Add OAuth to the App
builder.Services.AddAuthentication()
    .AddCookie()
    .AddGitHub(o =>
    {
        o.ClientId = Environment.GetEnvironmentVariable("AUTHENTICATION_GITHUB_CLIENTID")!;
        o.ClientSecret = Environment.GetEnvironmentVariable("AUTHENTICATION_GITHUB_CLIENTSECRET")!;
        o.CallbackPath = "/signin-github";
    });


var app = builder.Build();

//Creates a scope to retrieve the database context, ensures the database exists, and seeds the database with example data
//Code from https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-8.0&tabs=visual-studio-code#seed-the-database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ChirpDBContext>();
    context.Database.EnsureCreated(); 
    DbInitializer.SeedDatabase(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();