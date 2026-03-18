
CREATE   PROCEDURE dbo.ObtenerPlanificacionesPorUsuario
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.Id,
        p.IdUsuario,
        p.NumeroPeriodo,
        p.NombrePeriodo,
        p.FechaCreacion,
        p.Estado
    FROM dbo.Planificacion p
    WHERE p.IdUsuario = @IdUsuario
      AND p.Estado = 1
    ORDER BY p.NumeroPeriodo, p.FechaCreacion;
END;