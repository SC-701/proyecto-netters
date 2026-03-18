using System;

namespace Abstracciones.Modelos
{
    public class GrupoHorarioResponse
    {
        public Guid Id { get; set; }
        public Guid IdCurso { get; set; }
        public string NumeroGrupo { get; set; } = string.Empty;
        public string? Profesor { get; set; }
        public int? Cupo { get; set; }
        public bool Estado { get; set; }
        public List<BloqueHorarioResponse> Horarios { get; set; } = new();
    }

    public class BloqueHorarioResponse
    {
        public Guid Id { get; set; }
        public Guid IdGrupoHorario { get; set; }
        public string Dia { get; set; } = string.Empty;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string? Aula { get; set; }
    }
}
