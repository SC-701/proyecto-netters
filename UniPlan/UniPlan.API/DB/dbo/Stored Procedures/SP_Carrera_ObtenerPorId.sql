
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Carrera_ObtenerPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre, Activo
    FROM Carrera;
END;