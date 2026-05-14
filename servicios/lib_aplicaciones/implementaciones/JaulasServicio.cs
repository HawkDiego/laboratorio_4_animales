using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;

namespace lib_aplicaciones.implementaciones;

public class JaulasServicio : ServicioBase<Jaulas>, IJaulasServicio
{
    public JaulasServicio(IConexion conexion) : base(conexion) { }
}
