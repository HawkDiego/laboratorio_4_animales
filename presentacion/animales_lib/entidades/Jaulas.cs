using animales_lib.interfaces;

namespace animales_lib.entidades;

public class Jaulas : IEntidad
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public int? Estado { get; set; }
}
