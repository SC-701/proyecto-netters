using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class CarreraCursoFlujo : ICarreraCursoFlujo
    {
        private ICarreraCursoDA _carreraCursoDA;

        public CarreraCursoFlujo(ICarreraCursoDA carreraCursoDA)
        {
            _carreraCursoDA = carreraCursoDA;
        }

        public async Task<Guid> Agregar(CarreraCursoRequest carreraCurso)
        {
            return await _carreraCursoDA.Agregar(carreraCurso);
        }

        public async Task<Guid> Editar(Guid IdCarrera, Guid IdCurso, CarreraCursoRequest carreraCurso)
        {
            return await _carreraCursoDA.Editar(IdCarrera, IdCurso, carreraCurso);
        }

        public async Task<Guid> Eliminar(Guid IdCarrera, Guid IdCurso)
        {
            return await _carreraCursoDA.Eliminar(IdCarrera, IdCurso);
        }

        public async Task<IEnumerable<CarreraCursoResponse>> Obtener()
        {
            return await _carreraCursoDA.Obtener();
        }

        public async Task<CarreraCursoDetalle> Obtener(Guid IdCarrera)
        {
            return await _carreraCursoDA.Obtener(IdCarrera);
        }

    }
}
