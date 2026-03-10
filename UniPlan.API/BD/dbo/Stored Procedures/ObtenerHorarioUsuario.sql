CREATE PROCEDURE ObtenerHorarioUsuario
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.Id AS IdPlanificacion,
        p.NombrePeriodo,
        c.Codigo,
        c.Nombre AS NombreCurso,
        gh.NumeroGrupo,
        hd.Dia,
        hd.HoraInicio,
        hd.HoraFin,
        hd.Aula,
        cp.ColorHex
    FROM Planificacion p
    INNER JOIN CursoPlanificado cp
        ON p.Id = cp.IdPlanificacion
    INNER JOIN Curso c
        ON cp.IdCurso = c.Id
    LEFT JOIN GrupoHorario gh
        ON cp.IdGrupoHorario = gh.Id
    LEFT JOIN HorarioDetalle hd
        ON gh.Id = hd.IdGrupoHorario
    WHERE p.IdUsuario = @IdUsuario
      AND p.Estado = 1
    ORDER BY p.NumeroPeriodo, hd.Dia, hd.HoraInicio;
END;