IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Administrador] (
    [Id] int NOT NULL IDENTITY,
    [CompletoNombre] nvarchar(250) NOT NULL,
    [ConfirmPassword] nvarchar(max) NULL,
    [Email] nvarchar(255) NOT NULL,
    [Genero] nvarchar(50) NOT NULL,
    [Password] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Administrador] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [CompletoNombre] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Email] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [PasswordHash] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [RegisterViewModel] (
    [Id] int NOT NULL IDENTITY,
    [CompletoNombre] nvarchar(250) NOT NULL,
    [ConfirmPassword] nvarchar(max) NULL,
    [Email] nvarchar(255) NOT NULL,
    [Password] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_RegisterViewModel] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
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

CREATE TABLE [Empresas] (
    [Id] int NOT NULL IDENTITY,
    [Ciudad] nvarchar(250) NOT NULL,
    [Email] nvarchar(255) NOT NULL,
    [Estado] nvarchar(250) NOT NULL,
    [Nombre] nvarchar(100) NOT NULL,
    [Pais] nvarchar(250) NOT NULL,
    [RegisterId] nvarchar(max) NOT NULL,
    [RegisterViewModelId] int NULL,
    [Tipo] nvarchar(200) NOT NULL,
    CONSTRAINT [PK_Empresas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Empresas_RegisterViewModel_RegisterViewModelId] FOREIGN KEY ([RegisterViewModelId]) REFERENCES [RegisterViewModel] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Productos] (
    [Id] int NOT NULL IDENTITY,
    [Cantidad] int NOT NULL,
    [Categoria] nvarchar(50) NOT NULL,
    [Descripcion] nvarchar(400) NOT NULL,
    [EmpresaId] int NOT NULL,
    [FechaIngreso] datetime2 NOT NULL,
    [Nombre] nvarchar(50) NOT NULL,
    [Peso] float NOT NULL,
    [Precion] float NOT NULL,
    [Tipo] nvarchar(250) NOT NULL,
    CONSTRAINT [PK_Productos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Productos_Empresas_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresas] ([Id]) ON DELETE CASCADE
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

CREATE INDEX [IX_Empresas_RegisterViewModelId] ON [Empresas] ([RegisterViewModelId]);

GO

CREATE INDEX [IX_Productos_EmpresaId] ON [Productos] ([EmpresaId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180405060729_MyMigration', N'2.0.1-rtm-125');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Empresas') AND [c].[name] = N'RegisterId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Empresas] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Empresas] ALTER COLUMN [RegisterId] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180406104216_2Migration', N'2.0.1-rtm-125');

GO

ALTER TABLE [Productos] DROP CONSTRAINT [FK_Productos_Empresas_EmpresaId];

GO

DROP INDEX [IX_Productos_EmpresaId] ON [Productos];

GO

ALTER TABLE [Productos] ADD [RegisterId] nvarchar(max) NULL;

GO

ALTER TABLE [Productos] ADD [RegisterViewModelId] int NULL;

GO

CREATE INDEX [IX_Productos_RegisterViewModelId] ON [Productos] ([RegisterViewModelId]);

GO

ALTER TABLE [Productos] ADD CONSTRAINT [FK_Productos_RegisterViewModel_RegisterViewModelId] FOREIGN KEY ([RegisterViewModelId]) REFERENCES [RegisterViewModel] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180409000202_3Migration', N'2.0.1-rtm-125');

GO

