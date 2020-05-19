
CREATE PROCEDURE [dbo].[CanPutBook]
	@AccountId INT,
	@BookId INT,
	@Result BIT OUTPUT
AS
BEGIN
	DECLARE @LastGetUserId INT;

	SELECT @LastGetUserId = Result.AccountId FROM
	(SELECT AccountId, MAX(ActionTime) as LastTakeDateTime FROM BookTracking
	WHERE BookId = @BookId AND Action = 1
	GROUP BY AccountId) as Result;

	IF (@AccountId = @LastGetUserId)
	BEGIN
		SET @Result = 1;
	END
	ELSE 
	BEGIN
		SET @Result = 0;
	END
END;