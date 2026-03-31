CREATE PROCEDURE [dbo].[SP_CarreraCurso_Actualizar]
    @IdCarrera UNIQUEIDENTIFIER,
    @IdCurso UNIQUEIDENTIFIER,
    @Cuatrimestre INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CarreraCurso
    SET Cuatrimestre = @Cuatrimestre
    WHERE IdCarrera = @IdCarrera 
      AND IdCurso = @IdCurso;

    SELECT @IdCarrera;
END;