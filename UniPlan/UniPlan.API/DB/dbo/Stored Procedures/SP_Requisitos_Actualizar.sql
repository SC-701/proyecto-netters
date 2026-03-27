
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Requisitos_Actualizar
    @IdCarrera         UNIQUEIDENTIFIER,
    @IdCurso           UNIQUEIDENTIFIER,
    @IdCursoRequisito  UNIQUEIDENTIFIER,
    @EsCorequisito     BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Requisitos
    SET EsCorequisito = @EsCorequisito
    WHERE IdCarrera        = @IdCarrera
      AND IdCurso          = @IdCurso
      AND IdCursoRequisito = @IdCursoRequisito
      AND Activo           = 1;

    SELECT @IdCarrera AS IdCarrera, @IdCurso AS IdCurso, @IdCursoRequisito AS IdCursoRequisito;
END;