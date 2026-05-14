using animales_lib.entidades;
using animales_lib.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace animales_servicios.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class JaulasController : ControllerBase
{
    private readonly IJaulasServicio _servicio;
    public JaulasController(IJaulasServicio servicio) => _servicio = servicio;

    [HttpGet]
    public ActionResult<List<Jaulas>> Listar() => Ok(_servicio.Listar());
}
