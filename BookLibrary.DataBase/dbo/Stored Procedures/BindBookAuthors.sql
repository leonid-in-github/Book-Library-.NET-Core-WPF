
CREATE PROCEDURE [dbo].[BindBookAuthors]
	@BookId INT,
	@Authors nvarchar (1024)
AS
	
	DECLARE @Author as NVARCHAR (64);
	DECLARE @AuthorId as INT;
	
	DECLARE @AuthorCursor as CURSOR;
	
	
	SET @Authors = dbo.CustomTrim(@Authors, ' ');
	SET @Authors = dbo.CustomTrim(@Authors, ',');
	SET @AuthorCursor = CURSOR FOR
	SELECT value FROM STRING_SPLIT(@Authors, ',');
	
	OPEN @AuthorCursor;
	
	FETCH NEXT FROM @AuthorCursor INTO @Author;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @Author = dbo.CustomTrim(@Author, ' ');
		SELECT TOP 1 @AuthorId = ID
		FROM Authors
		WHERE Name = @Author
		ORDER BY ID ASC;
		
		IF @AuthorId IS NOT NULL
		BEGIN
			IF NOT EXISTS (SELECT * FROM BooksAuthors 
					   WHERE BookId = @BookId AND AuthorId = @AuthorId)
			BEGIN
				Insert into BooksAuthors (BookId, AuthorId) values(@BookId, @AuthorId) 
			END
			FETCH NEXT FROM @AuthorCursor INTO @Author;
		END
	END
	CLOSE @AuthorCursor;
	DEALLOCATE @AuthorCursor;
	
RETURN 0