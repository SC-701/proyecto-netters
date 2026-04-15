using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Escuelas
{
    public class ActivarModel : PageModel
    {
        public string AdminName { get; set; } = "Admin User";
        public string AdminEmail { get; set; } = "admin@uniplan.edu";

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public EscuelaResponse Escuela { get; set; } = new();
        public void OnGet()
        {
        }
    }
}
