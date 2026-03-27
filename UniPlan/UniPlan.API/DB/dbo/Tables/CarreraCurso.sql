CREATE TABLE [dbo].[CarreraCurso] (
    [IdCarrera]    UNIQUEIDENTIFIER NOT NULL,
    [IdCurso]      UNIQUEIDENTIFIER NOT NULL,
    [Cuatrimestre] INT              NOT NULL,
    [Activo]       BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CarreraCurso] PRIMARY KEY CLUSTERED ([IdCarrera] ASC, [IdCurso] ASC),
    CONSTRAINT [FK_CarreraCurso_Carrera] FOREIGN KEY ([IdCarrera]) REFERENCES [dbo].[Carrera] ([Id]),
    CONSTRAINT [FK_CarreraCurso_Curso] FOREIGN KEY ([IdCurso]) REFERENCES [dbo].[Curso] ([Id])
);

