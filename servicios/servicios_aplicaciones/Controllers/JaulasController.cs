using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_aplicaciones.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class JaulasController : ControllerBase
{
    private readonly IJaulasServicio _servicio;
    public JaulasController(IJaulasServicio servicio) => _servicio = servicio;

    [HttpGet]
    public ActionResult<List<Jaulas>> Listar() => Ok(_servicio.Listar());
}
