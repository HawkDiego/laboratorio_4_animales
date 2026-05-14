using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;

namespace lib_aplicaciones.implementaciones;

public class JaulasServicio : ServicioBaseHttp<Jaulas>, IJaulasServicio
{
    public JaulasServicio(HttpClient http) : base(http, "Jaulas") { }
}
