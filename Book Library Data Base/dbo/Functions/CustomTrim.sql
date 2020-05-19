
create FUNCTION [dbo].[CustomTrim]
(
	@String NVARCHAR(MAX),
	@TrimChar NCHAR(1)
)
RETURNS NVARCHAR(MAX)
WITH SCHEMABINDING 
AS 
BEGIN
	DECLARE @Result NVARCHAR(MAX);
	SET @Result = @String;
	
	IF( LEN(@Result) > 1)
		BEGIN
		WHILE SUBSTRING(@Result, 1, 1) = @TrimChar
		BEGIN
			SET @Result = SUBSTRING(@Result, 2, LEN(@Result) - 1)
		END
	
		WHILE SUBSTRING(@Result, LEN(@Result), 1) = @TrimChar
		BEGIN
			SET @Result = SUBSTRING(@Result, 1, LEN(@Result) - 1)
		END
	END
	
	RETURN (@Result);
END