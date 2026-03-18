
CREATE   PROCEDURE dbo.ObtenerHorarioUsuario
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.Id AS IdPlanificacion,
        p.NombrePeriodo,
        c.Codigo,
        c.Nombre AS NombreCurso,
        ISNULL(gh.NumeroGrupo, '') AS NumeroGrupo,
        hd.Dia,
        hd.HoraInicio,
        hd.HoraFin,
        ISNULL(hd.Aula, '') AS Aula,
        ISNULL(cp.ColorHex, '') AS ColorHex
    FROM dbo.Planificacion p
    INNER JOIN dbo.CursoPlanificado cp
        ON p.Id = cp.IdPlanificacion
    INNER JOIN dbo.Curso c
        ON cp.IdCurso = c.Id
    LEFT JOIN dbo.GrupoHorario gh
        ON cp.IdGrupoHorario = gh.Id
    LEFT JOIN dbo.HorarioDetalle hd
        ON gh.Id = hd.IdGrupoHorario
    WHERE p.IdUsuario = @IdUsuario
      AND p.Estado = 1
    ORDER BY
        p.NumeroPeriodo,
        CASE UPPER(hd.Dia)
            WHEN 'LUNES' THEN 1
            WHEN 'MARTES' THEN 2
            WHEN 'MIERCOLES' THEN 3
            WHEN 'MIÉRCOLES' THEN 3
            WHEN 'JUEVES' THEN 4
            WHEN 'VIERNES' THEN 5
            WHEN 'SABADO' THEN 6
            WHEN 'SÁBADO' THEN 6
            WHEN 'DOMINGO' THEN 7
            ELSE 99
        END,
        hd.HoraInicio,
        c.Codigo;
END;