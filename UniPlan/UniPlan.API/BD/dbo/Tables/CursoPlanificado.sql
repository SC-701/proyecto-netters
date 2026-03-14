CREATE TABLE [dbo].[CursoPlanificado] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [IdPlanificacion] UNIQUEIDENTIFIER NOT NULL,
    [IdCurso]         UNIQUEIDENTIFIER NOT NULL,
    [IdGrupoHorario]  UNIQUEIDENTIFIER NOT NULL,
    [ColorHex]        VARCHAR (20)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CursoPlanificado_Curso] FOREIGN KEY ([IdCurso]) REFERENCES [dbo].[Curso] ([Id]),
    CONSTRAINT [FK_CursoPlanificado_GrupoHorario] FOREIGN KEY ([IdGrupoHorario]) REFERENCES [dbo].[GrupoHorario] ([Id]),
    CONSTRAINT [FK_CursoPlanificado_Planificacion] FOREIGN KEY ([IdPlanificacion]) REFERENCES [dbo].[Planificacion] ([Id])
);

