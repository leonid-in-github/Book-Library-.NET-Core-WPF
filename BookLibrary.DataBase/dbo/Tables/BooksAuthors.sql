CREATE TABLE [dbo].[BooksAuthors] (
    [BookId]   INT NOT NULL,
    [AuthorId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([BookId] ASC, [AuthorId] ASC),
    CONSTRAINT [FK_BooksAuthorsAuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BooksAuthorsBookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

