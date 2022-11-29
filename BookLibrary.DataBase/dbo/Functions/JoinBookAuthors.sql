
CREATE FUNCTION [dbo].[JoinBookAuthors]
(
	@BookId INT
)
RETURNS NVARCHAR(MAX)
WITH SCHEMABINDING 
AS 
BEGIN
	DECLARE @Result NVARCHAR(MAX);
	SET @Result = '';

	DECLARE @Author as NVARCHAR (64);
	
	DECLARE @AuthorCursor as CURSOR;
	
	SET @AuthorCursor = CURSOR FOR
	SELECT Name FROM 
		(SELECT AuthorId from dbo.BooksAuthors where BookId = @BookId) as BooksAuthorsV
		INNER JOIN
		dbo.Authors ON  BooksAuthorsV.AuthorId = Authors.ID
	ORDER BY Name;
	
	OPEN @AuthorCursor;
	
	FETCH NEXT FROM @AuthorCursor INTO @Author;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @Result = @Result + @Author + ', ';
		FETCH NEXT FROM @AuthorCursor INTO @Author;
	END
	CLOSE @AuthorCursor;
	DEALLOCATE @AuthorCursor;

	SET @Result = dbo.CustomTrim(@Result, ' ');
	SET @Result = dbo.CustomTrim(@Result, ',');

	RETURN (@Result);
END