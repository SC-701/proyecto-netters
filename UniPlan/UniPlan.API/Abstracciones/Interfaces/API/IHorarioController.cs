using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IHorarioController
    {
        Task<IActionResult> Agregar(HorarioRequest horario);
        Task<IActionResult> Editar(Guid Id, HorarioRequest horario);
        Task<IActionResult> Eliminar(Guid Id);
        Task<IActionResult> Obtener();
        Task<IActionResult> Obtener(Guid Id);
        Task<IActionResult> Activar(Guid Id);
    }
}
