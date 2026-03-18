
CREATE   PROCEDURE dbo.ObtenerBloquesHorarioGrupo
    @IdGrupoHorario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        hd.Id,
        hd.IdGrupoHorario,
        hd.Dia,
        hd.HoraInicio,
        hd.HoraFin,
        hd.Aula
    FROM dbo.HorarioDetalle hd
    WHERE hd.IdGrupoHorario = @IdGrupoHorario
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
        hd.HoraFin;
END;