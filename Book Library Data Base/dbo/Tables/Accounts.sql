CREATE TABLE [dbo].[Accounts] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [Login]     NVARCHAR (32) NOT NULL,
    [Password]  NVARCHAR (16) NOT NULL,
    [ProfileId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Login] ASC),
    CONSTRAINT [FK_AccountsProfiles] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profiles] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    UNIQUE NONCLUSTERED ([ID] ASC)
);

