using Microsoft.EntityFrameworkCore;
using SalesWebMVC;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Configure services to use ApplicationDbContext with MySQL
var connectionString = builder.Configuration.GetConnectionString("SalesWebMVCContext");
builder.Services.AddDbContext<SalesWebMVCContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
