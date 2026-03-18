using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin;

// ---------------------------------------------------------------------------
// Modelos de datos
// ---------------------------------------------------------------------------

public class SubjectFilterModel
{
    public string? Query { get; set; }
    public string? CareerId { get; set; }
    public string? Semester { get; set; }
}

public class FilterChip
{
    public string Label { get; set; } = "";
    public Dictionary<string, string> RemoveParams { get; set; } = new();
}

public class SubjectItem
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
    public string Mention { get; set; } = "";
    public int Credits { get; set; }
    public string Career { get; set; } = "";
}

// ---------------------------------------------------------------------------
// PageModel
// ---------------------------------------------------------------------------

public class SubjectsModel : PageModel
{
    // Datos del admin — TODO: cargar desde sesión / claims
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // Filtros bindeados desde query string
    [BindProperty(SupportsGet = true)]
    public SubjectFilterModel Filter { get; set; } = new();

    // Paginación
    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public int TotalSubjects { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalSubjects / PageSize);
    public int PageStart => (CurrentPage - 1) * PageSize + 1;
    public int PageEnd => Math.Min(CurrentPage * PageSize, TotalSubjects);

    // Opciones del select de Carreras — TODO: cargar desde API
    public List<SelectListItem> CareerOptions { get; set; } = new()
    {
        new("Ingeniería de Sistemas", "1"),
        new("Ingeniería Civil",       "2"),
        new("Administración de Empresas", "3"),
        new("Medicina",               "4"),
        new("Derecho",                "5"),
    };

    // Chips de filtros activos (para mostrar las etiquetas con X)
    public List<FilterChip> ActiveFilters { get; set; } = new();

    // Asignaturas paginadas — TODO: cargar desde API
    public List<SubjectItem> Subjects { get; set; } = new()
    {
        new() { Id = 1, Code = "SIS-102", Name = "Algoritmos y Estructuras de Datos", Mention = "Mención: Software",        Credits = 4, Career = "Ingeniería de Sistemas"   },
        new() { Id = 2, Code = "MAT-205", Name = "Cálculo Multivariable",             Mention = "Básica Profesional",        Credits = 5, Career = "Ingeniería Civil"          },
        new() { Id = 3, Code = "ADM-301", Name = "Principios de Administración",      Mention = "Troncal Carrera",           Credits = 3, Career = "Administración de Empresas"},
        new() { Id = 4, Code = "BIO-112", Name = "Biología Celular",                  Mention = "Ciencias de la Salud",      Credits = 4, Career = "Medicina"                  },
        new() { Id = 5, Code = "DER-440", Name = "Derecho Penal II",                  Mention = "Obligatoria",               Credits = 4, Career = "Derecho"                   },
    };

    // -----------------------------------------------------------------------
    // Handlers
    // -----------------------------------------------------------------------

    public void OnGet()
    {
        CurrentPage = Math.Max(1, CurrentPage);

        // TODO: Subjects = await _subjectService.GetPagedAsync(Filter, CurrentPage, PageSize);
        // TODO: TotalSubjects = await _subjectService.CountAsync(Filter);

        TotalSubjects = 142; // placeholder

        // Construir chips de filtros activos
        BuildActiveFilters();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // TODO: await _subjectService.DeleteAsync(id);
        TempData["Success"] = "Asignatura eliminada correctamente.";
        return RedirectToPage();
    }

    // -----------------------------------------------------------------------
    // Helpers
    // -----------------------------------------------------------------------

    private void BuildActiveFilters()
    {
        // Params base sin el filtro que se está eliminando
        var baseParams = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(Filter.Query)) baseParams["query"] = Filter.Query;
        if (!string.IsNullOrEmpty(Filter.CareerId)) baseParams["careerId"] = Filter.CareerId;
        if (!string.IsNullOrEmpty(Filter.Semester)) baseParams["semester"] = Filter.Semester;

        if (!string.IsNullOrEmpty(Filter.Query))
        {
            var p = new Dictionary<string, string>(baseParams);
            p.Remove("query");
            ActiveFilters.Add(new FilterChip { Label = $"Búsqueda: {Filter.Query}", RemoveParams = p });
        }

        if (!string.IsNullOrEmpty(Filter.CareerId))
        {
            var label = CareerOptions.FirstOrDefault(c => c.Value == Filter.CareerId)?.Text ?? Filter.CareerId;
            var p = new Dictionary<string, string>(baseParams);
            p.Remove("careerId");
            ActiveFilters.Add(new FilterChip { Label = $"Carrera: {label}", RemoveParams = p });
        }

        if (!string.IsNullOrEmpty(Filter.Semester))
        {
            var label = Filter.Semester == "1" ? "Primer Semestre" : "Segundo Semestre";
            var p = new Dictionary<string, string>(baseParams);
            p.Remove("semester");
            ActiveFilters.Add(new FilterChip { Label = $"Semestre: {label}", RemoveParams = p });
        }
    }
}
