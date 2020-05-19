
CREATE PROCEDURE [dbo].[AddBookAuthor]
	@BookId INT,
	@AuthorId INT
AS
	IF NOT EXISTS (SELECT * FROM BooksAuthors 
                   WHERE BookId = @BookId AND AuthorId = @AuthorId)
	BEGIN
		Insert into BooksAuthors (BookId, AuthorId) values(@BookId, @AuthorId);
	END
RETURN 0