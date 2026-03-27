using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo {
    public class CarreraFlujo : ICarreraFlujo {
        private ICarreraDA _carreraDA;

        public CarreraFlujo (ICarreraDA carreraDA) {
            _carreraDA = carreraDA;
        }

        public async Task<Guid> Agregar (CarreraRequest carrera) {
            return await _carreraDA.Agregar(carrera);
        }

        public async Task<Guid> Editar (Guid Id, CarreraRequest carrera) {
            return await _carreraDA.Editar(Id, carrera);
        }

        public async Task<Guid> Eliminar (Guid Id) {
            return await _carreraDA.Eliminar(Id);
        }

        public async Task<IEnumerable<CarreraResponse>> Obtener () {
            return await _carreraDA.Obtener();
        }

        public async Task<CarreraResponse> Obtener (Guid Id) {
            return await _carreraDA.Obtener(Id);
        }
    }
}
