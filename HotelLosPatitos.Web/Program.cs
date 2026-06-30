using HotelLosPatitos.DataAccess;
using Microsoft.EntityFrameworkCore;
using HotelLosPatitos.DataAccess.Repositories;
using HotelLosPatitos.Business.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IHabitacionRepository, HabitacionRepository>();
builder.Services.AddScoped<IHabitacionService, HabitacionService>();
builder.Services.AddScoped<IReservacionRepository, ReservacionRepository>();
builder.Services.AddScoped<IReservacionService, ReservacionService>();


builder.Services.AddDbContext<ReservacionesDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ReservacionesConnection")));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
