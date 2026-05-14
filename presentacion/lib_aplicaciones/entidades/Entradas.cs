using lib_aplicaciones.interfaces;

namespace lib_aplicaciones.entidades;

public class Entradas : IEntidad
{
    public int      Id                { get; set; }
    public string?  Nombre            { get; set; }
    public decimal  PrecioEntrada     { get; set; }
    public decimal  Descuento         { get; set; }
    public decimal  ValorSinDescuento { get; set; }
    public DateTime Fecha             { get; set; }
    public int      JaulaId           { get; set; }
    public int?     Estado            { get; set; }
    public Jaulas?  Jaula             { get; set; }
}
