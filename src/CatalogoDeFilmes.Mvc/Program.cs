using CatalogoDeFilmes.Data.Contexts;
using CatalogoDeFilmes.Application.Services;
using CatalogoDeFilmes.Application.Services.Interfaces;
using CatalogoDeFilmes.Data.Repositories.Interfaces;
using CatalogoDeFilmes.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDiretorService, DiretorService>();
builder.Services.AddScoped<IDiretoresRepository, DiretoresRepository>();
builder.Services.AddDbContext<CatalogoConext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BdConnection"));
});
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
