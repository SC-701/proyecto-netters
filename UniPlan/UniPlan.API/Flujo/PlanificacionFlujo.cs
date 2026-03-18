using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo {
    public class PlanificacionFlujo : IPlanificacionFlujo {
        private readonly IPlanificacionDA _planificacionDA;
        private readonly IUsuarioDA _usuarioDA;
        private readonly ICursoDA _cursoDA;
        private readonly ICursoAprobadoDA _cursoAprobadoDA;

        public PlanificacionFlujo (IPlanificacionDA planificacionDA, IUsuarioDA usuarioDA, ICursoDA cursoDA, ICursoAprobadoDA cursoAprobadoDA) {
            _planificacionDA = planificacionDA;
            _usuarioDA = usuarioDA;
            _cursoDA = cursoDA;
            _cursoAprobadoDA = cursoAprobadoDA;
        }

        public async Task<Guid> CrearPlanificacion (PlanificacionRequest planificacion) {
            var usuario = await _usuarioDA.ObtenerUsuarioPorId(planificacion.IdUsuario);
            if (usuario == null)
                throw new InvalidOperationException("El usuario indicado no existe.");

            var yaExiste = await _planificacionDA.ExistePlanificacionUsuarioPeriodo(
                planificacion.IdUsuario,
                planificacion.NumeroPeriodo);

            if (yaExiste)
                throw new InvalidOperationException("Ya existe una planificación activa para ese período.");

            return await _planificacionDA.CrearPlanificacion(planificacion);
        }

        public async Task<IEnumerable<PlanificacionResponse>> ObtenerPlanificacionesPorUsuario (Guid idUsuario) {
            return await _planificacionDA.ObtenerPlanificacionesPorUsuario(idUsuario);
        }

        public async Task AgregarCursoPlanificado (CursoPlanificadoRequest cursoPlanificado) {
            var planificacion = await _planificacionDA.ObtenerPlanificacionPorId(cursoPlanificado.IdPlanificacion)
                ?? throw new InvalidOperationException("La planificación indicada no existe.");

            var usuario = await _usuarioDA.ObtenerUsuarioPorId(planificacion.IdUsuario)
                ?? throw new InvalidOperationException("El usuario asociado a la planificación no existe.");

            var curso = await _cursoDA.ObtenerCursoPorId(cursoPlanificado.IdCurso)
                ?? throw new InvalidOperationException("El curso indicado no existe.");

            if (!curso.Estado)
                throw new InvalidOperationException("El curso se encuentra inactivo.");

            if (curso.IdPrograma != usuario.IdPrograma)
                throw new InvalidOperationException("El curso no pertenece al programa del usuario.");

            var grupos = (await _cursoDA.ObtenerGruposHorarioPorCurso(cursoPlanificado.IdCurso)).ToList();
            var grupoSeleccionado = grupos.FirstOrDefault(x => x.Id == cursoPlanificado.IdGrupoHorario && x.Estado);

            if (grupoSeleccionado == null)
                throw new InvalidOperationException("El grupo horario seleccionado no existe o se encuentra inactivo.");

            var aprobados = (await _cursoAprobadoDA.ObtenerCursosAprobadosPorUsuario(usuario.Id)).ToList();
            var aprobadosIds = aprobados.Select(x => x.IdCurso).ToHashSet();

            if (aprobadosIds.Contains(cursoPlanificado.IdCurso))
                throw new InvalidOperationException("No se puede agregar un curso que ya fue aprobado.");

            var cursosPlanificados = (await _planificacionDA
                .ObtenerCursosPlanificadosPorPlanificacion(cursoPlanificado.IdPlanificacion))
                .ToList();

            if (cursosPlanificados.Any(x => x.IdCurso == cursoPlanificado.IdCurso))
                throw new InvalidOperationException("El curso ya fue agregado a la planificación.");

            var requisitos = (await _cursoDA.ObtenerRequisitosPorCurso(cursoPlanificado.IdCurso)).ToList();
            var planificadosIds = cursosPlanificados.Select(x => x.IdCurso).ToHashSet();
            var errores = new List<string>();

            foreach (var requisito in requisitos.Where(x => !x.EsCorrequisito)) {
                if (!aprobadosIds.Contains(requisito.IdCursoRequerido)) {
                    errores.Add($"No cumple el requisito {requisito.CodigoCursoRequerido} - {requisito.NombreCursoRequerido}.");
                }
            }

            foreach (var requisito in requisitos.Where(x => x.EsCorrequisito)) {
                if (!aprobadosIds.Contains(requisito.IdCursoRequerido) &&
                    !planificadosIds.Contains(requisito.IdCursoRequerido)) {
                    errores.Add($"No cumple el correquisito {requisito.CodigoCursoRequerido} - {requisito.NombreCursoRequerido}.");
                }
            }

            var bloquesSeleccionados = (await _cursoDA.ObtenerBloquesHorarioGrupo(cursoPlanificado.IdGrupoHorario)).ToList();
            if (!bloquesSeleccionados.Any()) {
                errores.Add("El grupo horario seleccionado no tiene bloques configurados.");
            }

            foreach (var bloqueExistente in cursosPlanificados) {
                foreach (var bloqueNuevo in bloquesSeleccionados) {
                    if (HayChoqueHorario(bloqueExistente.Dia, bloqueExistente.HoraInicio, bloqueExistente.HoraFin,
                        bloqueNuevo.Dia, bloqueNuevo.HoraInicio, bloqueNuevo.HoraFin)) {
                        errores.Add($"Existe choque horario con el curso {bloqueExistente.Codigo} - {bloqueExistente.NombreCurso}.");
                        return;
                    }
                }
            }


            if (errores.Any())
                throw new InvalidOperationException(string.Join(" ", errores.Distinct()));

            await _planificacionDA.AgregarCursoPlanificado(cursoPlanificado);
        }

        public async Task EliminarCursoPlanificado (Guid idPlanificacion, Guid idCurso) {
            var planificacion = await _planificacionDA.ObtenerPlanificacionPorId(idPlanificacion);
            if (planificacion == null)
                throw new InvalidOperationException("La planificación indicada no existe.");

            await _planificacionDA.EliminarCursoPlanificado(idPlanificacion, idCurso);
        }

        public async Task<IEnumerable<HorarioUsuarioResponse>> ObtenerHorarioUsuario (Guid idUsuario) {
            return await _planificacionDA.ObtenerHorarioUsuario(idUsuario);
        }

        private static bool HayChoqueHorario (
            string diaExistente,
            TimeSpan horaInicioExistente,
            TimeSpan horaFinExistente,
            string diaNuevo,
            TimeSpan horaInicioNuevo,
            TimeSpan horaFinNuevo) {
            return string.Equals(diaExistente, diaNuevo, StringComparison.OrdinalIgnoreCase)
                   && horaInicioNuevo < horaFinExistente
                   && horaInicioExistente < horaFinNuevo;
        }
    }
}
