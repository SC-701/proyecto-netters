using System;

namespace Abstracciones.Modelos
{
    public class RequisitoCursoResponse
    {
        public Guid Id { get; set; }
        public Guid IdCurso { get; set; }
        public Guid IdCursoRequerido { get; set; }
        public bool EsCorrequisito { get; set; }
        public string CodigoCursoRequerido { get; set; } = string.Empty;
        public string NombreCursoRequerido { get; set; } = string.Empty;
    }
}
