﻿CREATE DATABASE db_hoteles
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

CREATE TABLE [Personas]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[TipoDocumento] INT NOT NULL,
	[Documento] NVARCHAR(50) NOT NULL,
	[Nombre] NVARCHAR(200) NOT NULL,
	[FechaNacimiento] SMALLDATETIME NOT NULL,
	[Celular] NVARCHAR(50) NOT NULL,
	[Genero] INT NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Email] NVARCHAR(100) NOT NULL,
	[Contraseña] NVARCHAR(100) NOT NULL,
	[Confirmar] BIT NOT NULL,
	[Restablecer] BIT NOT NULL,
	[Token] NVARCHAR(100) NULL,
	[Ciudad] INT NOT NULL,
	[Activo] BIT NOT NULL,
	CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Personas__TipoDocumentos] FOREIGN KEY ([TipoDocumento]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Personas__Generos] FOREIGN KEY ([Genero]) REFERENCES [Tipos] ([Id]) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT [FK_Personas__Ciudades] FOREIGN KEY ([Ciudad]) REFERENCES [Ciudades] ([Id]) ON DELETE No Action ON UPDATE No Action,
)
GO

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Reserva')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Reserva', 'TiposFacturas', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Reserva')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Facturas', 'TiposFacturas', 1);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Cedula')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Cedula', 'TipoDocumentos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Cedula Extranjera')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Cedula Extranjera', 'TipoDocumentos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Cedula Extranjera')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('NIT', 'TipoDocumentos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Masculino')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Masculino', 'Generos', 0);
END 

IF NOT EXISTS (SELECT 1 FROM [Tipos] WHERE [Nombre] = 'Femenino')
BEGIN
	INSERT INTO [Tipos] ([Nombre], [Tabla], [Accion])
	VALUES ('Femenino', 'Generos', 0);
END 

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

DECLARE @ciudad INT
DECLARE @tipo_documento INT
DECLARE @genero INT
IF NOT EXISTS (SELECT 1 FROM [Personas] WHERE [Documento] = '4561321')
BEGIN
	SET @ciudad = (SELECT [Id] FROM [Ciudades] WHERE [Nombre] = 'Medellin');
	SET @tipo_documento = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Cedula');
	SET @genero = (SELECT [Id] FROM [Tipos] WHERE [Nombre] = 'Masculino');

	INSERT INTO [Personas] (
		[TipoDocumento],[Documento],[Nombre],[FechaNacimiento],
		[Celular],[Genero],[Direccion],[Email],[Contraseña],
		[Confirmar],[Restablecer],[Token],[Ciudad],[Activo])
	VALUES (
		@tipo_documento, '4561321', 'Juan Esteban Rios',GETDATE(), 
		'3004567852',@genero, 'Cra 75 # 24 - 16', 'juan@email.com',
		'GJugugs54s56df', 0, 0, NULL, @ciudad, 1);
END