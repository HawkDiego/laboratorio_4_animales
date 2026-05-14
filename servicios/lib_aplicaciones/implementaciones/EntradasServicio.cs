using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones;

public class EntradasServicio : ServicioBase<Entradas>, IEntradasServicio
{
    private static readonly (int Mes, int Dia)[] FechasDescuento =
    {
        (4, 22),
        (4, 24),
        (5, 20),
        (9,  7)
    };

    public EntradasServicio(IConexion conexion) : base(conexion) { }

    public override List<Entradas> Listar()
        => Set.Include(e => e.Jaula)
              .Where(e => e.Estado != 99)
              .ToList();

    public override Entradas? ObtenerPorId(int id)
    {
        var e = Set.Include(x => x.Jaula).FirstOrDefault(x => x.Id == id);
        return e is null || e.Estado == 99 ? null : e;
    }

    public override bool Insertar(Entradas entidad)
    {
        Validar(entidad);
        AplicarReglas(entidad);
        return base.Insertar(entidad);
    }

    public override bool Actualizar(Entradas entidad)
    {
        Validar(entidad);
        AplicarReglas(entidad);
        return base.Actualizar(entidad);
    }

    private void Validar(Entradas e)
    {
        if (string.IsNullOrWhiteSpace(e.Nombre))
            throw new InvalidOperationException("Nombre es obligatorio.");
        if (e.PrecioEntrada <= 0)
            throw new InvalidOperationException("PrecioEntrada debe ser mayor a 0.");
        if (e.JaulaId <= 0)
            throw new InvalidOperationException("JaulaId es obligatorio.");
        var jaula = _conexion.Set<Jaulas>().Find(e.JaulaId);
        if (jaula is null || jaula.Estado == 99)
            throw new InvalidOperationException($"Jaula {e.JaulaId} no existe.");
    }

    private void AplicarReglas(Entradas e)
    {
        var jaula = _conexion.Set<Jaulas>().AsNoTracking().FirstOrDefault(j => j.Id == e.JaulaId);
        var esAfrica = jaula?.Nombre?.Contains("Africa", StringComparison.OrdinalIgnoreCase) ?? false;

        e.ValorSinDescuento = Math.Round(e.PrecioEntrada * (esAfrica ? 1.05m : 1m), 2);
        e.Descuento = EsFechaDescuento(e.Fecha) ? 0.15m : 0m;
    }

    private static bool EsFechaDescuento(DateTime f)
        => FechasDescuento.Any(d => d.Mes == f.Month && d.Dia == f.Day);
}
