using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA {
    public class CursoDA : ICursoDA {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public CursoDA (IRepositorioDapper repositorioDapper) {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region CRUD
        public async Task<Guid> Agregar (CursoRequest curso) {
            string query = @"SP_Curso_Agregar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id = Guid.NewGuid(),
                Sigla = curso.Sigla,
                Nombre = curso.Nombre,
                Creditos = curso.Creditos,
                IdEscuela = curso.IdEscuela
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar (Guid Id, CursoRequest curso) {
            await verificarCursoExiste(Id);
            string query = @"SP_Curso_Actualizar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id = Id,
                Sigla = curso.Sigla,
                Nombre = curso.Nombre,
                Creditos = curso.Creditos,
                IdEscuela = curso.IdEscuela
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar (Guid Id) {
            await verificarCursoExiste(Id);
            string query = @"SP_Curso_Eliminar";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<CursoResponse>> Obtener () {
            string query = @"SP_Curso_ObtenerTodos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CursoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<CursoResponse> Obtener (Guid Id) {
            string query = @"SP_Curso_ObtenerPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CursoResponse>(query, new {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<CursoResponse> ObtenerSigla (string Sigla) {
            string query = @"SP_Curso_ObtenerPorSigla";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CursoResponse>(query, new {
                Sigla = Sigla
            });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarCursoExiste (Guid Id) {
            CursoResponse? resultadoConsulta = await Obtener(Id);
            if (resultadoConsulta == null) {
                throw new Exception("La curso no existe");
            }
        }
        #endregion
    }
}
