CREATE TABLE [dbo].[Profiles] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (32) NOT NULL,
    [LastName]  NVARCHAR (32) NOT NULL,
    [Email]     NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

