using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Admin.Requisitos;

public class IndexModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    private readonly IConfiguracion _configuracion;

    public IList<RequisitosResponse> requisitos { get; set; } = new List<RequisitosResponse>();
    public IList<CarreraResponse> carreras { get; set; } = new List<CarreraResponse>();
    public IList<CursoResponse> cursos { get; set; } = new List<CursoResponse>();

    [BindProperty(SupportsGet = true)]
    public Guid? IdCarrera { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? IdCurso { get; set; }

    public SelectList ListaCarreras { get; set; }
    public SelectList ListaCursos { get; set; }

    public IndexModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task OnGet()
    {
        await CargarCombos();

        if (IdCarrera.HasValue && IdCurso.HasValue)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerRequisitosPorCurso");
            using var cliente = ObtenerClienteConToken();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, IdCarrera, IdCurso));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.StatusCode == HttpStatusCode.NoContent)
            {
                requisitos = new List<RequisitosResponse>();
                return;
            }

            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (!string.IsNullOrWhiteSpace(resultado))
            {
                requisitos = JsonSerializer.Deserialize<List<RequisitosResponse>>(resultado, opciones) ?? new List<RequisitosResponse>();
            }
            else
            {
                requisitos = new List<RequisitosResponse>();
            }
        }
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
        var tokenClaim = HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "Token");

        var cliente = new HttpClient();

        if (tokenClaim != null)
        {
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenClaim.Value);
        }

        return cliente;
    }

    public async Task<IActionResult> OnPostCambiarEstado(Guid idCarrera, Guid idCurso, Guid idCursoRequisito, bool activo)
    {
        string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CambiarEstadoRequisito");
        var cliente = ObtenerClienteConToken();

        var request = new RequisitosEstadoRequest
        {
            IdCarrera = idCarrera,
            IdCurso = idCurso,
            IdCursoRequisito = idCursoRequisito,
            Activo = activo
        };

        var respuesta = await cliente.PutAsJsonAsync(endpoint, request);
        respuesta.EnsureSuccessStatusCode();

        return RedirectToPage("./Index", new
        {
            IdCarrera = idCarrera,
            IdCurso = idCurso
        });
    }
}