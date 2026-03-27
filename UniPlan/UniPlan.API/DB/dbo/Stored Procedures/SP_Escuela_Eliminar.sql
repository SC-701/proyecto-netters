
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Escuela_Eliminar
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Escuela
    SET Activo = 0
    WHERE Id = @Id;

    SELECT Id FROM Escuela WHERE Id = @Id;
END;