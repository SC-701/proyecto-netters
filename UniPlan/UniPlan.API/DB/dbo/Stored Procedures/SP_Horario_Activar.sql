CREATE   PROCEDURE SP_Horario_Activar
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Horario
    SET Activo = 1
    WHERE Id = @Id;

    SELECT Id FROM Horario WHERE Id = @Id;
END;