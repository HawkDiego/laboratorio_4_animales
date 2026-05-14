using animales_lib.entidades;
using animales_lib.interfaces;

namespace animales_lib.implementaciones;

public class JaulasServicio : ServicioBase<Jaulas>, IJaulasServicio
{
    public JaulasServicio(IConexion conexion) : base(conexion) { }
}
