CREATE TABLE [dbo].[BookTracking] (
    [ID]         INT      IDENTITY (1, 1) NOT NULL,
    [BookId]     INT      NOT NULL,
    [AccountId]  INT      NOT NULL,
    [ActionTime] DATETIME NOT NULL,
    [Action]     BIT      DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BookTrackingAccountId] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([ID]) ON UPDATE CASCADE,
    CONSTRAINT [FK_BookTrackingBookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

