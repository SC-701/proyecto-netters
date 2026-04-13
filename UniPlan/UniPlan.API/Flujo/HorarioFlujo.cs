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
    public class HorarioFlujo : IHorarioFlujo
    {
        private IHorarioDA _horarioDA;

        public HorarioFlujo(IHorarioDA horarioDA)
        {
            _horarioDA = horarioDA;
        }

        public async Task<Guid> Activar(Guid Id)
        {
            return await _horarioDA.Activar(Id);
        }

        public async Task<Guid> Agregar(HorarioRequest horario)
        {
            return await _horarioDA.Agregar(horario);
        }

        public Task<Guid> Editar(Guid Id, HorarioRequest horario)
        {
            return _horarioDA.Editar(Id, horario);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _horarioDA.Eliminar(Id);
        }

        public async Task<IEnumerable<HorarioResponse>> Obtener()
        {
            return await _horarioDA.Obtener();
        }

        public async Task<HorarioResponse> Obtener(Guid Id)
        {
            return await _horarioDA.Obtener(Id);
        }
    }
}
