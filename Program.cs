using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Management_Atelier_Reparatii.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Management_Atelier_ReparatiiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Management_Atelier_ReparatiiContext") ?? throw new InvalidOperationException("Connection string 'Management_Atelier_ReparatiiContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
