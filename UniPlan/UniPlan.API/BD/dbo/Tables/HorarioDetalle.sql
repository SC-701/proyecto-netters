CREATE TABLE [dbo].[HorarioDetalle] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [IdGrupoHorario] UNIQUEIDENTIFIER NOT NULL,
    [Dia]            VARCHAR (20)     NOT NULL,
    [HoraInicio]     TIME (7)         NOT NULL,
    [HoraFin]        TIME (7)         NOT NULL,
    [Aula]           VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HorarioDetalle_GrupoHorario] FOREIGN KEY ([IdGrupoHorario]) REFERENCES [dbo].[GrupoHorario] ([Id])
);

