-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ChangeAccountPassword]
	@AccountId INT,
	@Password NVARCHAR (32),
	@NewPassword NVARCHAR (32),
	@Result BIT OUTPUT
AS
	SET @Result = 0;
	DECLARE @AccountPassword NVARCHAR (32);
	SELECT @AccountPassword = Password
	FROM Accounts
	WHERE ID = @AccountId;

	IF (@AccountPassword = @Password)
	BEGIN
		UPDATE Accounts
		SET Password = @NewPassword
		WHERE ID = @AccountId;
		SET @Result = 1;
	END;
