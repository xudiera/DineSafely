using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

// Removes server header
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextPool<ApplicationDbContext>(optionsBuilder => optionsBuilder
    .UseNpgsql(connectionString, options => options.EnableRetryOnFailure().EnableRetryOnFailure()));

builder.Services.AddMiniProfiler().AddEntityFramework();

builder.Services.AddRepositories();

var app = builder.Build();

app.UseResponseCompression();

// TODO: Do migrations properly.
// See: https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiniProfiler();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSecurityHeaders();

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
