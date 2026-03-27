
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Carrera_Eliminar
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Carrera
    SET Activo = 0
    WHERE Id = @Id;

    SELECT Id FROM Carrera WHERE Id = @Id;
END;