
CREATE PROCEDURE [dbo].[GetBooksAvaliableDistinct]
AS
BEGIN
	SELECT min(ID) AS ID, Name, Authors, Year, Availability
	FROM (SELECT ID, Name, Authors = dbo.JoinBookAuthors(ID), Year, Availability
	FROM Books) AS T
	WHERE Availability = 1
	GROUP BY Name, Authors, Year, Availability;
END;