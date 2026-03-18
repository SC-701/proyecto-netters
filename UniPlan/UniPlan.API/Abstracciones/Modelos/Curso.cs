using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CursoBase 
    {
        public string Codigo {  get; set; } = string.Empty;
        public string Nombre {  get; set; } = string.Empty;

        public int Creditos { get; set; }

        public int Cuatrimestre { get; set;} 

        public Guid IdPrograma { get; set; }
    }


    public class CursoRequest : CursoBase
    {
    }

    public class CursoResponse : CursoBase
    {
        public Guid Id { get; set; }   
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class CursoDisponibleResponse : CursoResponse {
        public bool YaAprobado { get; set; }
        public bool YaPlanificado { get; set; }
        public bool TieneGruposActivos { get; set; }
        public bool CumpleRequisitos { get; set; }
        public bool CumpleCorrequisitos { get; set; }
        public bool Disponible { get; set; }
        public List<string> Mensajes { get; set; } = new();
    }

    public class CursoDetalleResponse : CursoResponse {
        public List<RequisitoCursoResponse> Requisitos { get; set; } = new();
        public List<GrupoHorarioResponse> GruposHorarios { get; set; } = new();
    }


}
