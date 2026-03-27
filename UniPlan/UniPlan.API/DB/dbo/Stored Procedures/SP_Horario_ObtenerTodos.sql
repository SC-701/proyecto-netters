
-- -----------------------------------------------------------

CREATE   PROCEDURE SP_Horario_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, HoraEntrada, HoraSalida, Dia, Activo
    FROM Horario
    
    ORDER BY Dia, HoraEntrada;
END;