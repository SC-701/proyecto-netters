
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Planificacion_Actualizar
    @Id      UNIQUEIDENTIFIER,
    @Periodo NVARCHAR(50),
    @Anio    INT,
    @Estado  NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Planificacion
    SET Periodo = @Periodo,
        Anio    = @Anio,
        Estado  = @Estado
    WHERE Id = @Id;

    SELECT Id FROM Planificacion WHERE Id = @Id;
END;