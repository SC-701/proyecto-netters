CREATE PROCEDURE SP_Perfil_Actualizar
    @Id INT,
    @Nombre NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Perfiles
    SET 
        Nombre = @Nombre,
        FechaModificacion = GETDATE()
    WHERE Id = @Id;

    SELECT Id
    FROM Perfiles
    WHERE Id = @Id;
END;