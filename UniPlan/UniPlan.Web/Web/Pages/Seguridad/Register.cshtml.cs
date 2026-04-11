using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Account;

public class RegisterModel : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; } = new();

    // Lista de carreras para el <select> — puede venir de tu API
    public List<SelectListItem> CareerOptions { get; set; } = new()
    {
        new SelectListItem("Ingeniería de Software",    "software"),
        new SelectListItem("Administración de Empresas","admin"),
        new SelectListItem("Derecho",                   "derecho"),
        new SelectListItem("Medicina",                  "medicina"),
        new SelectListItem("Diseño Gráfico",            "diseno"),
        new SelectListItem("Psicología",                "psicologia"),
    };

    public class InputModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre completo")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingresa un correo válido.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Selecciona tu carrera.")]
        [Display(Name = "Carrera")]
        public string Career { get; set; } = "";

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "Mínimo 8 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirma tu contraseña.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar")]
        public string ConfirmPassword { get; set; } = "";
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // TODO: conectar con tu API de registro
        // var result = await _authService.RegisterAsync(Input);
        // if (!result.Success) { ModelState.AddModelError("", result.Message); return Page(); }

        return RedirectToPage("/Account/Login");
    }
}
