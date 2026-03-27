
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Requisitos_Eliminar
    @IdCarrera         UNIQUEIDENTIFIER,
    @IdCurso           UNIQUEIDENTIFIER,
    @IdCursoRequisito  UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Requisitos
    SET Activo = 0
    WHERE IdCarrera        = @IdCarrera
      AND IdCurso          = @IdCurso
      AND IdCursoRequisito = @IdCursoRequisito;

    SELECT @IdCarrera AS IdCarrera, @IdCurso AS IdCurso, @IdCursoRequisito AS IdCursoRequisito;
END;