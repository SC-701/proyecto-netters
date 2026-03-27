using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA {
    public class EscuelaDA : IEscuelaDA {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public EscuelaDA (IRepositorioDapper repositorioDapper) {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region CRUD
        public async Task<Guid> Agregar (EscuelaRequest escuela) {
            string query = @"SP_Escuela_Agregar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Nombre = escuela.Nombre,
                Area = escuela.Area
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar (Guid Id, EscuelaRequest escuela) {
            await verificarEscuelaExiste(Id);
            string query = @"SP_Escuela_Actualizar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id = Id,
                Nombre = escuela.Nombre,
                Area = escuela.Area
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar (Guid Id) {
            await verificarEscuelaExiste(Id);
            string query = @"SP_Escuela_Eliminar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<EscuelaResponse>> Obtener () {
            string query = @"SP_Escuela_ObtenerTodos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<EscuelaResponse>(query);
            return resultadoConsulta;
        }

        public async Task<EscuelaResponse> Obtener (Guid Id) {
            string query = @"SP_Escuela_ObtenerPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<EscuelaResponse>(query, new {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarEscuelaExiste (Guid Id) {
            EscuelaResponse? resultadoConsulta = await Obtener(Id);
            if (resultadoConsulta == null) {
                throw new Exception("La escuela no existe");
            }
        }
        #endregion
    }
}
