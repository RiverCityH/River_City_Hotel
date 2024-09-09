CREATE DATABASE db_hoteles
GO
USE db_hoteles;
GO

CREATE TABLE [Paises]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_Paises] PRIMARY KEY CLUSTERED ([Id])
)
GO

CREATE TABLE [Tipos]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Tabla] NVARCHAR(50) NOT NULL,
	[Accion] INT NOT NULL,
	CONSTRAINT [PK_Tipos] PRIMARY KEY CLUSTERED ([Id])
)
GO

CREATE TABLE [Departamentos]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Pais] INT NOT NULL,
	CONSTRAINT [PK_Departamentos] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Departamentos__Paises] FOREIGN KEY ([Pais]) REFERENCES [Paises] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

CREATE TABLE [Ciudades]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Departamento] INT NOT NULL,
	CONSTRAINT [PK_Ciudades] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Ciudades__Departamentos] FOREIGN KEY ([Departamento]) REFERENCES [Departamentos] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

IF NOT EXISTS (SELECT 1 FROM [Paises] WHERE [Nombre] = 'Colombia')
BEGIN
	INSERT INTO [Paises] ([Nombre])
	VALUES ('Colombia');
END 

DECLARE @pais INT
IF NOT EXISTS (SELECT 1 FROM [Departamentos] WHERE [Nombre] = 'Antioquia')
BEGIN
	SET @pais = (SELECT [Id] FROM [Paises] WHERE [Nombre] = 'Colombia');

	INSERT INTO [Departamentos] ([Nombre], [Pais])
	VALUES ('Antioquia', @pais);
END

IF NOT EXISTS (SELECT 1 FROM [Departamentos] WHERE [Nombre] = 'Cundinamarca')
BEGIN
	SET @pais = (SELECT [Id] FROM [Paises] WHERE [Nombre] = 'Colombia');

	INSERT INTO [Departamentos] ([Nombre], [Pais])
	VALUES ('Cundinamarca', @pais);
END

DECLARE @departamento INT
IF NOT EXISTS (SELECT 1 FROM [Ciudades] WHERE [Nombre] = 'Medellin')
BEGIN
	SET @departamento = (SELECT [Id] FROM [Departamentos] WHERE [Nombre] = 'Antioquia');

	INSERT INTO [Ciudades] ([Nombre], [Departamento])
	VALUES ('Medellin', @departamento);
END