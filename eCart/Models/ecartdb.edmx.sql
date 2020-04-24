
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/23/2020 21:26:20
-- Generated from EDMX file: D:\Company\2020.RealSys\eCart\eCart\Models\ecartdb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
--USE [ecartdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserStatusUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_UserStatusUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreStatusStoreDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreDetails] DROP CONSTRAINT [FK_StoreStatusStoreDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreCategoryStoreDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreDetails] DROP CONSTRAINT [FK_StoreCategoryStoreDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCityStoreDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreDetails] DROP CONSTRAINT [FK_MasterCityStoreDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCityMasterBarangay]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MasterAreas] DROP CONSTRAINT [FK_MasterCityMasterBarangay];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterCityUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_MasterCityUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemCategoryStoreItemCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemMasterCategories] DROP CONSTRAINT [FK_ItemCategoryStoreItemCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemMasterItemMasterCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemMasterCategories] DROP CONSTRAINT [FK_ItemMasterItemMasterCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemMasterStoreItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreItems] DROP CONSTRAINT [FK_ItemMasterStoreItem];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailStoreItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreItems] DROP CONSTRAINT [FK_StoreDetailStoreItem];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDetailCartDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDetails] DROP CONSTRAINT [FK_UserDetailCartDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreDetailCartDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDetails] DROP CONSTRAINT [FK_StoreDetailCartDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDetailCartItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartItems] DROP CONSTRAINT [FK_CartDetailCartItem];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreItemCartItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartItems] DROP CONSTRAINT [FK_StoreItemCartItem];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterAreaUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_MasterAreaUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_MasterAreaStoreDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreDetails] DROP CONSTRAINT [FK_MasterAreaStoreDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartStatusCartDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartDetails] DROP CONSTRAINT [FK_CartStatusCartDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CartStatusCartHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartHistories] DROP CONSTRAINT [FK_CartStatusCartHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_CartDetailCartHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartHistories] DROP CONSTRAINT [FK_CartDetailCartHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_CartItemStatusCartItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartItems] DROP CONSTRAINT [FK_CartItemStatusCartItem];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemCatGroupItemCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemCategories] DROP CONSTRAINT [FK_ItemCatGroupItemCategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetails];
GO
IF OBJECT_ID(N'[dbo].[StoreDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreDetails];
GO
IF OBJECT_ID(N'[dbo].[UserStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserStatus];
GO
IF OBJECT_ID(N'[dbo].[StoreStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreStatus];
GO
IF OBJECT_ID(N'[dbo].[StoreCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreCategories];
GO
IF OBJECT_ID(N'[dbo].[MasterCities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MasterCities];
GO
IF OBJECT_ID(N'[dbo].[MasterAreas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MasterAreas];
GO
IF OBJECT_ID(N'[dbo].[ItemMasters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemMasters];
GO
IF OBJECT_ID(N'[dbo].[ItemCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemCategories];
GO
IF OBJECT_ID(N'[dbo].[ItemMasterCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemMasterCategories];
GO
IF OBJECT_ID(N'[dbo].[StoreItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreItems];
GO
IF OBJECT_ID(N'[dbo].[CartDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartDetails];
GO
IF OBJECT_ID(N'[dbo].[CartItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartItems];
GO
IF OBJECT_ID(N'[dbo].[CartStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartStatus];
GO
IF OBJECT_ID(N'[dbo].[CartHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartHistories];
GO
IF OBJECT_ID(N'[dbo].[CartItemStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartItemStatus];
GO
IF OBJECT_ID(N'[dbo].[ItemCatGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemCatGroups];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(20)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address] nvarchar(150)  NULL,
    [Email] nvarchar(30)  NULL,
    [Mobile] nvarchar(20)  NOT NULL,
    [Remarks] nvarchar(50)  NULL,
    [UserStatusId] int  NOT NULL,
    [MasterCityId] int  NOT NULL,
    [MasterAreaId] int  NOT NULL
);
GO

-- Creating table 'StoreDetails'
CREATE TABLE [dbo].[StoreDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LoginId] nvarchar(20)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address] nvarchar(150)  NOT NULL,
    [Remarks] nvarchar(80)  NOT NULL,
    [StoreStatusId] int  NOT NULL,
    [StoreCategoryId] int  NOT NULL,
    [MasterCityId] int  NOT NULL,
    [MasterAreaId] int  NOT NULL
);
GO

-- Creating table 'UserStatus'
CREATE TABLE [dbo].[UserStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(15)  NOT NULL
);
GO

-- Creating table 'StoreStatus'
CREATE TABLE [dbo].[StoreStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'StoreCategories'
CREATE TABLE [dbo].[StoreCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [SortOrder] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MasterCities'
CREATE TABLE [dbo].[MasterCities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'MasterAreas'
CREATE TABLE [dbo].[MasterAreas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL,
    [MasterCityId] int  NOT NULL
);
GO

-- Creating table 'ItemMasters'
CREATE TABLE [dbo].[ItemMasters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(80)  NOT NULL
);
GO

-- Creating table 'ItemCategories'
CREATE TABLE [dbo].[ItemCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [SortOrder] int  NOT NULL,
    [ItemCatGroupId] int  NOT NULL
);
GO

-- Creating table 'ItemMasterCategories'
CREATE TABLE [dbo].[ItemMasterCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemCategoryId] int  NOT NULL,
    [ItemMasterId] int  NOT NULL
);
GO

-- Creating table 'StoreItems'
CREATE TABLE [dbo].[StoreItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ItemMasterId] int  NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [UnitPrice] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'CartDetails'
CREATE TABLE [dbo].[CartDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserDetailId] int  NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [CartStatusId] int  NOT NULL,
    [StorePickupPointId] int  NOT NULL
);
GO

-- Creating table 'CartItems'
CREATE TABLE [dbo].[CartItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CartDetailId] int  NOT NULL,
    [StoreItemId] int  NOT NULL,
    [ItemQty] decimal(18,0)  NOT NULL,
    [ItemOrder] nvarchar(max)  NOT NULL,
    [CartItemStatusId] int  NOT NULL,
    [Remarks1] nvarchar(80)  NULL,
    [Remarks2] nvarchar(80)  NULL
);
GO

-- Creating table 'CartStatus'
CREATE TABLE [dbo].[CartStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'CartHistories'
CREATE TABLE [dbo].[CartHistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CartStatusId] int  NOT NULL,
    [CartDetailId] int  NOT NULL,
    [UserId] nvarchar(20)  NOT NULL,
    [dtStatus] datetime  NOT NULL
);
GO

-- Creating table 'CartItemStatus'
CREATE TABLE [dbo].[CartItemStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'ItemCatGroups'
CREATE TABLE [dbo].[ItemCatGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [SortOrder] int  NOT NULL
);
GO

-- Creating table 'StorePickupPoints'
CREATE TABLE [dbo].[StorePickupPoints] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StoreDetailId] int  NOT NULL,
    [Address] nvarchar(250)  NOT NULL,
    [Remarks] nvarchar(150)  NULL,
    [StorePickupStatusId] int  NOT NULL
);
GO

-- Creating table 'StorePickupPartners'
CREATE TABLE [dbo].[StorePickupPartners] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StorePickupPointId] int  NOT NULL,
    [StoreDetailId] int  NOT NULL
);
GO

-- Creating table 'StorePickupStatus'
CREATE TABLE [dbo].[StorePickupStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [PK_UserDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [PK_StoreDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserStatus'
ALTER TABLE [dbo].[UserStatus]
ADD CONSTRAINT [PK_UserStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreStatus'
ALTER TABLE [dbo].[StoreStatus]
ADD CONSTRAINT [PK_StoreStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreCategories'
ALTER TABLE [dbo].[StoreCategories]
ADD CONSTRAINT [PK_StoreCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MasterCities'
ALTER TABLE [dbo].[MasterCities]
ADD CONSTRAINT [PK_MasterCities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MasterAreas'
ALTER TABLE [dbo].[MasterAreas]
ADD CONSTRAINT [PK_MasterAreas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemMasters'
ALTER TABLE [dbo].[ItemMasters]
ADD CONSTRAINT [PK_ItemMasters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemCategories'
ALTER TABLE [dbo].[ItemCategories]
ADD CONSTRAINT [PK_ItemCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemMasterCategories'
ALTER TABLE [dbo].[ItemMasterCategories]
ADD CONSTRAINT [PK_ItemMasterCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [PK_StoreItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [PK_CartDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartItems'
ALTER TABLE [dbo].[CartItems]
ADD CONSTRAINT [PK_CartItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartStatus'
ALTER TABLE [dbo].[CartStatus]
ADD CONSTRAINT [PK_CartStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartHistories'
ALTER TABLE [dbo].[CartHistories]
ADD CONSTRAINT [PK_CartHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartItemStatus'
ALTER TABLE [dbo].[CartItemStatus]
ADD CONSTRAINT [PK_CartItemStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ItemCatGroups'
ALTER TABLE [dbo].[ItemCatGroups]
ADD CONSTRAINT [PK_ItemCatGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePickupPoints'
ALTER TABLE [dbo].[StorePickupPoints]
ADD CONSTRAINT [PK_StorePickupPoints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePickupPartners'
ALTER TABLE [dbo].[StorePickupPartners]
ADD CONSTRAINT [PK_StorePickupPartners]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StorePickupStatus'
ALTER TABLE [dbo].[StorePickupStatus]
ADD CONSTRAINT [PK_StorePickupStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserStatusId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_UserStatusUserDetail]
    FOREIGN KEY ([UserStatusId])
    REFERENCES [dbo].[UserStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStatusUserDetail'
CREATE INDEX [IX_FK_UserStatusUserDetail]
ON [dbo].[UserDetails]
    ([UserStatusId]);
GO

-- Creating foreign key on [StoreStatusId] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [FK_StoreStatusStoreDetail]
    FOREIGN KEY ([StoreStatusId])
    REFERENCES [dbo].[StoreStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreStatusStoreDetail'
CREATE INDEX [IX_FK_StoreStatusStoreDetail]
ON [dbo].[StoreDetails]
    ([StoreStatusId]);
GO

-- Creating foreign key on [StoreCategoryId] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [FK_StoreCategoryStoreDetail]
    FOREIGN KEY ([StoreCategoryId])
    REFERENCES [dbo].[StoreCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreCategoryStoreDetail'
CREATE INDEX [IX_FK_StoreCategoryStoreDetail]
ON [dbo].[StoreDetails]
    ([StoreCategoryId]);
GO

-- Creating foreign key on [MasterCityId] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [FK_MasterCityStoreDetail]
    FOREIGN KEY ([MasterCityId])
    REFERENCES [dbo].[MasterCities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCityStoreDetail'
CREATE INDEX [IX_FK_MasterCityStoreDetail]
ON [dbo].[StoreDetails]
    ([MasterCityId]);
GO

-- Creating foreign key on [MasterCityId] in table 'MasterAreas'
ALTER TABLE [dbo].[MasterAreas]
ADD CONSTRAINT [FK_MasterCityMasterBarangay]
    FOREIGN KEY ([MasterCityId])
    REFERENCES [dbo].[MasterCities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCityMasterBarangay'
CREATE INDEX [IX_FK_MasterCityMasterBarangay]
ON [dbo].[MasterAreas]
    ([MasterCityId]);
GO

-- Creating foreign key on [MasterCityId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_MasterCityUserDetail]
    FOREIGN KEY ([MasterCityId])
    REFERENCES [dbo].[MasterCities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCityUserDetail'
CREATE INDEX [IX_FK_MasterCityUserDetail]
ON [dbo].[UserDetails]
    ([MasterCityId]);
GO

-- Creating foreign key on [ItemCategoryId] in table 'ItemMasterCategories'
ALTER TABLE [dbo].[ItemMasterCategories]
ADD CONSTRAINT [FK_ItemCategoryStoreItemCategory]
    FOREIGN KEY ([ItemCategoryId])
    REFERENCES [dbo].[ItemCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemCategoryStoreItemCategory'
CREATE INDEX [IX_FK_ItemCategoryStoreItemCategory]
ON [dbo].[ItemMasterCategories]
    ([ItemCategoryId]);
GO

-- Creating foreign key on [ItemMasterId] in table 'ItemMasterCategories'
ALTER TABLE [dbo].[ItemMasterCategories]
ADD CONSTRAINT [FK_ItemMasterItemMasterCategory]
    FOREIGN KEY ([ItemMasterId])
    REFERENCES [dbo].[ItemMasters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemMasterItemMasterCategory'
CREATE INDEX [IX_FK_ItemMasterItemMasterCategory]
ON [dbo].[ItemMasterCategories]
    ([ItemMasterId]);
GO

-- Creating foreign key on [ItemMasterId] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [FK_ItemMasterStoreItem]
    FOREIGN KEY ([ItemMasterId])
    REFERENCES [dbo].[ItemMasters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemMasterStoreItem'
CREATE INDEX [IX_FK_ItemMasterStoreItem]
ON [dbo].[StoreItems]
    ([ItemMasterId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [FK_StoreDetailStoreItem]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStoreItem'
CREATE INDEX [IX_FK_StoreDetailStoreItem]
ON [dbo].[StoreItems]
    ([StoreDetailId]);
GO

-- Creating foreign key on [UserDetailId] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [FK_UserDetailCartDetail]
    FOREIGN KEY ([UserDetailId])
    REFERENCES [dbo].[UserDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDetailCartDetail'
CREATE INDEX [IX_FK_UserDetailCartDetail]
ON [dbo].[CartDetails]
    ([UserDetailId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [FK_StoreDetailCartDetail]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailCartDetail'
CREATE INDEX [IX_FK_StoreDetailCartDetail]
ON [dbo].[CartDetails]
    ([StoreDetailId]);
GO

-- Creating foreign key on [CartDetailId] in table 'CartItems'
ALTER TABLE [dbo].[CartItems]
ADD CONSTRAINT [FK_CartDetailCartItem]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDetailCartItem'
CREATE INDEX [IX_FK_CartDetailCartItem]
ON [dbo].[CartItems]
    ([CartDetailId]);
GO

-- Creating foreign key on [StoreItemId] in table 'CartItems'
ALTER TABLE [dbo].[CartItems]
ADD CONSTRAINT [FK_StoreItemCartItem]
    FOREIGN KEY ([StoreItemId])
    REFERENCES [dbo].[StoreItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreItemCartItem'
CREATE INDEX [IX_FK_StoreItemCartItem]
ON [dbo].[CartItems]
    ([StoreItemId]);
GO

-- Creating foreign key on [MasterAreaId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_MasterAreaUserDetail]
    FOREIGN KEY ([MasterAreaId])
    REFERENCES [dbo].[MasterAreas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterAreaUserDetail'
CREATE INDEX [IX_FK_MasterAreaUserDetail]
ON [dbo].[UserDetails]
    ([MasterAreaId]);
GO

-- Creating foreign key on [MasterAreaId] in table 'StoreDetails'
ALTER TABLE [dbo].[StoreDetails]
ADD CONSTRAINT [FK_MasterAreaStoreDetail]
    FOREIGN KEY ([MasterAreaId])
    REFERENCES [dbo].[MasterAreas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterAreaStoreDetail'
CREATE INDEX [IX_FK_MasterAreaStoreDetail]
ON [dbo].[StoreDetails]
    ([MasterAreaId]);
GO

-- Creating foreign key on [CartStatusId] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [FK_CartStatusCartDetail]
    FOREIGN KEY ([CartStatusId])
    REFERENCES [dbo].[CartStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartStatusCartDetail'
CREATE INDEX [IX_FK_CartStatusCartDetail]
ON [dbo].[CartDetails]
    ([CartStatusId]);
GO

-- Creating foreign key on [CartStatusId] in table 'CartHistories'
ALTER TABLE [dbo].[CartHistories]
ADD CONSTRAINT [FK_CartStatusCartHistory]
    FOREIGN KEY ([CartStatusId])
    REFERENCES [dbo].[CartStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartStatusCartHistory'
CREATE INDEX [IX_FK_CartStatusCartHistory]
ON [dbo].[CartHistories]
    ([CartStatusId]);
GO

-- Creating foreign key on [CartDetailId] in table 'CartHistories'
ALTER TABLE [dbo].[CartHistories]
ADD CONSTRAINT [FK_CartDetailCartHistory]
    FOREIGN KEY ([CartDetailId])
    REFERENCES [dbo].[CartDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartDetailCartHistory'
CREATE INDEX [IX_FK_CartDetailCartHistory]
ON [dbo].[CartHistories]
    ([CartDetailId]);
GO

-- Creating foreign key on [CartItemStatusId] in table 'CartItems'
ALTER TABLE [dbo].[CartItems]
ADD CONSTRAINT [FK_CartItemStatusCartItem]
    FOREIGN KEY ([CartItemStatusId])
    REFERENCES [dbo].[CartItemStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CartItemStatusCartItem'
CREATE INDEX [IX_FK_CartItemStatusCartItem]
ON [dbo].[CartItems]
    ([CartItemStatusId]);
GO

-- Creating foreign key on [ItemCatGroupId] in table 'ItemCategories'
ALTER TABLE [dbo].[ItemCategories]
ADD CONSTRAINT [FK_ItemCatGroupItemCategory]
    FOREIGN KEY ([ItemCatGroupId])
    REFERENCES [dbo].[ItemCatGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemCatGroupItemCategory'
CREATE INDEX [IX_FK_ItemCatGroupItemCategory]
ON [dbo].[ItemCategories]
    ([ItemCatGroupId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StorePickupPoints'
ALTER TABLE [dbo].[StorePickupPoints]
ADD CONSTRAINT [FK_StoreDetailStorePickupPoint]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStorePickupPoint'
CREATE INDEX [IX_FK_StoreDetailStorePickupPoint]
ON [dbo].[StorePickupPoints]
    ([StoreDetailId]);
GO

-- Creating foreign key on [StorePickupPointId] in table 'StorePickupPartners'
ALTER TABLE [dbo].[StorePickupPartners]
ADD CONSTRAINT [FK_StorePickupPointStorePickupPartner]
    FOREIGN KEY ([StorePickupPointId])
    REFERENCES [dbo].[StorePickupPoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePickupPointStorePickupPartner'
CREATE INDEX [IX_FK_StorePickupPointStorePickupPartner]
ON [dbo].[StorePickupPartners]
    ([StorePickupPointId]);
GO

-- Creating foreign key on [StoreDetailId] in table 'StorePickupPartners'
ALTER TABLE [dbo].[StorePickupPartners]
ADD CONSTRAINT [FK_StoreDetailStorePickupPartner]
    FOREIGN KEY ([StoreDetailId])
    REFERENCES [dbo].[StoreDetails]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreDetailStorePickupPartner'
CREATE INDEX [IX_FK_StoreDetailStorePickupPartner]
ON [dbo].[StorePickupPartners]
    ([StoreDetailId]);
GO

-- Creating foreign key on [StorePickupPointId] in table 'CartDetails'
ALTER TABLE [dbo].[CartDetails]
ADD CONSTRAINT [FK_StorePickupPointCartDetail]
    FOREIGN KEY ([StorePickupPointId])
    REFERENCES [dbo].[StorePickupPoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePickupPointCartDetail'
CREATE INDEX [IX_FK_StorePickupPointCartDetail]
ON [dbo].[CartDetails]
    ([StorePickupPointId]);
GO

-- Creating foreign key on [StorePickupStatusId] in table 'StorePickupPoints'
ALTER TABLE [dbo].[StorePickupPoints]
ADD CONSTRAINT [FK_StorePickupStatusStorePickupPoint]
    FOREIGN KEY ([StorePickupStatusId])
    REFERENCES [dbo].[StorePickupStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StorePickupStatusStorePickupPoint'
CREATE INDEX [IX_FK_StorePickupStatusStorePickupPoint]
ON [dbo].[StorePickupPoints]
    ([StorePickupStatusId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------