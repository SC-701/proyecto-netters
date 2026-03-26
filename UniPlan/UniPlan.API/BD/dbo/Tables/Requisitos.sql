CREATE TABLE [dbo].[Requisitos] (
    [IdCurso]          UNIQUEIDENTIFIER NOT NULL,
    [IdCursoRequisito] UNIQUEIDENTIFIER NOT NULL,
    [EsCorequisito]    BIT              DEFAULT ((0)) NOT NULL,
    [Activo]           BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Requisitos] PRIMARY KEY CLUSTERED ([IdCurso] ASC, [IdCursoRequisito] ASC),
    CONSTRAINT [FK_Requisitos_Curso] FOREIGN KEY ([IdCurso]) REFERENCES [dbo].[Curso] ([Id]),
    CONSTRAINT [FK_Requisitos_CursoReq] FOREIGN KEY ([IdCursoRequisito]) REFERENCES [dbo].[Curso] ([Id])
);

