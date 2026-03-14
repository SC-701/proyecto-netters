CREATE TABLE [dbo].[Usuario] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [NombreCompleto] VARCHAR (150)    NOT NULL,
    [Correo]         VARCHAR (150)    NOT NULL,
    [Contrasenna]    VARCHAR (255)    NOT NULL,
    [IdPrograma]     UNIQUEIDENTIFIER NULL,
    [Estado]         BIT              DEFAULT ((1)) NOT NULL,
    [FechaCreacion]  DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Usuario_Programa] FOREIGN KEY ([IdPrograma]) REFERENCES [dbo].[Programa] ([IdPrograma]),
    UNIQUE NONCLUSTERED ([Correo] ASC)
);

