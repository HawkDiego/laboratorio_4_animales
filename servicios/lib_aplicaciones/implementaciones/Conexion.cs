using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones;

public class Conexion : DbContext, IConexion
{
    public string? StringConexion { get; set; }

    public Conexion() => StringConexion = ConfiguracionBD.StringConexion;
    public Conexion(string cadena) => StringConexion = cadena;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.AddInterceptors(new AuditoriaInterceptor());
    }

    public DbSet<Entradas>?   Entradas   { get; set; }
    public DbSet<Jaulas>?     Jaulas     { get; set; }
    public DbSet<Auditorias>? Auditorias { get; set; }
}
