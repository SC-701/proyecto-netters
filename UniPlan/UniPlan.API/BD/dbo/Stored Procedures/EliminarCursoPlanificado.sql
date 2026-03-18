
CREATE   PROCEDURE dbo.EliminarCursoPlanificado
    @IdPlanificacion UNIQUEIDENTIFIER,
    @IdCurso UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.CursoPlanificado
    WHERE IdPlanificacion = @IdPlanificacion
      AND IdCurso = @IdCurso;
END;