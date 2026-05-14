using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;

namespace lib_aplicaciones.implementaciones;

public class EntradasServicio : ServicioBaseHttp<Entradas>, IEntradasServicio
{
    public EntradasServicio(HttpClient http) : base(http, "Entradas") { }
}
