using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CursoPlanificadoBase
    {
        public Guid IdPlanificacion { get; set; }

        public Guid IdCurso { get; set; }

        public Guid IdGrupoHorario { get; set; }

        public string ColorHex { get; set; } = string.Empty;
    }

    public class CursoPlanificadoRequest : CursoPlanificadoBase
    {
    }

    public class CursoPlanificadoResponse : CursoPlanificadoBase
    {
        public Guid Id { get; set; }
    }

    public class CursoPlanificadoHorarioResponse {
        public Guid IdPlanificacion { get; set; }
        public Guid IdCurso { get; set; }
        public Guid IdGrupoHorario { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string NombreCurso { get; set; } = string.Empty;
        public string Dia { get; set; } = string.Empty;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}