CREATE   PROCEDURE SP_Curso_Activar
    @Id        UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Curso
    SET Activo = 1
    WHERE Id = @Id;

    SELECT Id FROM Curso WHERE Id = @Id;
END;