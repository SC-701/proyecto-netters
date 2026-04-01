using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICarreraCursoDA
    {
        Task<IEnumerable<CarreraCursoResponse>> Obtener();
        Task<CarreraCursoDetalle> Obtener(Guid IdCarrera);
        Task<Guid> Agregar(CarreraCursoRequest carreraCurso);
        Task<Guid> Editar(Guid IdCarrera, Guid IdCurso, CarreraCursoRequest carreraCurso);
        Task<Guid> Eliminar(Guid IdCarrera, Guid IdCurso);
    }
}
