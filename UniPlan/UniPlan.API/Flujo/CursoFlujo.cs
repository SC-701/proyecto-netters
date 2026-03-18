using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo {
    public class CursoFlujo : ICursoFlujo {
        private readonly ICursoDA _cursoDA;
        private readonly IUsuarioDA _usuarioDA;
        private readonly ICursoAprobadoDA _cursoAprobadoDA;
        private readonly IPlanificacionDA _planificacionDA;

        public CursoFlujo (ICursoDA cursoDA, IUsuarioDA usuarioDA, ICursoAprobadoDA cursoAprobadoDA, IPlanificacionDA planificacionDA) {
            _cursoDA = cursoDA;
            _usuarioDA = usuarioDA;
            _cursoAprobadoDA = cursoAprobadoDA;
            _planificacionDA = planificacionDA;
        }

        public async Task<IEnumerable<CursoResponse>> ObtenerCursosPorPrograma (Guid idPrograma) {
            return await _cursoDA.ObtenerCursosPorPrograma(idPrograma);
        }

        public async Task<CursoDetalleResponse?> ObtenerCursoDetalle (Guid idCurso) {
            var curso = await _cursoDA.ObtenerCursoPorId(idCurso);
            if (curso == null)
                return null;

            var requisitos = (await _cursoDA.ObtenerRequisitosPorCurso(idCurso)).ToList();
            var grupos = (await _cursoDA.ObtenerGruposHorarioPorCurso(idCurso)).ToList();

            foreach (var grupo in grupos) {
                grupo.Horarios = (await _cursoDA.ObtenerBloquesHorarioGrupo(grupo.Id)).ToList();
            }

            return new CursoDetalleResponse {
                Id = curso.Id,
                Codigo = curso.Codigo,
                Nombre = curso.Nombre,
                Creditos = curso.Creditos,
                Cuatrimestre = curso.Cuatrimestre,
                IdPrograma = curso.IdPrograma,
                Estado = curso.Estado,
                FechaCreacion = curso.FechaCreacion,
                Requisitos = requisitos,
                GruposHorarios = grupos
            };
        }

        public async Task<IEnumerable<CursoDisponibleResponse>> ObtenerCursosDisponiblesParaUsuario (Guid idUsuario) {
            var usuario = await _usuarioDA.ObtenerUsuarioPorId(idUsuario)
                ?? throw new InvalidOperationException("El usuario indicado no existe.");

            var cursosPrograma = (await _cursoDA.ObtenerCursosPorPrograma(usuario.IdPrograma)).ToList();
            var aprobados = (await _cursoAprobadoDA.ObtenerCursosAprobadosPorUsuario(idUsuario)).ToList();
            var aprobadosIds = aprobados.Select(x => x.IdCurso).ToHashSet();

            var planificaciones = (await _planificacionDA.ObtenerPlanificacionesPorUsuario(idUsuario)).ToList();
            var planificadosIds = new HashSet<Guid>();

            foreach (var planificacion in planificaciones) {
                var cursosPlanificados = await _planificacionDA.ObtenerCursosPlanificadosPorPlanificacion(planificacion.Id);
                foreach (var cursoPlanificado in cursosPlanificados) {
                    planificadosIds.Add(cursoPlanificado.IdCurso);
                }
            }

            var resultado = new List<CursoDisponibleResponse>();

            foreach (var curso in cursosPrograma) {
                var grupos = (await _cursoDA.ObtenerGruposHorarioPorCurso(curso.Id)).ToList();
                var requisitos = (await _cursoDA.ObtenerRequisitosPorCurso(curso.Id)).ToList();

                var mensajes = new List<string>();
                var cumpleRequisitos = true;
                var cumpleCorrequisitos = true;

                foreach (var requisito in requisitos.Where(x => !x.EsCorrequisito)) {
                    if (!aprobadosIds.Contains(requisito.IdCursoRequerido)) {
                        cumpleRequisitos = false;
                        mensajes.Add($"Requisito pendiente: {requisito.CodigoCursoRequerido} - {requisito.NombreCursoRequerido}");
                    }
                }

                foreach (var requisito in requisitos.Where(x => x.EsCorrequisito)) {
                    if (!aprobadosIds.Contains(requisito.IdCursoRequerido) &&
                        !planificadosIds.Contains(requisito.IdCursoRequerido)) {
                        cumpleCorrequisitos = false;
                        mensajes.Add($"Correquisito pendiente: {requisito.CodigoCursoRequerido} - {requisito.NombreCursoRequerido}");
                    }
                }

                var yaAprobado = aprobadosIds.Contains(curso.Id);
                var yaPlanificado = planificadosIds.Contains(curso.Id);
                var tieneGruposActivos = grupos.Any(x => x.Estado);

                if (yaAprobado)
                    mensajes.Add("El curso ya fue aprobado por el estudiante.");

                if (yaPlanificado)
                    mensajes.Add("El curso ya se encuentra agregado en una planificación activa.");

                if (!tieneGruposActivos)
                    mensajes.Add("El curso no tiene grupos activos disponibles.");

                resultado.Add(new CursoDisponibleResponse {
                    Id = curso.Id,
                    Codigo = curso.Codigo,
                    Nombre = curso.Nombre,
                    Creditos = curso.Creditos,
                    Cuatrimestre = curso.Cuatrimestre,
                    IdPrograma = curso.IdPrograma,
                    Estado = curso.Estado,
                    FechaCreacion = curso.FechaCreacion,
                    YaAprobado = yaAprobado,
                    YaPlanificado = yaPlanificado,
                    TieneGruposActivos = tieneGruposActivos,
                    CumpleRequisitos = cumpleRequisitos,
                    CumpleCorrequisitos = cumpleCorrequisitos,
                    Disponible = !yaAprobado && !yaPlanificado && tieneGruposActivos && cumpleRequisitos && cumpleCorrequisitos,
                    Mensajes = mensajes
                });
            }

            return resultado
                .OrderBy(x => x.Cuatrimestre)
                .ThenBy(x => x.Nombre);
        }
    }
}
