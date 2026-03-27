
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Escuela_Actualizar
    @Id     UNIQUEIDENTIFIER,
    @Nombre NVARCHAR(150),
    @Area   NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Escuela
    SET Nombre = @Nombre,
        Area   = @Area
    WHERE Id = @Id;

    SELECT Id FROM Escuela WHERE Id = @Id;
END;