CREATE   PROCEDURE SP_Requisitos_CambiarEstado
    @IdCarrera         UNIQUEIDENTIFIER,
    @IdCurso           UNIQUEIDENTIFIER,
    @IdCursoRequisito  UNIQUEIDENTIFIER,
    @Activo            BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Requisitos
    SET Activo = @Activo
    WHERE IdCarrera = @IdCarrera
      AND IdCurso = @IdCurso
      AND IdCursoRequisito = @IdCursoRequisito;

    SELECT @IdCarrera AS IdCarrera,
           @IdCurso AS IdCurso,
           @IdCursoRequisito AS IdCursoRequisito;
END;