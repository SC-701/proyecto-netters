using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IHorarioDA
    {
        Task<IEnumerable<HorarioResponse>> Obtener();
        Task<HorarioResponse> Obtener(Guid Id);
        Task<Guid> Agregar(HorarioRequest horario);
        Task<Guid> Editar(Guid Id, HorarioRequest horario);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
    }
}
