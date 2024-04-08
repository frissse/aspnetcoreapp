using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM.DAL;
using PM.DAL.EF;
using PM.BL;
using PM.BL.Domain;
using PM.UI.Web.MVC;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PMdBContextConnection") ?? throw new InvalidOperationException("Connection string 'PMdBContextConnection' not found.");

builder.Services.AddScoped<DbContextOptionsBuilder>();
builder.Services.AddDbContext<PMdBContext>(options =>
    options.UseSqlite(@"Data Source=PM_MVC_DB.sqlite;"));
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IManager, Manager>();
// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PMdBContext>();

builder.Services.ConfigureApplicationCookie(cao =>
{
    cao.Events.OnRedirectToLogin += redirectContext =>
    {
        if (redirectContext.Request.Path.StartsWithSegments("/api"))
            redirectContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    cao.Events.OnRedirectToAccessDenied += redirectContext =>
    {
        if (redirectContext.Request.Path.StartsWithSegments("/api"))
            redirectContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});


builder.Services.AddRazorPages();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    PMdBContext ctx = scope.ServiceProvider.GetService<PMdBContext>();
    if (ctx.CreateDatabase(dropDatabase: true))
    {
        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();
        SeedIdentity(userManager, roleManager);
        DataSeeder.Seed(ctx);
    }
}

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
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Episode}/{action=Index}/{id?}");

app.Run();

void SeedIdentity(UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    const string adminRole = "Admin";
    roleManager.CreateAsync(new IdentityRole(adminRole)).Wait();

    const string userRole = "User";
    roleManager.CreateAsync(new IdentityRole(userRole)).Wait();
    
    var user1 = new IdentityUser()
    {
        Id = "User1Id",
        UserName = "user1@app.com",
        Email = "user1@app.com"
    };

    var user2 = new IdentityUser()
    {
        Id = "User2Id",
        UserName = "user2@app.com",
        Email = "user2@app.com"
    };

    var user3 = new IdentityUser()
    {
        Id = "User3Id",
        UserName = "user3@app.com",
        Email = "user3@app.com"
    };
        
    var user4 = new IdentityUser()
    {
        Id = "User4Id",
        UserName ="user4@app.com",
        Email = "user4@app.com"
    };

    var user5 = new IdentityUser()
    {
        Id = "User5Id",
        UserName = "user5@app.com",
        Email = "user5@app.com"
    };
    
    userManager.CreateAsync(user1, "Password1!").Wait();
    userManager.AddToRoleAsync(user1, adminRole).Wait();
    userManager.CreateAsync(user2, "Password1!").Wait();
    userManager.AddToRoleAsync(user2, userRole).Wait();
    userManager.CreateAsync(user3, "Password1!").Wait();
    userManager.AddToRoleAsync(user3, userRole).Wait();
    userManager.CreateAsync(user4, "Password1!").Wait();
    userManager.AddToRoleAsync(user4, userRole).Wait();
    userManager.CreateAsync(user5, "Password1!").Wait();
    userManager.AddToRoleAsync(user5, userRole).Wait();

}

public partial class Program { }