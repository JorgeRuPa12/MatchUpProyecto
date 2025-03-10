using MatchUpProyecto.Data;
using MatchUpProyecto.Repositories;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreSession.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("SqlMatchUpCasa");


builder.Services.AddSingleton<HelperSessionContextAccesor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<RepositoryDeportes>();
builder.Services.AddTransient<RepositoryPachanga>();
builder.Services.AddTransient<RepositoryUsers>();
builder.Services.AddTransient<RepositoryEquipos>();
builder.Services.AddDbContext<MatchUpContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

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

app.MapStaticAssets();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
