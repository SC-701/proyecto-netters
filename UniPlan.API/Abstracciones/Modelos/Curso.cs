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

        public int Credtos { get; set; }

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



}
