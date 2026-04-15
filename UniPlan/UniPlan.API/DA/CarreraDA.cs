using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA {
    public class CarreraDA : ICarreraDA {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public CarreraDA (IRepositorioDapper repositorioDapper) {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region CRUD
        public async Task<Guid> Agregar (CarreraRequest carrera) {
            string query = @"SP_Carrera_Agregar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Nombre = carrera.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar (Guid Id, CarreraRequest carrera) {
            await verificarCarreraExiste(Id);
            string query = @"SP_Carrera_Actualizar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id = Id,
                Nombre = carrera.Nombre,
                Activo = carrera.Activo
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar (Guid Id) {
            await verificarCarreraExiste(Id);
            string query = @"SP_Carrera_Eliminar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<CarreraResponse>> Obtener () {
            string query = @"SP_Carrera_ObtenerTodos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CarreraResponse>(query);
            return resultadoConsulta;
        }

        public async Task<CarreraResponse> Obtener (Guid Id) {
            string query = @"SP_Carrera_ObtenerPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CarreraResponse>(query, new {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarCarreraExiste (Guid Id) {
            CarreraResponse? resultadoConsulta = await Obtener(Id);
            if (resultadoConsulta == null) {
                throw new Exception("La carrera no existe");
            }
        }
        #endregion
    }
}
