
CREATE PROCEDURE [dbo].[GetBooksDistinct]
AS
BEGIN
	SELECT min(ID) AS ID, Name, Authors, Year, MAX(convert(TINYINT,Availability)) AS 'Availability'
	FROM (SELECT ID, Name, Authors = dbo.JoinBookAuthors(ID), Year, Availability
	FROM Books) AS T
	GROUP BY Name, Authors, Year;
END;