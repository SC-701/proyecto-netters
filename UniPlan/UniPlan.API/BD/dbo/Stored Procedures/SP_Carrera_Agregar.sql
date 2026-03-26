-- =============================================
-- STORED PROCEDURES - SQL Server
-- Convención de nombres: SP_[Tabla]_[Accion]
-- =============================================


-- ============================================================
--  CARRERA
-- ============================================================

CREATE   PROCEDURE SP_Carrera_Agregar
    @Nombre NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Carrera (Id, Nombre, Activo)
    VALUES (@Id, @Nombre, 1);

    SELECT Id FROM Carrera WHERE Id = @Id;
END;