using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Requisitos
{
    public class ActivarModel : PageModel
    {
        public string AdminName { get; set; } = "Admin User";
        public string AdminEmail { get; set; } = "admin@uniplan.edu";

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public RequisitosResponse requisitoMostrar { get; set; } = new();
        public RequisitosRequest requisitoActivar { get; set; } = new();
        public void OnGet()
        {
        }
    }
}
