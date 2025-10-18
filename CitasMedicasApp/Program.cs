using CitasMedicasApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

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
    name: "departamentos",
    pattern: "Departamentos",
    defaults: new { controller = "Departamentos", action = "Index" });

app.MapControllerRoute(
    name: "servicios",
    pattern: "Servicios",
    defaults: new { controller = "Servicios", action = "Index" });

app.MapControllerRoute(
    name: "doctores",
    pattern: "Doctores",
    defaults: new { controller = "Doctores", action = "Index" });

app.MapControllerRoute(
    name: "maspaginas",
    pattern: "MasPaginas",
    defaults: new { controller = "MasPaginas", action = "Index" });

app.MapControllerRoute(
    name: "contacto",
    pattern: "Contacto",
    defaults: new { controller = "Contacto", action = "Index" });

app.MapControllerRoute(
    name: "citas",
    pattern: "Citas",
    defaults: new { controller = "Citas", action = "Index" });

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Citas}/{action=Index}/{id?}");

app.Run();
