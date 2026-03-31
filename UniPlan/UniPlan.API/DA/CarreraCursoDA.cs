using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class CarreraCursoDA : ICarreraCursoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        #region Constructor

        public CarreraCursoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        #endregion

        #region Operaciones
        public async Task<Guid> Agregar(CarreraCursoRequest carreraCurso)
        {
            string query = @"AgregarCarreraCurso";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdCarrera = carreraCurso.IdCarrera,
                IdCurso = carreraCurso.IdCurso,
                Cuatrimestre = carreraCurso.Cuatrimestre,
                Activo = carreraCurso.Activo
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid IdCarrera, Guid IdCurso, CarreraCursoRequest carreraCurso)
        {
            await verificarCarreraCursoExiste(IdCarrera, IdCurso);
            string query = @"EditarCarreraCurso";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdCarrera = IdCarrera,
                IdCurso = IdCurso,
                Cuatrimestre = carreraCurso.Cuatrimestre,
                Activo = carreraCurso.Activo
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid IdCarrera, Guid IdCurso)
        {
            await verificarCarreraCursoExiste(IdCarrera, IdCurso);
            string query = @"EliminarCarreraCurso";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                IdCarrera = IdCarrera,
                IdCurso = IdCurso
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<CarreraCursoResponse>> Obtener()
        {
            string query = @"ObtenerCarreraCurso";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CarreraCursoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<CarreraCursoDetalle> Obtener(Guid IdCarrera, Guid IdCurso)
        {
            string query = @"ObtenerCarreraCursoPorId";
            var resultadoConsulta = await _sqlConnection.QueryAsync<CarreraCursoDetalle>(query,
                new
                {
                    IdCarrera = IdCarrera,
                    IdCurso = IdCurso
                });
            return resultadoConsulta.FirstOrDefault();
        }
        #endregion

        #region Helpers
        private async Task verificarCarreraCursoExiste(Guid IdCarrera, Guid IdCurso)
        {
            CarreraCursoResponse? resultadoConsultaCarreraCurso = await Obtener(IdCarrera, IdCurso);
            if (resultadoConsultaCarreraCurso == null)
                throw new Exception("No se encontro la relacion CarreraCurso");
        }
        #endregion
    }
}

