using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Requisitos;

public class CrearModel : PageModel
{
    public string AdminName { get; set; } = "Admin User";
    public string AdminEmail { get; set; } = "admin@uniplan.edu";

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

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: await _requisitoService.CrearAsync(Input);

        TempData["Exito"] = "Requisito creado correctamente.";
        return RedirectToPage("/Admin/Requisitos/Index");
    }
}
