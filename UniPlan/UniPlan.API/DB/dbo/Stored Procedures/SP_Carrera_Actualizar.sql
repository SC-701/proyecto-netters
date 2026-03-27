
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Carrera_Actualizar
    @Id     UNIQUEIDENTIFIER,
    @Nombre NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Carrera
    SET Nombre = @Nombre
    WHERE Id = @Id;

    SELECT Id FROM Carrera WHERE Id = @Id;
END;