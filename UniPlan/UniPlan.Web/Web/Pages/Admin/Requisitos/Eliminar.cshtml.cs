using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Web.Pages.Admin.Requisitos;

[Authorize]
public class EliminarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    public RequisitosResponse requisitoMostrar { get; set; } = new();

    [BindProperty]
    public RequisitosEliminarRequest requisitoEliminar { get; set; } = new();

    public EliminarModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task<ActionResult> OnGet(Guid idCarrera, Guid idCurso, Guid idCursoRequisito)
    {
        if (idCarrera == Guid.Empty || idCurso == Guid.Empty || idCursoRequisito == Guid.Empty)
            return NotFound();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerRequisitosPorCurso");
        var cliente = ObtenerClienteConToken();
        var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, idCarrera, idCurso));

        var respuesta = await cliente.SendAsync(solicitud);

        if (respuesta.StatusCode == HttpStatusCode.NoContent)
            return NotFound();

        respuesta.EnsureSuccessStatusCode();

        var resultado = await respuesta.Content.ReadAsStringAsync();
        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var lista = new List<RequisitosResponse>();

        if (!string.IsNullOrWhiteSpace(resultado))
        {
            lista = JsonSerializer.Deserialize<List<RequisitosResponse>>(resultado, opciones) ?? new List<RequisitosResponse>();
        }

        var requisitoEncontrado = lista.FirstOrDefault(x =>
            x.IdCarrera == idCarrera &&
            x.IdCurso == idCurso &&
            x.IdCursoRequisito == idCursoRequisito);

        if (requisitoEncontrado == null)
            return NotFound();

        requisitoMostrar = requisitoEncontrado;

        requisitoEliminar = new RequisitosEliminarRequest
        {
            IdCarrera = requisitoEncontrado.IdCarrera,
            IdCurso = requisitoEncontrado.IdCurso,
            IdCursoRequisito = requisitoEncontrado.IdCursoRequisito
        };

        return Page();
    }

    public async Task<ActionResult> OnPost()
    {
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarRequisito");
        var cliente = ObtenerClienteConToken();

        var request = new HttpRequestMessage(HttpMethod.Delete, endpoint)
        {
            Content = JsonContent.Create(requisitoEliminar)
        };

        var respuesta = await cliente.SendAsync(request);
        respuesta.EnsureSuccessStatusCode();

        return RedirectToPage("./Index", new
        {
            idCarrera = requisitoEliminar.IdCarrera,
            idCurso = requisitoEliminar.IdCurso
        });
    }

    private HttpClient ObtenerClienteConToken()
    {
        var tokenClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token");
        var cliente = new HttpClient();

        if (tokenClaim != null)
        {
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenClaim.Value);
        }

        return cliente;
    }
}