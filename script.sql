IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categorias] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(100) NOT NULL,
    [Nombre] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Marcas] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Marcas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Unidades] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Unidades] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Ventas] (
    [Id] int NOT NULL IDENTITY,
    [CodigoVenta] nvarchar(max) NOT NULL,
    [Fecha] datetime2 NOT NULL,
    [Total] int NOT NULL,
    CONSTRAINT [PK_Ventas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Productos] (
    [Id] int NOT NULL IDENTITY,
    [Codigo] nvarchar(100) NOT NULL,
    [Nombre] nvarchar(100) NOT NULL,
    [MarcaId] int NOT NULL,
    [UnidadId] int NOT NULL,
    [Precio] decimal(18,2) NOT NULL,
    [Stock] int NOT NULL,
    [CategoriaId] int NOT NULL,
    CONSTRAINT [PK_Productos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Productos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Productos_Marcas_MarcaId] FOREIGN KEY ([MarcaId]) REFERENCES [Marcas] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Productos_Unidades_UnidadId] FOREIGN KEY ([UnidadId]) REFERENCES [Unidades] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [DetalleVentas] (
    [Id] int NOT NULL IDENTITY,
    [VentaId] int NOT NULL,
    [ProductoId] int NOT NULL,
    [Cantidad] int NOT NULL,
    [Precio] int NOT NULL,
    CONSTRAINT [PK_DetalleVentas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DetalleVentas_Productos_ProductoId] FOREIGN KEY ([ProductoId]) REFERENCES [Productos] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DetalleVentas_Ventas_VentaId] FOREIGN KEY ([VentaId]) REFERENCES [Ventas] ([Id]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [DetalleVenta_UQ] ON [DetalleVentas] ([VentaId], [ProductoId]);
GO

CREATE INDEX [IX_DetalleVentas_ProductoId] ON [DetalleVentas] ([ProductoId]);
GO

CREATE INDEX [IX_Productos_CategoriaId] ON [Productos] ([CategoriaId]);
GO

CREATE INDEX [IX_Productos_MarcaId] ON [Productos] ([MarcaId]);
GO

CREATE INDEX [IX_Productos_UnidadId] ON [Productos] ([UnidadId]);
GO

CREATE UNIQUE INDEX [Producto_UQ] ON [Productos] ([Codigo], [CategoriaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241029183712_inicio', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250821000004_Identity', N'8.0.10');
GO

COMMIT;
GO

