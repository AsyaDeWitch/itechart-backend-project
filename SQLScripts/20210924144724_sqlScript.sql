Build started...
Build succeeded.
The Entity Framework tools version '5.0.9' is older than that of the runtime '5.0.10'. Update the tools for the latest features and bug fixes.
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

CREATE TABLE [AspNetRoles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] int NOT NULL IDENTITY,
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
    [RoleId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
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
    [UserId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] int NOT NULL,
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
VALUES (N'20210902115449_initial', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [AddressDeliveryId] int NULL;
GO

CREATE TABLE [Address] (
    [Id] int NOT NULL IDENTITY,
    [Country] nvarchar(200) NOT NULL,
    [City] nvarchar(200) NOT NULL,
    [Street] nvarchar(200) NOT NULL,
    [HouseNumber] int NOT NULL,
    [HouseBuilding] nvarchar(20) NULL DEFAULT N'-',
    [EntranceNumber] int NOT NULL DEFAULT 1,
    [FloorNumber] int NOT NULL,
    [FlatNumber] int NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_AspNetUsers_AddressDeliveryId] ON [AspNetUsers] ([AddressDeliveryId]);
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Address_AddressDeliveryId] FOREIGN KEY ([AddressDeliveryId]) REFERENCES [Address] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210913084903_User_Extension', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Address_AddressDeliveryId];
GO

DROP INDEX [IX_AspNetUsers_AddressDeliveryId] ON [AspNetUsers];
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'AddressDeliveryId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [AddressDeliveryId] int NOT NULL;
ALTER TABLE [AspNetUsers] ADD DEFAULT 0 FOR [AddressDeliveryId];
CREATE INDEX [IX_AspNetUsers_AddressDeliveryId] ON [AspNetUsers] ([AddressDeliveryId]);
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Address_AddressDeliveryId] FOREIGN KEY ([AddressDeliveryId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210913104516_User_Extension_1', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Address_AddressDeliveryId];
GO

ALTER TABLE [Address] DROP CONSTRAINT [PK_Address];
GO

EXEC sp_rename N'[Address]', N'Addresses';
GO

ALTER TABLE [Addresses] ADD CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id]);
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Addresses_AddressDeliveryId] FOREIGN KEY ([AddressDeliveryId]) REFERENCES [Addresses] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210913113654_User_Extension_2', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [Platform] int NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [TotalRating] float NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Products_DateCreated] ON [Products] ([DateCreated]);
GO

CREATE INDEX [IX_Products_Name] ON [Products] ([Name]);
GO

CREATE INDEX [IX_Products_Platform] ON [Products] ([Platform]);
GO

