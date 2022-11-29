
CREATE PROCEDURE [dbo].[GetBooks]
AS
BEGIN
	SELECT ID, Name, Authors = dbo.JoinBookAuthors(ID), Year, Availability
	FROM Books
	ORDER BY ID DESC;
END;