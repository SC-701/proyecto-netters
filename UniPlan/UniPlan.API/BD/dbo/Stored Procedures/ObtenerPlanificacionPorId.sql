
CREATE   PROCEDURE dbo.ObtenerPlanificacionPorId
    @IdPlanificacion UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1)
        p.Id,
        p.IdUsuario,
        p.NumeroPeriodo,
        p.NombrePeriodo,
        p.FechaCreacion,
        p.Estado
    FROM dbo.Planificacion p
    WHERE p.Id = @IdPlanificacion;
END;