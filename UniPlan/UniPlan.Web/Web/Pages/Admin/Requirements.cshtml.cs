using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin;

// ---------------------------------------------------------------------------
// Modelos de datos
// ---------------------------------------------------------------------------

public class RequirementStats
{
    public int TotalCourses { get; set; }
    public int ActivePrerequisites { get; set; }
    public int Corequisites { get; set; }
    public int Inconsistencies { get; set; }
}

public class RequirementRow
{
    public int Id { get; set; }
    public string CourseName { get; set; } = "";
    public string Department { get; set; } = "";
    public string Code { get; set; } = "";
    public List<string> Prerequisites { get; set; } = new();
    public List<string> Corequisites { get; set; } = new();
}

public class FlowNode
{
    public string Label { get; set; } = "";
    public bool IsActive { get; set; }   // nodo resaltado en azul
    public bool IsLocked { get; set; }   // nodo gris/bloqueado
}

// ---------------------------------------------------------------------------
// PageModel
// ---------------------------------------------------------------------------

public class RequirementsModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // Filtro de búsqueda
    [BindProperty(SupportsGet = true)]
    public string? Query { get; set; }

    // Paginación
    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages => (int)Math.Ceiling((double)Stats.TotalCourses / PageSize);
    public int PageStart => (CurrentPage - 1) * PageSize + 1;
    public int PageEnd => Math.Min(CurrentPage * PageSize, Stats.TotalCourses);

    // Estadísticas — TODO: cargar desde API
    public RequirementStats Stats { get; set; } = new()
    {
        TotalCourses = 124,
        ActivePrerequisites = 312,
        Corequisites = 48,
        Inconsistencies = 2,
    };

    // Filas de la tabla — TODO: cargar desde API con paginación y filtro
    public List<RequirementRow> Requirements { get; set; } = new()
    {
        new() {
            Id = 1, CourseName = "Cálculo Diferencial", Department = "Departamento de Ciencias",
            Code = "MATH101",
            Prerequisites = new(),
            Corequisites  = new() { "Geometría Analítica" },
        },
        new() {
            Id = 2, CourseName = "Cálculo Integral", Department = "Departamento de Ciencias",
            Code = "MATH102",
            Prerequisites = new() { "MATH101" },
            Corequisites  = new(),
        },
        new() {
            Id = 3, CourseName = "Física Mecánica", Department = "Departamento de Física",
            Code = "PHYS201",
            Prerequisites = new() { "MATH101" },
            Corequisites  = new() { "MATH102" },
        },
        new() {
            Id = 4, CourseName = "Programación Orientada a Objetos", Department = "Departamento de Sistemas",
            Code = "COMP205",
            Prerequisites = new() { "COMP101", "MATH101" },
            Corequisites  = new(),
        },
    };

    // Nodos para la vista previa del flujo — TODO: cargar desde API
    public List<FlowNode> FlowPreview { get; set; } = new()
    {
        new() { Label = "Fundamentos de Programación", IsActive = false, IsLocked = false },
        new() { Label = "Estructura de Datos",         IsActive = true,  IsLocked = false },
        new() { Label = "Algoritmos Avanzados",        IsActive = false, IsLocked = true  },
    };

    // -----------------------------------------------------------------------
    // Handlers
    // -----------------------------------------------------------------------

    public void OnGet()
    {
        CurrentPage = Math.Max(1, CurrentPage);
        // TODO: Requirements = await _requirementService.GetPagedAsync(Query, CurrentPage, PageSize);
        // TODO: Stats        = await _requirementService.GetStatsAsync();
        // TODO: FlowPreview  = await _requirementService.GetFlowPreviewAsync();
    }

    public IActionResult OnGetExport()
    {
        // TODO: var file = await _requirementService.ExportCsvAsync();
        // return File(file, "text/csv", "requisitos.csv");
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // TODO: await _requirementService.DeleteAsync(id);
        TempData["Success"] = "Requisito eliminado correctamente.";
        return RedirectToPage();
    }
}
