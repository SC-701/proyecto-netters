using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class PlanificacionBase
    {
        public Guid IdUsuario { get; set; }

        public int NumeroPeriodo { get; set; }

        public string NombrePeriodo { get; set; } = string.Empty;
    }

    public class PlanificacionRequest : PlanificacionBase
    {
    }

    public class PlanificacionResponse : PlanificacionBase
    {
        public Guid Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Estado { get; set; }
    }
}