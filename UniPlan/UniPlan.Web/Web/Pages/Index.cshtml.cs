using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class FeatureItem
{
    public string Icon { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}

public class IndexModel : PageModel
{
    public string StudentCount { get; set; } = "10,000";
    public int ProgressPercent { get; set; } = 68;
    public string HeroImageUrl { get; set; } = "https://lh3.googleusercontent.com/aida-public/AB6AXuBWoQvQ3xm_3e0nhmLVeMRxU1ok1xsSc5Mb9NSlYUicUs2BnAhK-IsZ5yjYLYTxUmDYWtPyBZHaQjqQdSyTjO-G65Dl8duxglIPGles9cWxehFK5g9bUn3q8di3nkpqHGMuNpubHEvSqwBJTtx-oABiYCVK81dcleoyyg1a70J0HnU7l2S9yufUfWxV38c2JVi1ihRzZZa1D4wZsu2yqmeSutv9Pwd0o9oa6yX1uQBvqioy7r4WoO0BlAj4gNNMeBK5WlqwUoTqiQ4";

    public List<FeatureItem> Features { get; set; } = new()
    {
        new() {
            Icon        = "architecture",
            Title       = "Planificación Inteligente",
            Description = "Valida requisitos, correlativas y créditos de forma automática. Olvida las sorpresas al momento de inscribirte."
        },
        new() {
            Icon        = "calendar_today",
            Title       = "Horarios Dinámicos",
            Description = "Organiza tu semana con una interfaz estilo Notion y Google Calendar. Visualiza tus clases y huecos libres al instante."
        },
        new() {
            Icon        = "trending_up",
            Title       = "Seguimiento de Progreso",
            Description = "Visualiza cuánto te falta para completar tu plan de estudios con gráficos intuitivos y metas personalizadas."
        },
    };

    public void OnGet() { }
}