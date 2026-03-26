

-- ============================================================
--  CURSO
-- ============================================================

CREATE   PROCEDURE SP_Curso_Agregar
    @Sigla     NVARCHAR(20),
    @Nombre    NVARCHAR(150),
    @Creditos  INT,
    @IdEscuela UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();

    INSERT INTO Curso (Id, Sigla, Nombre, Creditos, IdEscuela, Activo)
    VALUES (@Id, @Sigla, @Nombre, @Creditos, @IdEscuela, 1);

    SELECT Id FROM Curso WHERE Id = @Id;
END;