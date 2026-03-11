using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProgramaBase 
    {
     
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
       

    }



    public class ProgramaRequest : ProgramaBase { }



    public class ProgramaResponse : ProgramaBase { 
    
        public Guid IdPrograma { get; set; }
        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }





    }
}
