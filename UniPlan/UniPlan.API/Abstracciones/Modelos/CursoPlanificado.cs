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
}