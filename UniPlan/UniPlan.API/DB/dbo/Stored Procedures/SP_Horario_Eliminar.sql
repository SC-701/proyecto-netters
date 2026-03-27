
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Horario_Eliminar
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Horario
    SET Activo = 0
    WHERE Id = @Id;

    SELECT Id FROM Horario WHERE Id = @Id;
END;