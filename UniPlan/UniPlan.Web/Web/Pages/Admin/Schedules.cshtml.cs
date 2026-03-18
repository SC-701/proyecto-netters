using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin;

// ---------------------------------------------------------------------------
// Modelos de datos
// ---------------------------------------------------------------------------

public class ScheduleFilterModel
{
    public string? SemesterId { get; set; }
    public string? FacultyId { get; set; }
    public string? CareerId { get; set; }
}

public class GroupItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string BadgeCode { get; set; } = "";   // ej: "CS10"
    public string BadgeBgColor { get; set; } = "#DBEAFE";
    public string BadgeTextColor { get; set; } = "#1D4ED8";
    public string Teacher { get; set; } = "";
    public string Room { get; set; } = "";
    public List<string> TimeSlots { get; set; } = new();
    public int EnrolledCount { get; set; }
    public int Capacity { get; set; }
    public bool NeedsAttention { get; set; }

    public int CapacityPercent => Capacity == 0 ? 0
        : (int)Math.Round((double)EnrolledCount / Capacity * 100);
}

// ---------------------------------------------------------------------------
// PageModel
// ---------------------------------------------------------------------------

public class SchedulesModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // Filtros y vista activa
    [BindProperty(SupportsGet = true)]
    public ScheduleFilterModel Filter { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string ActiveView { get; set; } = "list";   // "list" | "calendar"

    // Paginación
    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public int TotalGroups { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalGroups / PageSize);
    public int PageEnd => Math.Min(CurrentPage * PageSize, TotalGroups);

    // Opciones de los selects — TODO: cargar desde API
    public List<SelectListItem> SemesterOptions { get; set; } = new()
    {
        new("2024-II (Actual)",       "2024-2"),
        new("2025-I (Planificación)", "2025-1"),
        new("2024-I",                 "2024-1"),
    };

    public List<SelectListItem> FacultyOptions { get; set; } = new()
    {
        new("Ingeniería de Sistemas",       "1"),
        new("Ciencias de la Salud",         "2"),
        new("Derecho y Ciencias Políticas", "3"),
    };

    public List<SelectListItem> CareerOptions { get; set; } = new()
    {
        new("Software Engineering", "1"),
        new("Data Science",         "2"),
        new("Cybersecurity",        "3"),
    };

    // Grupos paginados — TODO: cargar desde API
    public List<GroupItem> Groups { get; set; } = new()
    {
        new()
        {
            Id             = 1,
            Name           = "Programación Avanzada - Grupo A",
            BadgeCode      = "CS10",
            BadgeBgColor   = "#DBEAFE",
            BadgeTextColor = "#2563EB",
            Teacher        = "Dr. Roberto Gómez",
            Room           = "Aula 402, Pabellón B",
            TimeSlots      = new() { "LUN 08:00 - 10:00", "MIE 08:00 - 10:00" },
            EnrolledCount  = 32,
            Capacity       = 40,
            NeedsAttention = false,
        },
        new()
        {
            Id             = 2,
            Name           = "Cálculo Multivariable - Grupo C",
            BadgeCode      = "MA22",
            BadgeBgColor   = "#EDE9FE",
            BadgeTextColor = "#7C3AED",
            Teacher        = "Dra. Elena Martínez",
            Room           = "Aula 105, Pabellón C",
            TimeSlots      = new() { "MAR 14:00 - 16:00", "JUE 14:00 - 16:00" },
            EnrolledCount  = 12,
            Capacity       = 45,
            NeedsAttention = false,
        },
        new()
        {
            Id             = 3,
            Name           = "Grupo sin asignar profesor",
            BadgeCode      = "--",
            BadgeBgColor   = "#F1F5F9",
            BadgeTextColor = "#94A3B8",
            Teacher        = "",
            Room           = "",
            TimeSlots      = new() { "VIE 10:00 - 13:00" },
            EnrolledCount  = 0,
            Capacity       = 30,
            NeedsAttention = true,
        },
    };

    // -----------------------------------------------------------------------
    // Handlers
    // -----------------------------------------------------------------------

    public void OnGet()
    {
        CurrentPage = Math.Max(1, CurrentPage);
        TotalGroups = 48; // placeholder
        // TODO: Groups      = await _scheduleService.GetPagedAsync(Filter, CurrentPage, PageSize);
        // TODO: TotalGroups = await _scheduleService.CountAsync(Filter);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // TODO: await _scheduleService.DeleteAsync(id);
        TempData["Success"] = "Grupo eliminado correctamente.";
        return RedirectToPage();
    }
}
