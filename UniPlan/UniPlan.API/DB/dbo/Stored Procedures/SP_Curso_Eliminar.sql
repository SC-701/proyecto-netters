
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Curso_Eliminar
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Curso
    SET Activo = 0
    WHERE Id = @Id;

    SELECT Id FROM Curso WHERE Id = @Id;
END;