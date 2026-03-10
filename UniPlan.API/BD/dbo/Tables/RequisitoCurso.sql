CREATE TABLE [dbo].[RequisitoCurso] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [IdCurso]          UNIQUEIDENTIFIER NOT NULL,
    [IdCursoRequerido] UNIQUEIDENTIFIER NOT NULL,
    [EsCorrequisito]   BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequisitoCurso_Curso] FOREIGN KEY ([IdCurso]) REFERENCES [dbo].[Curso] ([Id]),
    CONSTRAINT [FK_RequisitoCurso_CursoRequerido] FOREIGN KEY ([IdCursoRequerido]) REFERENCES [dbo].[Curso] ([Id])
);

