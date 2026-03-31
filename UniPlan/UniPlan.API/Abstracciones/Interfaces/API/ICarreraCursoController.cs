using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICarreraCursoController
    {
        Task<IActionResult> Agregar(CarreraCursoRequest carreraCurso);
        Task<IActionResult> Editar(Guid IdCarrera, Guid IdCurso, CarreraCursoRequest carreraCurso);
        Task<IActionResult> Eliminar(Guid IdCarrera, Guid IdCurso);
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid IdCarrera);
    }
}
