
CREATE PROCEDURE [dbo].[DeleteBook]
	@ID INT
AS
	delete from Books where ID = @ID
RETURN 0