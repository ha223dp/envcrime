using EnviromentCrime.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IerrandsRepository, EfDatabaseRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DBInitializer.EnsurePopulated(services);
    IdentityInitializer.EnsurePopulated(services).Wait();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
