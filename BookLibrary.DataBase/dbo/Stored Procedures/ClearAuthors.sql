
CREATE PROCEDURE [dbo].[ClearAuthors]
AS
	DECLARE @AuthorId as INT;
	
	DECLARE @AuthorCursor as CURSOR;
	
	SET @AuthorCursor = CURSOR FOR
	SELECT ID FROM Authors;
	
	OPEN @AuthorCursor;
	
	FETCH NEXT FROM @AuthorCursor INTO @AuthorId;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF NOT EXISTS (SELECT * FROM BooksAuthors 
                   WHERE AuthorId = @AuthorId)
		BEGIN
			DELETE FROM Authors WHERE ID = @AuthorId;
		END
		FETCH NEXT FROM @AuthorCursor INTO @AuthorId;
	END
	CLOSE @AuthorCursor;
	DEALLOCATE @AuthorCursor;
RETURN 0