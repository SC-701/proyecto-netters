CREATE TABLE [dbo].[Programa] (
    [IdPrograma]    UNIQUEIDENTIFIER NOT NULL,
    [Nombre]        VARCHAR (150)    NOT NULL,
    [Descripcion]   VARCHAR (400)    NULL,
    [Estado]        BIT              CONSTRAINT [DF_Programa_Estado] DEFAULT ((1)) NOT NULL,
    [FechaCreacion] DATETIME         CONSTRAINT [DF_Programa_FechaCreacion] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Programa] PRIMARY KEY CLUSTERED ([IdPrograma] ASC)
);

