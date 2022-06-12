using eTicketing.Data;
using eTicketing.Data.Cart;
using eTicketing.Data.Services;
using eTicketing.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//Services Configuration

//LifeTime Explanation

//Singleton.  Single Instance is created
//Scoped		Once per request within scope
//Transient	Created each time once requested
builder.Services.AddScoped<IActorInterface, ActorService>();
builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IOrdersService,OrdersService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

// Authentication and Authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // Add Password Conditions 
});


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
app.UseSession();

// Authentication & Autherization
app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.Seed(app);
await AppDbInitializer.SeedUsersAndRolesAsync(app);


app.Run();

