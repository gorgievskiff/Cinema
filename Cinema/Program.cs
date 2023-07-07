using DinkToPdf.Contracts;
using DinkToPdf;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repo;
using Repo.Implementation;
using Repo.Interfaces;
using Service.Implementations;
using Service.Interfaces;
using System.Configuration;
using Domain.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Identity;
using Microsoft.Extensions.Hosting;

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
builder.Services.TryAddScoped<IUserManagementDa, UserManagementDa>();
builder.Services.TryAddScoped<IUserManagementService, UserManagementService>();

builder.Services.TryAddScoped<IEmailServiceOwn, EmailServiceC>();

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddScoped<IUserStore<IdentityUser>, UserStore<IdentityUser, IdentityRole, ApplicationDbContext>>();
builder.Services.AddScoped<IUserEmailStore<IdentityUser>, UserStore<IdentityUser, IdentityRole, ApplicationDbContext>>();


var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


var app = builder.Build();
var serviceProvider = app.Services;

// Create a scope to resolve services
using (var scope = serviceProvider.CreateScope())
{
    // Get the RoleManager and UserManager from the DI container
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var cartService = scope.ServiceProvider.GetRequiredService<IShoppingCartService>();

    // Create roles if they don't exist 
    var roles = new[] { "Customer", "Admin" };
    foreach (var roleName in roles)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Create the admin user if it doesn't exist
    var adminEmail = "filipgorg192011@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };

        var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");

        await cartService.Add(new ShoppingCart { UserId = adminUser.Id });

        if (result.Succeeded)
        {
            // Assign the "Admin" role to the admin user
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            // Handle user creation failure
            var errors = result.Errors;
        }
    }
}


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
