using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.App;

// ---------------------------------------------------------------------------
// Modelos de datos
// ---------------------------------------------------------------------------

public class AcademicStats
{
    public int ApprovedSubjects { get; set; }
    public int TotalSubjects { get; set; }
    public int CompletedCredits { get; set; }
    public int TotalCredits { get; set; }
    public int PendingSubjects { get; set; }
    public int CreditsPercent => TotalCredits == 0 ? 0 : (int)Math.Round((double)CompletedCredits / TotalCredits * 100);
    public string GeneralAverage { get; set; } = "";
    public string LastTermAverage { get; set; } = "";
}

public class SubjectItem
{
    public string Name { get; set; } = "";
    public string Initials { get; set; } = "";
    public string Schedule { get; set; } = "";
    public int Credits { get; set; }
    public string BgColor { get; set; } = "#EFF6FF";   // Tailwind blue-50
    public string TextColor { get; set; } = "#2463eb";
}

public class UpcomingSubject
{
    public string Name { get; set; } = "";
    public string Prerequisite { get; set; } = "";
    public bool IsAvailable { get; set; }             // true = punto azul, false = gris
}

// ---------------------------------------------------------------------------
// PageModel
// ---------------------------------------------------------------------------
[Authorize]
public class DashboardModel : PageModel
{
    // Datos del usuario — TODO: cargar desde sesión / API
    public string UserName { get; set; } = "Alex Thompson";
    public string FirstName { get; set; } = "Alex";
    public string UserCareer { get; set; } = "Ingeniería de Software";
    public string UserAvatarUrl { get; set; } =
        "https://lh3.googleusercontent.com/aida-public/AB6AXuBbdaSxX2F1IzAxP7xWO_9TF9pCPclgikgt5h-J7wpb7Kn455-qSfSl3xXOE7tLYrPBcDBclZR42VZCGEstwVO3uqyGbKFZ8oDv2XLaFIJIzhlJrYeyHikW_hfa6xSESIAypSuyFTGt2qY1yvcWC1hz0QqNcDQfqv5pqzvSTLfmjhIKvGpkADchcUSfXzOxgqtRkS4xJTuArzQOlQJ0iTli2skUvl5Np9YRhAmqbLnZvaTLvzqcKOalZnLjF9RAFAudANHDCHMtSQw";

    // Estadísticas — TODO: cargar desde API
    public AcademicStats Stats { get; set; } = new()
    {
        ApprovedSubjects = 15,
        TotalSubjects = 40,
        CompletedCredits = 60,
        TotalCredits = 120,
        PendingSubjects = 25,
        GeneralAverage = "8.4",
        LastTermAverage = "9.1",
    };

    // Materias del cuatrimestre actual — TODO: cargar desde API
    public List<SubjectItem> CurrentTermSubjects { get; set; } = new()
    {
        new() { Name = "Estructuras de Datos",  Initials = "ED", Schedule = "Lunes y Miércoles 18:00 - 20:00", Credits = 4, BgColor = "#DBEAFE", TextColor = "#1D4ED8" },
        new() { Name = "Sistemas Operativos",   Initials = "SO", Schedule = "Martes y Jueves 09:00 - 11:00",   Credits = 6, BgColor = "#EDE9FE", TextColor = "#7C3AED" },
        new() { Name = "Bases de Datos I",      Initials = "BD", Schedule = "Viernes 14:00 - 18:00",           Credits = 5, BgColor = "#FFEDD5", TextColor = "#C2410C" },
    };

    // Próximas materias — TODO: cargar desde API
    public List<UpcomingSubject> UpcomingSubjects { get; set; } = new()
    {
        new() { Name = "Diseño de Algoritmos",     Prerequisite = "Estructuras de Datos", IsAvailable = true  },
        new() { Name = "Ingeniería de Requisitos", Prerequisite = "Bases de Datos I",     IsAvailable = false },
        new() { Name = "Redes de Computadoras",    Prerequisite = "Sistemas Operativos",  IsAvailable = false },
        new() { Name = "Arquitectura de Software", Prerequisite = "",                     IsAvailable = false },
    };

    // Mensaje del tip — TODO: generar dinámicamente desde API
    public string TipMessage { get; set; } =
        "Has completado el 75% de las materias de nivel inicial. ¡Estás muy cerca de comenzar las especializaciones!";

    public void OnGet()
    {
        // TODO: reemplazar datos estáticos con llamadas a tu API
        // var student = await _studentService.GetCurrentStudentAsync(User);
        // Stats = await _academicService.GetStatsAsync(student.Id);
        // CurrentTermSubjects = await _academicService.GetCurrentTermAsync(student.Id);
        // UpcomingSubjects = await _academicService.GetUpcomingSubjectsAsync(student.Id);
    }
}
