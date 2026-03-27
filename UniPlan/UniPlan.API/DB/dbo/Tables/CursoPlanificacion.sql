CREATE TABLE [dbo].[CursoPlanificacion] (
    [IdPlan]    UNIQUEIDENTIFIER NOT NULL,
    [IdCurso]   UNIQUEIDENTIFIER NOT NULL,
    [Estado]    NVARCHAR (50)    NOT NULL,
    [IdHorario] UNIQUEIDENTIFIER NOT NULL,
    [Activo]    BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CursoPlanificacion] PRIMARY KEY CLUSTERED ([IdPlan] ASC, [IdCurso] ASC),
    CONSTRAINT [FK_CursoPlani_Curso] FOREIGN KEY ([IdCurso]) REFERENCES [dbo].[Curso] ([Id]),
    CONSTRAINT [FK_CursoPlani_Horario] FOREIGN KEY ([IdHorario]) REFERENCES [dbo].[Horario] ([Id]),
    CONSTRAINT [FK_CursoPlani_Planificacion] FOREIGN KEY ([IdPlan]) REFERENCES [dbo].[Planificacion] ([Id])
);

