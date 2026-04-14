
CREATE   PROCEDURE [dbo].[SP_Escuela_Activar]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Escuela
    SET Activo = 1
    WHERE Id = @Id;

    SELECT Id
    FROM Escuela
    WHERE Id = @Id;
END;