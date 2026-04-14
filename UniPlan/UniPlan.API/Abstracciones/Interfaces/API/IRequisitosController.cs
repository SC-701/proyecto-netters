using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IRequisitosController
    {
        Task<IActionResult> Agregar(RequisitosRequest requisito);
        Task<IActionResult> Editar(RequisitosRequest requisito);
        Task<IActionResult> Eliminar(RequisitosEliminarRequest requisito);
        Task<IActionResult> ObtenerPorCurso(Guid IdCarrera, Guid IdCurso);
        Task<IActionResult> ObtenerCursosQueLoRequieren(Guid IdCursoRequisito);

        Task<IActionResult> CambiarEstado(RequisitosEstadoRequest requisito);
    }
}