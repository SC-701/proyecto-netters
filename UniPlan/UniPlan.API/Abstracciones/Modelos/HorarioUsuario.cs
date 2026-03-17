using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class HorarioUsuarioResponse
    {
        public Guid IdPlanificacion { get; set; }

        public string NombrePeriodo { get; set; } = string.Empty;

        public string Codigo { get; set; } = string.Empty;

        public string NombreCurso { get; set; } = string.Empty;

        public string NumeroGrupo { get; set; } = string.Empty;

        public string Dia { get; set; } = string.Empty;

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public string Aula { get; set; } = string.Empty;

        public string ColorHex { get; set; } = string.Empty;
    }
}