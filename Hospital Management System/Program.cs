using Hospital_Management_System.DbContext;
using Hospital_Management_System.Models;
using Hospital_Management_System.Repositories;
using Hospital_Management_System.Repositories.Interfaces;
using Hospital_Management_System.Services;
using Hospital_Management_System.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddTransient<IDoctorServices, DoctorService>();
builder.Services.AddTransient<IRoomTypeServices, RoomTypeServices>();
builder.Services.AddTransient<IRoomServices, RoomServices>();

// Adding application repositories
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();

// Adding automapper configuration 

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
