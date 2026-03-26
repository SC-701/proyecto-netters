

-- ============================================================
--  ESCUELA
-- ============================================================

CREATE   PROCEDURE SP_Escuela_Agregar
    @Nombre NVARCHAR(150),
    @Area   NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Escuela (Id, Nombre, Area, Activo)
    VALUES (@Id, @Nombre, @Area, 1);

    SELECT Id FROM Escuela WHERE Id = @Id;
END;