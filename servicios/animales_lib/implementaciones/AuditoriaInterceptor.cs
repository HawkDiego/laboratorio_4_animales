using animales_lib.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace animales_lib.implementaciones;

public class AuditoriaInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        Registrar(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        Registrar(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void Registrar(DbContext? ctx)
    {
        if (ctx is null) return;
        var ahora = DateTime.Now;
        var registros = ctx.ChangeTracker.Entries<Entradas>()
            .Where(e => e.State is EntityState.Added
                                 or EntityState.Modified
                                 or EntityState.Deleted)
            .ToList();
        foreach (var e in registros)
        {
            ctx.Set<Auditorias>().Add(new Auditorias
            {
                Accion = MapearAccion(e),
                Fecha  = ahora
            });
        }
    }

    private static string MapearAccion(EntityEntry<Entradas> e) => e.State switch
    {
        EntityState.Added                                          => "Insertar Entrada",
        EntityState.Modified when EsSoftDelete(e)                  => "Eliminar Entrada (soft)",
        EntityState.Modified                                       => "Actualizar Entrada",
        EntityState.Deleted                                        => "Eliminar Entrada (hard)",
        _                                                          => "Cambio Entrada"
    };

    private static bool EsSoftDelete(EntityEntry<Entradas> e)
        => e.Property(nameof(Entradas.Estado)).IsModified
        && (e.Entity.Estado == 99);
}
