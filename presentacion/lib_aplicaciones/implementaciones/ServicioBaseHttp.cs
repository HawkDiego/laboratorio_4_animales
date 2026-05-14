using System.Net;
using System.Net.Http.Json;
using lib_aplicaciones.interfaces;

namespace lib_aplicaciones.implementaciones;

public abstract class ServicioBaseHttp<T> : IServicio<T> where T : class, IEntidad
{
    private readonly HttpClient _http;
    private readonly string _recurso;

    protected ServicioBaseHttp(HttpClient http, string recurso)
    {
        _http = http;
        _recurso = recurso;
    }

    public List<T> Listar()
    {
        var lista = _http.GetFromJsonAsync<List<T>>($"{_recurso}/Listar")
            .GetAwaiter().GetResult();
        return lista ?? new List<T>();
    }

    public T? ObtenerPorId(int id)
    {
        var resp = _http.GetAsync($"{_recurso}/ObtenerPorId/{id}")
            .GetAwaiter().GetResult();
        if (resp.StatusCode == HttpStatusCode.NotFound)
            return null;
        resp.EnsureSuccessStatusCode();
        return resp.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
    }

    public bool Insertar(T entidad)
    {
        var resp = _http.PostAsJsonAsync($"{_recurso}/Insertar", entidad)
            .GetAwaiter().GetResult();
        if (!resp.IsSuccessStatusCode)
            return false;
        var creada = resp.Content.ReadFromJsonAsync<T>().GetAwaiter().GetResult();
        if (creada != null)
            entidad.Id = creada.Id;
        return true;
    }

    public bool Actualizar(T entidad)
    {
        var resp = _http.PutAsJsonAsync($"{_recurso}/Actualizar", entidad)
            .GetAwaiter().GetResult();
        return resp.IsSuccessStatusCode;
    }

    public bool Eliminar(int id)
    {
        var resp = _http.DeleteAsync($"{_recurso}/Eliminar/{id}")
            .GetAwaiter().GetResult();
        return resp.IsSuccessStatusCode;
    }
}
