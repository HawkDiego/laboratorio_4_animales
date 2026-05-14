using lib_aplicaciones.interfaces;

namespace lib_aplicaciones.entidades;

public class Jaulas : IEntidad
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public int? Estado { get; set; }
}
