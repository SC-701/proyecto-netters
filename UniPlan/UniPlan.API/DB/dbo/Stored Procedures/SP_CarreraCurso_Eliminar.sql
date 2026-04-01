CREATE PROCEDURE [dbo].[SP_CarreraCurso_Eliminar]
    @IdCarrera UNIQUEIDENTIFIER,
    @IdCurso   UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CarreraCurso
    SET Activo = 0
    WHERE IdCarrera = @IdCarrera 
      AND IdCurso = @IdCurso;

    SELECT @IdCarrera;
END;