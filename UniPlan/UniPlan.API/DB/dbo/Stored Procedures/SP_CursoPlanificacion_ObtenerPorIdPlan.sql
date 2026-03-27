
-- -----------------------------------------------------------
-- ObtenerPorIdPlan: todos los cursos de una planificación
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_CursoPlanificacion_ObtenerPorIdPlan
    @IdPlan UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  cp.IdPlan,
            cp.IdCurso,
            cu.Sigla,
            cu.Nombre       AS Curso,
            cu.Creditos,
            cp.Estado,
            cp.IdHorario,
            h.Dia,
            h.HoraEntrada,
            h.HoraSalida,
            cp.Activo
    FROM  CursoPlanificacion cp
    JOIN  Curso              cu ON cu.Id = cp.IdCurso
    JOIN  Horario            h  ON h.Id  = cp.IdHorario
    WHERE cp.IdPlan = @IdPlan
    ORDER BY cu.Sigla;
END;