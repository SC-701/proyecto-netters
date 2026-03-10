CREATE TABLE [dbo].[CursoAprobado] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [IdUsuario]       UNIQUEIDENTIFIER NOT NULL,
    [IdCurso]         UNIQUEIDENTIFIER NOT NULL,
    [FechaAprobacion] DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CursoAprobado_Curso] FOREIGN KEY ([IdCurso]) REFERENCES [dbo].[Curso] ([Id]),
    CONSTRAINT [FK_CursoAprobado_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([Id])
);

