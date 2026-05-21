using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Management_Atelier_Reparatii.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container and require authentication for ComenziService pages.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/ComenziService");
});
builder.Services.AddDbContext<Management_Atelier_ReparatiiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Management_Atelier_ReparatiiContext") ?? throw new InvalidOperationException("Connection string 'Management_Atelier_ReparatiiContext' not found.")));

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<Management_Atelier_ReparatiiContext>();

var app = builder.Build();
app.UseAuthentication();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets();

app.MapGet("/", () => Results.Redirect("/ComenziService"));
app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    var roles = new[] { "Admin", "Mecanic", "Customer" };
    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
        }
    }

    // Optional: create initial admin from config (set in appsettings or change literals)
    var adminEmail = builder.Configuration["AdminUser:Email"] ?? "admin@local";
    var adminPassword = builder.Configuration["AdminUser:Password"] ?? "Admin123!"; 
    var admin = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
    if (admin == null)
    {
        admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var createResult = userManager.CreateAsync(admin, adminPassword).GetAwaiter().GetResult();
        if (createResult.Succeeded)
        {
            userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
        }
    }
    else
    {
        if (!userManager.IsInRoleAsync(admin, "Admin").GetAwaiter().GetResult())
        {
            userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
        }
    }

    // Optional: create initial mecanic account from config or defaults
    var mecanicEmail = builder.Configuration["MecanicUser:Email"] ?? "mecanic@local";
    var mecanicPassword = builder.Configuration["MecanicUser:Password"] ?? "Mecanic123!";
    var mecanic = userManager.FindByEmailAsync(mecanicEmail).GetAwaiter().GetResult();
    if (mecanic == null)
    {
        mecanic = new IdentityUser { UserName = mecanicEmail, Email = mecanicEmail, EmailConfirmed = true };
        var createMech = userManager.CreateAsync(mecanic, mecanicPassword).GetAwaiter().GetResult();
        if (createMech.Succeeded)
        {
            userManager.AddToRoleAsync(mecanic, "Mecanic").GetAwaiter().GetResult();
        }
    }
    else
    {
        if (!userManager.IsInRoleAsync(mecanic, "Mecanic").GetAwaiter().GetResult())
        {
            userManager.AddToRoleAsync(mecanic, "Mecanic").GetAwaiter().GetResult();
        }
    }
}

app.Run();
