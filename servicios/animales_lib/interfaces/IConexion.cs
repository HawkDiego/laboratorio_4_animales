using animales_lib.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace animales_lib.interfaces;

public interface IConexion
{
    string? StringConexion { get; set; }

    DbSet<Entradas>?   Entradas   { get; set; }
    DbSet<Jaulas>?     Jaulas     { get; set; }
    DbSet<Auditorias>? Auditorias { get; set; }

    EntityEntry<T> Entry<T>(T entity) where T : class;
    DbSet<T>       Set<T>() where T : class;

    int SaveChanges();
}
