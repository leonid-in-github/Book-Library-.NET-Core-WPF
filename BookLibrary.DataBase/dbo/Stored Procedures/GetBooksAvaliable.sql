
CREATE PROCEDURE [dbo].[GetBooksAvaliable]
AS
BEGIN
	SELECT ID, Name, Authors = dbo.JoinBookAuthors(ID), Year, Availability
	FROM Books
	WHERE Availability = 1
	ORDER BY Name
END;