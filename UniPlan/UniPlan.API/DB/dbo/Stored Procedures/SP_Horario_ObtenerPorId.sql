
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Horario_ObtenerPorId
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, HoraEntrada, HoraSalida, Dia, Activo
    FROM Horario
    WHERE Id = @Id;
END;