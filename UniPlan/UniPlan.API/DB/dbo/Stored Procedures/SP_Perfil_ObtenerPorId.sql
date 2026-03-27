CREATE PROCEDURE SP_Perfil_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre
    FROM Perfiles
    WHERE Id = @Id;
END;