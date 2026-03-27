using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA {
    public class PerfilDA : IPerfilDA {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public PerfilDA (IRepositorioDapper repositorioDapper) {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region CRUD
        public async Task<int> Agregar (PerfilRequest perfil) {
            string query = @"SP_Perfil_Agregar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<int>(query, new {
                Nombre = perfil.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<int> Editar (int Id, PerfilRequest perfil) {
            await verificarPerfilExiste(Id);
            string query = @"SP_Perfil_Actualizar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<int>(query, new {
                Id = Id,
                Nombre = perfil.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<int> Eliminar (int Id) {
            await verificarPerfilExiste(Id);
            string query = @"SP_Perfil_Eliminar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<int>(query, new {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<PerfilResponse>> Obtener () {
            string query = @"SP_Perfil_ObtenerTodos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<PerfilResponse>(query);
            return resultadoConsulta;
        }

        public async Task<PerfilResponse> Obtener (int Id) {
            string query = @"SP_Perfil_ObtenerPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<PerfilResponse>(query, new {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarPerfilExiste (int Id) {
            PerfilResponse? resultadoConsulta = await Obtener(Id);
            if (resultadoConsulta == null) {
                throw new Exception("La perfil no existe");
            }
        }
        #endregion
    }
}
