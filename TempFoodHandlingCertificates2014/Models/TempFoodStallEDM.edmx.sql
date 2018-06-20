
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/05/2016 16:17:07
-- Generated from EDMX file: D:\Projects\2016\TempFoodHandlingCertificates\TempFoodHandlingCertificatesVS2013\TempFoodHandlingCertificates2014\Models\TempFoodStallEDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TempFoodHandlingCertificates];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FoodEventApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Applications] DROP CONSTRAINT [FK_FoodEventApplication];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationProcessApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProcessApplications] DROP CONSTRAINT [FK_ApplicationProcessApplication];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Applications];
GO
IF OBJECT_ID(N'[dbo].[FoodEvents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FoodEvents];
GO
IF OBJECT_ID(N'[dbo].[ProcessApplications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessApplications];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Applications'
CREATE TABLE [dbo].[Applications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AppID] int  NULL,
    [GUID] nvarchar(max)  NOT NULL,
    [SubmittedDate] datetime  NULL,
    [EventDetails_EventName] nvarchar(max)  NULL,
    [EventDetails_EventLocation] nvarchar(max)  NULL,
    [EventDetails_EventDescription] nvarchar(max)  NULL,
    [EventDetails_EventStartDate] datetime  NULL,
    [EventDetails_OtherEventDates] nvarchar(max)  NULL,
    [StallDetails_StallBusinessName] nvarchar(max)  NULL,
    [StallDetails_StallStartTime] nvarchar(max)  NULL,
    [StallDetails_StallFinishTime] nvarchar(max)  NULL,
    [ApplicantDetails_ApplicantName] nvarchar(max)  NULL,
    [ApplicantDetails_ApplicantPhoneNumber] nvarchar(max)  NULL,
    [ApplicantDetails_ApplicantEmail] nvarchar(max)  NULL,
    [ApplicantDetails_ApplicantPostalAddress] nvarchar(max)  NULL,
    [StallType] nvarchar(max)  NULL,
    [FoodDetails_FoodDetailsTypeOfFood] nvarchar(max)  NULL,
    [FoodDetails_FoodDetailsWhereStoredPrior] nvarchar(max)  NULL,
    [FoodDetails_FoodDetailsFoodsPreparedOffSite] nvarchar(max)  NULL,
    [FoodDetails_FoodDetailsFoodsPreparedOnSite] nvarchar(max)  NULL,
    [HowFoodsKeptCold] nvarchar(max)  NULL,
    [HowFoodsKeptHot] nvarchar(max)  NULL,
    [HandwashFacilities] nvarchar(max)  NULL,
    [FoodEvent_Id] int  NULL
);
GO

-- Creating table 'FoodEvents'
CREATE TABLE [dbo].[FoodEvents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventName] nvarchar(max)  NOT NULL,
    [EventStartDate] nvarchar(max)  NOT NULL,
    [EventEndDate] nvarchar(max)  NULL
);
GO

-- Creating table 'ProcessApplications'
CREATE TABLE [dbo].[ProcessApplications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GUID] nvarchar(max)  NULL,
    [PaymentRecieved] bit  NOT NULL,
    [PaymentReceivedDescription] nvarchar(max)  NULL,
    [PaymentLastCheckedDate] datetime  NULL,
    [CanIssueCertificate] bit  NOT NULL,
    [CertificateIssuedDate] datetime  NULL,
    [AmountDue] decimal(18,0)  NULL,
    [Comments] nvarchar(max)  NULL,
    [InspectionRequired] nvarchar(max)  NULL,
    [ReceiptNumber] nvarchar(max)  NULL,
    [ReminderNotes] nvarchar(max)  NULL,
    [ReminderSent] datetime  NULL,
    [IsDeleted] bit  NOT NULL,
    [NotificationSentDate] datetime  NULL,
    [NotificationApproved] bit  NULL,
    [Application_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Applications'
ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [PK_Applications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FoodEvents'
ALTER TABLE [dbo].[FoodEvents]
ADD CONSTRAINT [PK_FoodEvents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProcessApplications'
ALTER TABLE [dbo].[ProcessApplications]
ADD CONSTRAINT [PK_ProcessApplications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FoodEvent_Id] in table 'Applications'
ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [FK_FoodEventApplication]
    FOREIGN KEY ([FoodEvent_Id])
    REFERENCES [dbo].[FoodEvents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FoodEventApplication'
CREATE INDEX [IX_FK_FoodEventApplication]
ON [dbo].[Applications]
    ([FoodEvent_Id]);
GO

-- Creating foreign key on [Application_Id] in table 'ProcessApplications'
ALTER TABLE [dbo].[ProcessApplications]
ADD CONSTRAINT [FK_ApplicationProcessApplication]
    FOREIGN KEY ([Application_Id])
    REFERENCES [dbo].[Applications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationProcessApplication'
CREATE INDEX [IX_FK_ApplicationProcessApplication]
ON [dbo].[ProcessApplications]
    ([Application_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------