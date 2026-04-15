using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Admin.Requisitos;

[Authorize]
public class CrearModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    [BindProperty]
    public RequisitosRequest requisito { get; set; } = new();

    public IList<CarreraResponse> carreras { get; set; } = new List<CarreraResponse>();
    public IList<CursoResponse> cursos { get; set; } = new List<CursoResponse>();

    public SelectList ListaCarreras { get; set; }
    public SelectList ListaCursos { get; set; }

    public CrearModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task OnGet()
    {
        await CargarCombos();
    }

    public async Task<ActionResult> OnPost()
    {
        await CargarCombos();

        if (!ModelState.IsValid)
            return Page();

        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarRequisito");
        var cliente = ObtenerClienteConToken();
        var respuesta = await cliente.PostAsJsonAsync(endpoint, requisito);
        respuesta.EnsureSuccessStatusCode();

        return RedirectToPage("./Index", new { idCarrera = requisito.IdCarrera, idCurso = requisito.IdCurso });
    }

    private async Task CargarCombos()
    {
        using var cliente = ObtenerClienteConToken();
        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        string endpointCarreras = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCarreras");
        var respuestaCarreras = await cliente.GetAsync(endpointCarreras);

        if (respuestaCarreras.StatusCode == HttpStatusCode.NoContent)
        {
            carreras = new List<CarreraResponse>();
        }
        else
        {
            respuestaCarreras.EnsureSuccessStatusCode();
            var resultadoCarreras = await respuestaCarreras.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(resultadoCarreras))
            {
                carreras = JsonSerializer.Deserialize<List<CarreraResponse>>(resultadoCarreras, opciones) ?? new List<CarreraResponse>();
            }
            else
            {
                carreras = new List<CarreraResponse>();
            }
        }

        string endpointCursos = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCursos");
        var respuestaCursos = await cliente.GetAsync(endpointCursos);

        if (respuestaCursos.StatusCode == HttpStatusCode.NoContent)
        {
            cursos = new List<CursoResponse>();
        }
        else
        {
            respuestaCursos.EnsureSuccessStatusCode();
            var resultadoCursos = await respuestaCursos.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(resultadoCursos))
            {
                cursos = JsonSerializer.Deserialize<List<CursoResponse>>(resultadoCursos, opciones) ?? new List<CursoResponse>();
            }
            else
            {
                cursos = new List<CursoResponse>();
            }
        }

        ListaCarreras = new SelectList(carreras, "Id", "Nombre");
        ListaCursos = new SelectList(cursos, "Id", "Nombre");
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