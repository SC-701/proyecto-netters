using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin;

// ---------------------------------------------------------------------------
// Modelos de datos
// ---------------------------------------------------------------------------

public class CareerStats
{
    public int TotalCareers { get; set; }
    public int TotalFaculties { get; set; }
    public int ActiveStudents { get; set; }
}

public class CareerItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Faculty { get; set; } = "";
    public int TotalCredits { get; set; }
    public int StudentCount { get; set; }
    public string Icon { get; set; } = "school";      // Material Symbol
    public string IconBgColor { get; set; } = "#DBEAFE";     // bg color inline
    public string IconColor { get; set; } = "#1D4ED8";     // icon color inline
}

// ---------------------------------------------------------------------------
// PageModel
// ---------------------------------------------------------------------------

public class CareersModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // Paginación
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 4;
    public int TotalPages => (int)Math.Ceiling((double)Stats.TotalCareers / PageSize);
    public int PageStart => (CurrentPage - 1) * PageSize + 1;
    public int PageEnd => Math.Min(CurrentPage * PageSize, Stats.TotalCareers);

    // Estadísticas — TODO: cargar desde API
    public CareerStats Stats { get; set; } = new()
    {
        TotalCareers = 24,
        TotalFaculties = 6,
        ActiveStudents = 4450,
    };

    // Lista de carreras paginada — TODO: cargar desde API con paginación real
    public List<CareerItem> Careers { get; set; } = new()
    {
        new() { Id = 1, Name = "Ingeniería de Sistemas",       Faculty = "Ingeniería",          TotalCredits = 240, StudentCount = 1200, Icon = "terminal",          IconBgColor = "#DBEAFE", IconColor = "#1D4ED8" },
        new() { Id = 2, Name = "Licenciatura en Administración",Faculty = "Ciencias Económicas", TotalCredits = 180, StudentCount = 950,  Icon = "payments",          IconBgColor = "#D1FAE5", IconColor = "#059669" },
        new() { Id = 3, Name = "Derecho",                       Faculty = "Ciencias Jurídicas",  TotalCredits = 300, StudentCount = 1500, Icon = "gavel",             IconBgColor = "#EDE9FE", IconColor = "#7C3AED" },
        new() { Id = 4, Name = "Medicina",                      Faculty = "Ciencias de la Salud",TotalCredits = 360, StudentCount = 800,  Icon = "medical_services",  IconBgColor = "#FEE2E2", IconColor = "#DC2626" },
    };

    // -----------------------------------------------------------------------
    // Handlers
    // -----------------------------------------------------------------------

    public void OnGet(int page = 1)
    {
        CurrentPage = Math.Max(1, page);
        // TODO: Careers = await _careerService.GetPagedAsync(CurrentPage, PageSize);
        // TODO: Stats   = await _careerService.GetStatsAsync();
    }

    public IActionResult OnGetCreate()
    {
        // TODO: redirigir al formulario de creación
        return RedirectToPage("/Admin/CareerForm");
    }

    public IActionResult OnGetEdit(int id)
    {
        // TODO: redirigir al formulario de edición
        return RedirectToPage("/Admin/CareerForm", new { id });
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // TODO: await _careerService.DeleteAsync(id);
        TempData["Success"] = "Carrera eliminada correctamente.";
        return RedirectToPage();
    }
}
