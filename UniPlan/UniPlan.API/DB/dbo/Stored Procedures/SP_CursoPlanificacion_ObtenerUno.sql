
-- -----------------------------------------------------------
-- ObtenerUno: Obtiene un curso específico de la planificacion
-- -----------------------------------------------------------

CREATE PROCEDURE SP_CursoPlanificacion_ObtenerUno
    @IdPlan UNIQUEIDENTIFIER,
    @IdCurso UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  IdPlan,
            IdCurso
    FROM  CursoPlanificacion
    WHERE IdPlan = @IdPlan
    AND IdCurso = @IdCurso
END;