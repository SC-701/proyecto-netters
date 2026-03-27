
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Escuela_ObtenerPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre, Area, Activo
    FROM Escuela
    WHERE Id = @Id;
END;