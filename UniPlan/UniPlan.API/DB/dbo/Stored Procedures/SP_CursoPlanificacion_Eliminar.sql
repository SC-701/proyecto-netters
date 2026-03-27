
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_CursoPlanificacion_Eliminar
    @IdPlan  UNIQUEIDENTIFIER,
    @IdCurso UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CursoPlanificacion
    SET Activo = 0
    WHERE IdPlan = @IdPlan AND IdCurso = @IdCurso;

    SELECT @IdPlan AS IdPlan, @IdCurso AS IdCurso;
END;