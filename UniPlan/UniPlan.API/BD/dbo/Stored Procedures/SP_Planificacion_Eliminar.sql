
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Planificacion_Eliminar
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Planificacion
    SET Activo = 0
    WHERE Id = @Id;

    SELECT Id FROM Planificacion WHERE Id = @Id;
END;