CREATE PROCEDURE [dbo].[SP_Carrera_Actualizar]
    @Id UNIQUEIDENTIFIER,
    @Nombre NVARCHAR(150),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Carrera
    SET Nombre = @Nombre,
        Activo = @Activo
    WHERE Id = @Id;

    SELECT Id
    FROM Carrera
    WHERE Id = @Id;
END