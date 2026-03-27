

-- ============================================================
--  PLANIFICACION
--  ObtenerPorId = obtener planificaciones de un usuario
-- ============================================================

CREATE   PROCEDURE SP_Planificacion_Agregar
    @Periodo   NVARCHAR(50),
    @Anio      INT,
    @IdUsuario UNIQUEIDENTIFIER,
    @Estado    NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Planificacion (Id, Periodo, Anio, IdUsuario, Estado, Activo)
    VALUES (@Id, @Periodo, @Anio, @IdUsuario, @Estado, 1);

    SELECT Id FROM Planificacion WHERE Id = @Id;
END;