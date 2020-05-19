CREATE TABLE [dbo].[Books] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (64) NOT NULL,
    [Year]         DATETIME      NOT NULL,
    [Availability] BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

