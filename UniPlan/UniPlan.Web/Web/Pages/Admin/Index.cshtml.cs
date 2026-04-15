using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

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
    public string AdminName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string AdminEmail { get; set; } = "";
    public string AdminRole { get; set; } = "Administrador";

    public int TotalCarreras { get; set; } = 8;
    public int TotalCursos { get; set; } = 42;
    public int TotalEscuelas { get; set; } = 5;
    public int TotalRequisitos { get; set; } = 26;

    public List<AdminQuickAccess> QuickAccess { get; set; } = new();

    public void OnGet()
    {
        AdminName = User.FindFirst("NombreUsuario")?.Value
                    ?? User.FindFirst(ClaimTypes.Name)?.Value
                    ?? "Administrador";

        FirstName = AdminName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "Admin";

        AdminEmail = User.FindFirst("CorreoElectronico")?.Value ?? "admin@uniplan.edu";
        AdminRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "Administrador";

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
}