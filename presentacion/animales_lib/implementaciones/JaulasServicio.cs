using animales_lib.entidades;
using animales_lib.interfaces;

namespace animales_lib.implementaciones;

public class JaulasServicio : ServicioBaseHttp<Jaulas>, IJaulasServicio
{
    public JaulasServicio(HttpClient http) : base(http, "Jaulas") { }
}
