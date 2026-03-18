using System;

namespace Abstracciones.Modelos
{
    public class CursoAprobadoBase
    {
        public Guid IdUsuario { get; set; }
        public Guid IdCurso { get; set; }
    }

    public class CursoAprobadoRequest : CursoAprobadoBase
    {
    }

    public class CursoAprobadoResponse : CursoAprobadoBase
    {
        public Guid Id { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public string CodigoCurso { get; set; } = string.Empty;
        public string NombreCurso { get; set; } = string.Empty;
    }
}
