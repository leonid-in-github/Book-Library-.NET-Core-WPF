
CREATE PROCEDURE [dbo].[AddAuthors]
	@AuthorsString NVARCHAR (1024)
AS
	DECLARE @Author as NVARCHAR (64);
	
	DECLARE @AuthorCursor as CURSOR;
	
	SET @AuthorsString = dbo.CustomTrim(@AuthorsString, ' ');
	SET @AuthorsString = dbo.CustomTrim(@AuthorsString, ',');
	SET @AuthorCursor = CURSOR FOR
	SELECT value FROM STRING_SPLIT(@AuthorsString, ',');
	
	OPEN @AuthorCursor;
	
	FETCH NEXT FROM @AuthorCursor INTO @Author;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @Author = dbo.CustomTrim(@Author, ' ');
		IF NOT EXISTS (SELECT * FROM Authors 
                   WHERE Name = @Author)
		BEGIN
			Insert into Authors (Name) values(@Author) 
		END
	 FETCH NEXT FROM @AuthorCursor INTO @Author;
	END
	CLOSE @AuthorCursor;
	DEALLOCATE @AuthorCursor;

RETURN 0