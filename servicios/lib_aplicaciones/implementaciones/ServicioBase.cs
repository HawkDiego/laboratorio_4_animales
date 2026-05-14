using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones;

public abstract class ServicioBase<T> : IServicio<T> where T : class, IEntidad
{
    protected readonly IConexion _conexion;
    protected DbSet<T> Set => _conexion.Set<T>();

    protected ServicioBase(IConexion conexion) => _conexion = conexion;

    public virtual List<T> Listar()
        => Set.Where(e => e.Estado != 99).ToList();

    public virtual T? ObtenerPorId(int id)
    {
        var e = Set.Find(id);
        return e is null || e.Estado == 99 ? null : e;
    }

    public virtual bool Insertar(T entidad)
    {
        if (entidad.Estado == null) entidad.Estado = 1;
        Set.Add(entidad);
        return _conexion.SaveChanges() > 0;
    }

    public virtual bool Actualizar(T entidad)
    {
        if (entidad.Estado == null) entidad.Estado = 1;
        Set.Update(entidad);
        return _conexion.SaveChanges() > 0;
    }

    public virtual bool Eliminar(int id)
    {
        var e = Set.Find(id);
        if (e is null || e.Estado == 99) return false;
        e.Estado = 99;
        Set.Update(e);
        return _conexion.SaveChanges() > 0;
    }
}
