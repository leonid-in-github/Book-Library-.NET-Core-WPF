
CREATE PROCEDURE [dbo].[AddBook]
	@Name NVARCHAR (1024),
	@Authors nvarchar (1024) = NULL,
	@Year DATETIME,
	@Quantity INT = 1
AS
	
		EXEC dbo.AddAuthors @AuthorsString = @Authors;

	while (@Quantity > 0) 
	begin
		Insert into Books (Name, Year) values(@Name, @Year); 
		set @Quantity = @Quantity - 1;
		
			DECLARE @BookId AS INT;
			SET @BookId = IDENT_CURRENT('Books');
		
			EXEC dbo.BindBookAuthors @BookId = @BookId, @Authors = @Authors;
	end 
RETURN 0