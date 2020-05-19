
CREATE PROCEDURE [dbo].[UpdateBook]
	@ID INT,
	@NewName NVARCHAR(64),
	@NewAuthors NVARCHAR(1024) = NULL,
	@NewYear DATETIME
AS
	UPDATE Books
	SET Name = @NewName,
		Year = @NewYear
	WHERE ID = @ID
	
	EXEC dbo.UpdateBookAuthors @BookID = @ID, @NewAuthors = @NewAuthors;
	
RETURN 0