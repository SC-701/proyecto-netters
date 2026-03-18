
CREATE   PROCEDURE dbo.ObtenerCursosPlanificadosPorPlanificacion
    @IdPlanificacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        cp.IdPlanificacion,
        cp.IdCurso,
        cp.IdGrupoHorario,
        c.Codigo,
        c.Nombre AS NombreCurso,
        hd.Dia,
        hd.HoraInicio,
        hd.HoraFin
    FROM dbo.CursoPlanificado cp
    INNER JOIN dbo.Curso c
        ON c.Id = cp.IdCurso
    INNER JOIN dbo.GrupoHorario gh
        ON gh.Id = cp.IdGrupoHorario
    INNER JOIN dbo.HorarioDetalle hd
        ON hd.IdGrupoHorario = gh.Id
    WHERE cp.IdPlanificacion = @IdPlanificacion
    ORDER BY
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