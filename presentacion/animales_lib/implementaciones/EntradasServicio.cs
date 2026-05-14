using animales_lib.entidades;
using animales_lib.interfaces;

namespace animales_lib.implementaciones;

public class EntradasServicio : ServicioBaseHttp<Entradas>, IEntradasServicio
{
    public EntradasServicio(HttpClient http) : base(http, "Entradas") { }
}
