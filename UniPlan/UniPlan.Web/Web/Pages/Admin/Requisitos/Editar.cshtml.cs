using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Requisitos;

public class EditarModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

    // Los 3 campos componen la clave compuesta del requisito
    [BindProperty(SupportsGet = true)]
    public Guid IdCarrera { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid IdCurso { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid IdCursoRequisito { get; set; }

    [BindProperty]
    public RequisitosRequest Input { get; set; } = new();

    // TODO: cargar desde API
    public List<SelectListItem> CarreraOptions { get; set; } = new()
    {
        new("Ingeniería de Sistemas",    Guid.NewGuid().ToString()),
        new("Administración de Empresas",Guid.NewGuid().ToString()),
        new("Psicología Organizacional", Guid.NewGuid().ToString()),
    };

    public List<SelectListItem> CursoOptions { get; set; } = new()
    {
        new("Cálculo Integral",    Guid.NewGuid().ToString()),
        new("Estructura de Datos", Guid.NewGuid().ToString()),
        new("Física General II",   Guid.NewGuid().ToString()),
    };

    public List<SelectListItem> CursoRequisitoOptions { get; set; } = new()
    {
        new("Cálculo Diferencial", Guid.NewGuid().ToString()),
        new("Programación Básica", Guid.NewGuid().ToString()),
        new("Física General I",    Guid.NewGuid().ToString()),
    };

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: var req = await _requisitoService.ObtenerAsync(IdCarrera, IdCurso, IdCursoRequisito);
        // if (req is null) return NotFound();
        // Input = new RequisitosRequest { IdCarrera = req.IdCarrera, IdCurso = req.IdCurso, IdCursoRequisito = req.IdCursoRequisito, EsCorequisito = req.EsCorequisito };

        // Datos de ejemplo
        Input = new RequisitosRequest
        {
            IdCarrera = IdCarrera,
            IdCurso = IdCurso,
            IdCursoRequisito = IdCursoRequisito,
            EsCorequisito = false,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: await _requisitoService.ActualizarAsync(IdCarrera, IdCurso, IdCursoRequisito, Input);

        TempData["Exito"] = "Requisito actualizado correctamente.";
        return RedirectToPage("/Admin/Requisitos/Index");
    }
}
