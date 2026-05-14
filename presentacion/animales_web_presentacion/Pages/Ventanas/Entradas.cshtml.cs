using animales_lib.entidades;
using animales_lib.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace animales_web_presentacion.Pages.Ventanas;

public class EntradasModel : PageModel
{
    private readonly IEntradasServicio _entradas;
    private readonly IJaulasServicio   _jaulas;

    [BindProperty] public List<Entradas>? Lista       { get; set; }
    [BindProperty] public Entradas?       Entidad     { get; set; }
    [BindProperty] public bool            Borrando    { get; set; }
    public List<SelectListItem>           JaulasOpts  { get; set; } = new();

    public EntradasModel(IEntradasServicio entradas, IJaulasServicio jaulas)
    {
        _entradas = entradas;
        _jaulas   = jaulas;
    }

    public void OnGet() => OnPostBtRefrescar();

    public void OnPostBtRefrescar()
    {
        try
        {
            Lista   = _entradas.Listar();
            Entidad = null;
            CargarJaulas();
        }
        catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
    }

    public void OnPostBtNuevo()
    {
        Entidad = new Entradas
        {
            Estado = 1,
            Fecha  = DateTime.Today
        };
        Borrando = false;
        CargarJaulas();
    }

    public void OnPostBtModificar(int data)
    {
        try
        {
            Lista = _entradas.Listar();
            Entidad = Lista.FirstOrDefault(x => x.Id == data);
            Lista = null;
            Borrando = false;
            CargarJaulas();
        }
        catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
    }

    public void OnPostBtGuardar()
    {
        try
        {
            if (Entidad == null) return;
            if (Entidad.Id == 0)
                _entradas.Insertar(Entidad);
            else
                _entradas.Actualizar(Entidad);
            OnPostBtRefrescar();
        }
        catch (Exception ex)
        {
            ViewData["Mensaje"] = ex.Message;
            CargarJaulas();
        }
    }

    public void OnPostBtBorrar()
    {
        try
        {
            if (Entidad == null) return;
            _entradas.Eliminar(Entidad.Id);
            OnPostBtRefrescar();
        }
        catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
    }

    public void OnPostBtBorrarVal(int data)
    {
        try
        {
            Lista = _entradas.Listar();
            Entidad = Lista.FirstOrDefault(x => x.Id == data);
            Lista = null;
            Borrando = true;
            CargarJaulas();
        }
        catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
    }

    public void OnPostBtCerrar()
    {
        OnPostBtRefrescar();
        Borrando = false;
    }

    private void CargarJaulas()
    {
        try
        {
            JaulasOpts = _jaulas.Listar()
                .Select(j => new SelectListItem(j.Nombre, j.Id.ToString()))
                .ToList();
        }
        catch (Exception ex) { ViewData["Mensaje"] = ex.Message; }
    }
}
