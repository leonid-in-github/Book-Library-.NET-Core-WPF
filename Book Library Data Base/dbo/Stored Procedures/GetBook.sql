
CREATE PROCEDURE [dbo].[GetBook]
	@BookID INT
AS
BEGIN
	SELECT ID, Name, Authors = dbo.JoinBookAuthors(@BookID), Year, Availability
	FROM Books
	WHERE ID = @BookID
	ORDER BY ID DESC;
END;