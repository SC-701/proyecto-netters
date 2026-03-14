using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class UsuarioBase
    {


        public string NombreCompleto { get; set; }= string.Empty;

        public string Correo {  get; set; }= string.Empty;

        public Guid IdPrograma { get; set; }

    }

    

    public class UsuarioRequest : UsuarioBase { 
    

        public string Contrasenna { get; set; } = string.Empty;
    
    }

   

    public class UsuarioResponse : UsuarioBase
    {
        public Guid Id { get; set; }


        

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }





    }


    public class LoginRequest
    {
        public string Correo { get; set; } = string.Empty;
        public string Contrasenna { get; set; } = string.Empty;
    }

}
