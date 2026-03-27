
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_CursoPlanificacion_Actualizar
    @IdPlan    UNIQUEIDENTIFIER,
    @IdCurso   UNIQUEIDENTIFIER,
    @Estado    NVARCHAR(50),
    @IdHorario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CursoPlanificacion
    SET Estado    = @Estado,
        IdHorario = @IdHorario
    WHERE IdPlan  = @IdPlan AND IdCurso = @IdCurso;

    SELECT @IdPlan AS IdPlan, @IdCurso AS IdCurso;
END;