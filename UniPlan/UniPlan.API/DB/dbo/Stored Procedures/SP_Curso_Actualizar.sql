
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Curso_Actualizar
    @Id        UNIQUEIDENTIFIER,
    @Sigla     NVARCHAR(20),
    @Nombre    NVARCHAR(150),
    @Creditos  INT,
    @IdEscuela UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Curso
    SET Sigla     = @Sigla,
        Nombre    = @Nombre,
        Creditos  = @Creditos,
        IdEscuela = @IdEscuela
    WHERE Id = @Id;

    SELECT Id FROM Curso WHERE Id = @Id;
END;