using Microsoft.EntityFrameworkCore;
using SolderPasteUsage.Data;
using SolderPasteUsage.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout =
        TimeSpan.FromHours(4);
});

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString(
                "DefaultConnection"
            )
        )
);

builder.Services.AddScoped<AutoOrderService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Account}/{action=Login}/{id?}"
);

app.Run();