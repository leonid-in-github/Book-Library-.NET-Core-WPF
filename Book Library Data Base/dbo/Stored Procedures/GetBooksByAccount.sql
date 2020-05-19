
CREATE PROCEDURE [dbo].[GetBooksByAccount]
	@AccountId INT
AS
BEGIN
	SELECT ID, Name, Authors = dbo.JoinBookAuthors(ID), Year, Availability
	FROM
		(
		SELECT BookId, MAX(ActionTime) AS ActionTime
		FROM BookTracking
		WHERE Action = 1 AND AccountId = 1
		GROUP BY BookId
		) AS BooksV
	INNER JOIN Books
	ON BooksV.BookId = Books.ID
	WHERE Availability = 0
	ORDER BY BooksV.ActionTime
END;