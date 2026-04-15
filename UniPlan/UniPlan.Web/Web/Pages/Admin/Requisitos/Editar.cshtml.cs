using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Admin.Requisitos;

[Authorize]
public class EditarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    [BindProperty]
    public RequisitosRequest requisito { get; set; } = new();

    public string NombreCarrera { get; set; } = string.Empty;
    public string NombreCurso { get; set; } = string.Empty;
    public string NombreCursoRequisito { get; set; } = string.Empty;

    public EditarModel(IConfiguracion configuracion)
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

        requisito = new RequisitosRequest
        {
            IdCarrera = requisitoEncontrado.IdCarrera,
            IdCurso = requisitoEncontrado.IdCurso,
            IdCursoRequisito = requisitoEncontrado.IdCursoRequisito,
            EsCorequisito = requisitoEncontrado.EsCorequisito
        };

        NombreCarrera = requisitoEncontrado.Carrera;
        NombreCurso = requisitoEncontrado.Curso;
        NombreCursoRequisito = requisitoEncontrado.CursoRequisito;

        return Page();
    }

    public async Task<ActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarRequisito");
        var cliente = ObtenerClienteConToken();

        var respuesta = await cliente.PutAsJsonAsync(endpoint, requisito);
        respuesta.EnsureSuccessStatusCode();

        return RedirectToPage("./Index");
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