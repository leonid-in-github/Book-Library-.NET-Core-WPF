
CREATE PROCEDURE [dbo].[UpdateBookAuthors]
	@BookID INT,
	@NewAuthors NVARCHAR(1024) = NULL
AS
	DELETE FROM BooksAuthors WHERE BookId = @BookID;
	
		EXEC dbo.AddAuthors @AuthorsString = @NewAuthors;
		EXEC dbo.BindBookAuthors @BookId = @BookID, @Authors = @NewAuthors;
RETURN 0