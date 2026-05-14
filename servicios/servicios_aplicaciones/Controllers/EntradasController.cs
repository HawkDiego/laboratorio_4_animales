using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_aplicaciones.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class EntradasController : ControllerBase
{
    private readonly IEntradasServicio _servicio;
    public EntradasController(IEntradasServicio servicio) => _servicio = servicio;

    [HttpGet]
    public ActionResult<List<Entradas>> Listar() => Ok(_servicio.Listar());

    [HttpGet("{id:int}")]
    public ActionResult<Entradas> ObtenerPorId(int id)
    {
        var e = _servicio.ObtenerPorId(id);
        return e is null ? NotFound() : Ok(e);
    }

    [HttpPost]
    public ActionResult Insertar([FromBody] Entradas entidad)
    {
        try
        {
            return _servicio.Insertar(entidad)
                ? CreatedAtAction(nameof(ObtenerPorId), new { id = entidad.Id }, entidad)
                : BadRequest();
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpPut]
    public ActionResult Actualizar([FromBody] Entradas entidad)
    {
        try
        {
            return _servicio.Actualizar(entidad) ? Ok() : NotFound();
        }
        catch (InvalidOperationException ex) { return BadRequest(ex.Message); }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Eliminar(int id)
        => _servicio.Eliminar(id) ? NoContent() : NotFound();
}
