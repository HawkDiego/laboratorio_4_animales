using animales_lib.implementaciones;
using animales_lib.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(o => o.AddDefaultPolicy(p => p
    .WithOrigins("http://localhost:5009", "https://localhost:7257")
    .AllowAnyHeader()
    .AllowAnyMethod()));

builder.Services.AddScoped<IConexion>(_ => new Conexion
{
    StringConexion = builder.Configuration.GetConnectionString("Sql")
});

builder.Services.AddScoped<IEntradasServicio, EntradasServicio>();
builder.Services.AddScoped<IJaulasServicio,   JaulasServicio>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
