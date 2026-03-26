
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_CursoPlanificacion_ObtenerTodos
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
    ORDER BY cp.IdPlan, cu.Sigla;
END;