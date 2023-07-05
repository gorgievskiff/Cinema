using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repo;
using Repo.Implementation;
using Repo.Interfaces;
using Service.Implementations;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// services and data access

builder.Services.TryAddScoped<IGenreService, GenreService>();
builder.Services.TryAddScoped<IGenreDa, GenreDa>();
builder.Services.TryAddScoped<IMovieService, MovieService>();
builder.Services.TryAddScoped<IMovieDa, MovieDa>();
builder.Services.TryAddScoped<ITicketDa, TicketDa>();
builder.Services.TryAddScoped<ITicketService , TicketService>();
builder.Services.TryAddScoped<IShoppingCartDa, ShoppingCartDa>();
builder.Services.TryAddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.TryAddScoped<IOrderDa, OrderDa>();
builder.Services.TryAddScoped<IOrderService, OrderService>();



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
