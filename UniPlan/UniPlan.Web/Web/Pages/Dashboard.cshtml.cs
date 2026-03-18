using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class DashboardModel : PageModel
    {

        public string StudentName { get; set; } = "Alex";
        public void OnGet()
        {
        }
    }
}
