using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA {
    public class PerfilxUsuarioDA : IPerfilxUsuarioDA {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public PerfilxUsuarioDA (IRepositorioDapper repositorioDapper) {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region CRUD
        public async Task Agregar (PerfilxUsuarioRequest perfilxUsuario) {
            string query = @"SP_PerfilxUsuario_Agregar";
            await _sqlConnection.ExecuteScalarAsync(query, new {
                IdUsuario = perfilxUsuario.IdUsuario,
                IdPerfil = perfilxUsuario.IdPerfil
            });
        }
                
        public async Task Eliminar (PerfilxUsuarioRequest perfilxUsuario) {
            string query = @"SP_PerfilxUsuario_Eliminar";
            await _sqlConnection.ExecuteScalarAsync(query, new {
                IdUsuario = perfilxUsuario.IdUsuario,
                IdPerfil = perfilxUsuario.IdPerfil
            });
        }
        #endregion
    }
}