CREATE INDEX [IX_Products_TotalRating] ON [Products] ([TotalRating]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210915143502_Add_Product', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'Name', N'Platform', N'TotalRating') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] ON;
INSERT INTO [Products] ([Id], [DateCreated], [Name], [Platform], [TotalRating])
VALUES (1, '2015-05-18T00:00:00.0000000', N'The Witcher 3: Wild Hunt', 0, 9.3000000000000007E0),
(23, '2016-10-28T00:00:00.0000000', N'The Elder Scrolls V: Skyrim', 7, 7.7000000000000002E0),
(22, '2011-11-11T00:00:00.0000000', N'The Elder Scrolls V: Skyrim', 3, 9.5999999999999996E0),
(21, '2011-11-11T00:00:00.0000000', N'The Elder Scrolls V: Skyrim', 6, 9.1999999999999993E0),
(20, '2011-11-11T00:00:00.0000000', N'The Elder Scrolls V: Skyrim', 0, 9.4000000000000004E0),
(19, '2018-11-05T00:00:00.0000000', N'Red Dead Redemption 2', 0, 9.3000000000000007E0),
(18, '2018-10-26T00:00:00.0000000', N'Red Dead Redemption 2', 4, 9.6999999999999993E0),
(17, '2018-10-26T00:00:00.0000000', N'Red Dead Redemption 2', 7, 9.6999999999999993E0),
(16, '2008-11-18T00:00:00.0000000', N'World of Warcraft: Wrath of the Lich King', 1, 9.0999999999999996E0),
(15, '2008-11-18T00:00:00.0000000', N'World of Warcraft: Wrath of the Lich King', 0, 9.0999999999999996E0),
(14, '2015-04-14T00:00:00.0000000', N'Grand Theft Auto V', 0, 9.5999999999999996E0),
(24, '2016-10-28T00:00:00.0000000', N'The Elder Scrolls V: Skyrim', 4, 8.1999999999999993E0),
(13, '2014-11-18T00:00:00.0000000', N'Grand Theft Auto V', 4, 9.6999999999999993E0),
(11, '2013-09-17T00:00:00.0000000', N'Grand Theft Auto V', 3, 9.6999999999999993E0),
(10, '2013-09-17T00:00:00.0000000', N'Grand Theft Auto V', 6, 9.6999999999999993E0),
(9, '1999-12-21T00:00:00.0000000', N'Heroes of Might and Magic III', 2, 9.1999999999999993E0),
(8, '1999-12-20T00:00:00.0000000', N'Heroes of Might and Magic III', 1, 9.1999999999999993E0),
(7, '1999-02-28T00:00:00.0000000', N'Heroes of Might and Magic III', 0, 9.1999999999999993E0),
(6, '2009-06-02T00:00:00.0000000', N'The Sims 3', 1, 8.5999999999999996E0),
(5, '2009-06-02T00:00:00.0000000', N'The Sims 3', 0, 8.5999999999999996E0),
(4, '2019-10-15T00:00:00.0000000', N'The Witcher 3: Wild Hunt', 9, 8.5E0),
(3, '2015-05-18T00:00:00.0000000', N'The Witcher 3: Wild Hunt', 4, 9.0999999999999996E0),
(2, '2015-05-18T00:00:00.0000000', N'The Witcher 3: Wild Hunt', 7, 9.1999999999999993E0),
(12, '2014-11-18T00:00:00.0000000', N'Grand Theft Auto V', 7, 9.6999999999999993E0),
(25, '2017-11-17T00:00:00.0000000', N'The Elder Scrolls V: Skyrim', 9, 8.4000000000000004E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DateCreated', N'Name', N'Platform', N'TotalRating') AND [object_id] = OBJECT_ID(N'[Products]'))
    SET IDENTITY_INSERT [Products] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210915155103_Add_Products_Test_Data', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Addresses_AddressDeliveryId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'AddressDeliveryId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AspNetUsers] ALTER COLUMN [AddressDeliveryId] int NULL;
GO

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Addresses_AddressDeliveryId] FOREIGN KEY ([AddressDeliveryId]) REFERENCES [Addresses] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210917090631_Extended_User_Address_Allow_Null', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] ADD [Background] nvarchar(max) NOT NULL DEFAULT N'https://firebasestorage.googleapis.com/v0/b/lab-web-app-299ef.appspot.com/o/empty_background.jpg?alt=media&token=0ad4ed61-2a4e-4b48-be4b-d683282d5fc5e';
GO

ALTER TABLE [Products] ADD [Count] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Products] ADD [Genre] nvarchar(100) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Products] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Products] ADD [Logo] nvarchar(max) NOT NULL DEFAULT N'https://firebasestorage.googleapis.com/v0/b/lab-web-app-299ef.appspot.com/o/empty_image.png?alt=media&token=fdbc866c-69d0-458e-a162-29a17eca00fe';
GO

ALTER TABLE [Products] ADD [Price] float NOT NULL DEFAULT 0.0E0;
GO

ALTER TABLE [Products] ADD [Rating] int NOT NULL DEFAULT 0;
GO

