using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace Web.Pages.Admin;

public class AdminQuickAccess
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string PageUrl { get; set; } = "";
    public string Icon { get; set; } = "";
    public string BgColor { get; set; } = "";
    public string TextColor { get; set; } = "";
}

[Authorize]
public class IndexModel : PageModel
{
    private readonly IConfiguracion _configuracion;

    public string AdminName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string AdminEmail { get; set; } = "";

    public int TotalCarreras { get; set; }
    public int TotalCursos { get; set; }
    public int TotalEscuelas { get; set; }
    public int TotalHorarios { get; set; }
    public int TotalRequisitos { get; set; }

    public List<AdminQuickAccess> QuickAccess { get; set; } = new();

    public IndexModel(IConfiguracion configuracion)
    {
        _configuracion = configuracion;
    }

    public async Task OnGet()
    {
        CargarDatosAdministrador();
        ConfigurarAccesosRapidos();
        await CargarResumenAdministrativo();
    }

    private void CargarDatosAdministrador()
    {
        AdminName = User.FindFirst("NombreUsuario")?.Value
                    ?? User.FindFirst(ClaimTypes.Name)?.Value
                    ?? "Administrador";

        FirstName = AdminName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "Admin";

        AdminEmail = User.FindFirst("CorreoElectronico")?.Value
                     ?? User.FindFirst(ClaimTypes.Email)?.Value
                     ?? "admin@uniplan.edu";
    }

    private void ConfigurarAccesosRapidos()
    {
        QuickAccess = new List<AdminQuickAccess>
        {
            new()
            {
                Title = "Carreras",
                Description = "Gestiona las carreras registradas en el sistema.",
                PageUrl = "/Admin/Carreras/Index",
                Icon = "school",
                BgColor = "#DBEAFE",
                TextColor = "#1D4ED8"
            },
            new()
            {
                Title = "Cursos",
                Description = "Administra cursos, siglas y créditos.",
                PageUrl = "/Admin/Cursos/Index",
                Icon = "menu_book",
                BgColor = "#EDE9FE",
                TextColor = "#7C3AED"
            },
            new()
            {
                Title = "Escuelas",
                Description = "Organiza las escuelas académicas.",
                PageUrl = "/Admin/Escuelas/Index",
                Icon = "domain",
                BgColor = "#DCFCE7",
                TextColor = "#15803D"
            },
            new()
            {
                Title = "Horarios",
                Description = "Consulta y administra horarios disponibles.",
                PageUrl = "/Admin/Horarios/Index",
                Icon = "schedule",
                BgColor = "#FEF3C7",
                TextColor = "#B45309"
            },
            new()
            {
                Title = "Requisitos",
                Description = "Gestiona requisitos y corequisitos de cursos.",
                PageUrl = "/Admin/Requisitos/Index",
                Icon = "rule",
                BgColor = "#FCE7F3",
                TextColor = "#BE185D"
            }
        };
    }

    private async Task CargarResumenAdministrativo()
    {
        using var cliente = ObtenerClienteConToken();
        var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        TotalCarreras = await ContarRegistros<CarreraResponse>(cliente, "ObtenerCarreras", opciones);
        TotalCursos = await ContarRegistros<CursoResponse>(cliente, "ObtenerCursos", opciones);
        TotalEscuelas = await ContarRegistros<EscuelaResponse>(cliente, "ObtenerEscuelas", opciones);
        TotalHorarios = await ContarRegistros<HorarioResponse>(cliente, "ObtenerHorarios", opciones);
        TotalRequisitos = await ContarRegistros<RequisitosResponse>(cliente, "ObtenerRequisitos", opciones);
    }

    private async Task<int> ContarRegistros<T>(HttpClient cliente, string nombreMetodo, JsonSerializerOptions opciones)
    {
        try
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", nombreMetodo);
            var respuesta = await cliente.GetAsync(endpoint);

            if (respuesta.StatusCode == HttpStatusCode.NoContent)
                return 0;

            respuesta.EnsureSuccessStatusCode();

            var resultado = await respuesta.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(resultado))
                return 0;

            var lista = JsonSerializer.Deserialize<List<T>>(resultado, opciones) ?? new List<T>();
            return lista.Count;
        }
        catch
        {
            return 0;
        }
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