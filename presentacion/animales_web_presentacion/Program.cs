using animales_lib.implementaciones;
using animales_lib.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var apiBase = builder.Configuration["ApiBase"]
    ?? throw new InvalidOperationException("Falta ApiBase en appsettings");

builder.Services.AddHttpClient<IEntradasServicio, EntradasServicio>(c =>
    c.BaseAddress = new Uri(apiBase));
builder.Services.AddHttpClient<IJaulasServicio, JaulasServicio>(c =>
    c.BaseAddress = new Uri(apiBase));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