UPDATE [Products] SET [Count] = 10, [Genre] = N'RPG', [Price] = 19.989999999999998E0, [Rating] = 3
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 5, [Genre] = N'RPG', [Price] = 19.989999999999998E0, [Rating] = 3
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 7, [Genre] = N'RPG', [Price] = 19.989999999999998E0, [Rating] = 3
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 8, [Genre] = N'RPG', [Price] = 19.989999999999998E0, [Rating] = 3
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 8, [Genre] = N'Simulation', [Price] = 19.989999999999998E0, [Rating] = 1
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 13, [Genre] = N'Simulation', [Price] = 19.989999999999998E0, [Rating] = 1
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 6, [Genre] = N'Turn-based strategy with RPG elements', [Price] = 8.25E0
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 3, [Genre] = N'Turn-based strategy with RPG elements', [Price] = 8.25E0
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 1, [Genre] = N'Turn-based strategy with RPG elements', [Price] = 8.25E0
WHERE [Id] = 9;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 6, [Genre] = N'Action-adventure', [Price] = 12.58E0, [Rating] = 3
WHERE [Id] = 10;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 8, [Genre] = N'Action-adventure', [Price] = 12.58E0, [Rating] = 3
WHERE [Id] = 11;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 12, [Genre] = N'Action-adventure', [Price] = 12.58E0, [Rating] = 3
WHERE [Id] = 12;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 4, [Genre] = N'Action-adventure', [Price] = 12.58E0, [Rating] = 3
WHERE [Id] = 13;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 2, [Genre] = N'Action-adventure', [Price] = 12.58E0, [Rating] = 3
WHERE [Id] = 14;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 2, [Genre] = N'RPG', [Price] = 40.450000000000003E0, [Rating] = 1
WHERE [Id] = 15;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 3, [Genre] = N'RPG', [Price] = 40.450000000000003E0, [Rating] = 1
WHERE [Id] = 16;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 25, [Genre] = N'Action-adventure', [Price] = 34.990000000000002E0, [Rating] = 3
WHERE [Id] = 17;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 2, [Genre] = N'Action-adventure', [Price] = 34.990000000000002E0, [Rating] = 3
WHERE [Id] = 18;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 14, [Genre] = N'Action-adventure', [Price] = 34.990000000000002E0, [Rating] = 3
WHERE [Id] = 19;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 3, [Genre] = N'RPG', [Price] = 7.9900000000000002E0, [Rating] = 3
WHERE [Id] = 20;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 7, [Genre] = N'RPG', [Price] = 7.9900000000000002E0, [Rating] = 3
WHERE [Id] = 21;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 5, [Genre] = N'RPG', [Price] = 7.9900000000000002E0, [Rating] = 3
WHERE [Id] = 22;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 3, [Genre] = N'RPG', [Price] = 7.9900000000000002E0, [Rating] = 3
WHERE [Id] = 23;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 5, [Genre] = N'RPG', [Price] = 7.9900000000000002E0, [Rating] = 3
WHERE [Id] = 24;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Count] = 4, [Genre] = N'RPG', [Price] = 7.9900000000000002E0, [Rating] = 3
WHERE [Id] = 25;
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210920124323_Product_Extension', N'5.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Genre');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Products] ALTER COLUMN [Genre] int NOT NULL;
GO

CREATE TABLE [ProductRatings] (
    [ProductId] int NOT NULL,
    [UserId] int NOT NULL,
    [Rating] float NOT NULL DEFAULT 10.0E0,
    CONSTRAINT [PK_ProductRatings] PRIMARY KEY ([ProductId], [UserId]),
    CONSTRAINT [FK_ProductRatings_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductRatings_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 5
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 5
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 13
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 13
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 13
WHERE [Id] = 9;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 9
WHERE [Id] = 10;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 9
WHERE [Id] = 11;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 9
WHERE [Id] = 12;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 9
WHERE [Id] = 13;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 9
WHERE [Id] = 14;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 15;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 16;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 9
WHERE [Id] = 17;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 9
WHERE [Id] = 18;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 9
WHERE [Id] = 19;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 20;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 21;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 22;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 23;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 24;
SELECT @@ROWCOUNT;

GO

UPDATE [Products] SET [Genre] = 4
WHERE [Id] = 25;
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Products_Genre] ON [Products] ([Genre]);
GO

CREATE INDEX [IX_Products_Price] ON [Products] ([Price]);
GO

CREATE INDEX [IX_Products_Rating] ON [Products] ([Rating]);
GO

CREATE INDEX [IX_ProductRatings_UserId] ON [ProductRatings] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210923131045_Add_ProductRatings', N'5.0.10');
GO

COMMIT;
GO


