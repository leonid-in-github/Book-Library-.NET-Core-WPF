
CREATE PROCEDURE [dbo].[LoginAccount]
	@Login NVARCHAR (64),
	@Password NVARCHAR (32),
	@Result INT OUTPUT
AS
	DECLARE @AccountId INT;
	SET @AccountId = 0;
	SELECT @AccountId = ID
	FROM Accounts
	WHERE Login = @Login
	AND Password = @Password;
SET @Result = @AccountId