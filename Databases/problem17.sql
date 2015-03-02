 if(object_id('fn_MountainsPeaksJSON') is not null)
 	drop FUNCTION fn_MountainsPeaksJSON
GO
CREATE function fn_MountainsPeaksJSON()
returns nvarchar(max)
as
begin
declare @result nvarchar (max) = '{"mountains":['
	
	declare mountCursor cursor read_only for
	select m.MountainRange FROM Mountains m
	OPEN mountCursor
	DECLARE @mname NVARCHAR(MAX)
	FETCH NEXT FROM mountCursor INTO @mname
	WHILE @@fetch_status = 0
	BEGIN
		DECLARE @mountain nvarchar(max)
		SET @mountain = '{"name":"' + @mname + '",'
		DECLARE peakCursor cursor READ_ONLY FOR
		SELECT p.PeakName, p.Elevation FROM Peaks p WHERE p.MountainId = (SELECT m.Id FROM Mountains m WHERE m.MountainRange = @mname)
		OPEN peakCursor
		DECLARE @pname nvarchar(MAX),
		@pelevation NVARCHAR(MAX),
		@peaks nvarchar(max) = '"peaks":['
		FETCH NEXT FROM peakCursor INTO @pname, @pelevation
		WHILE @@fetch_status = 0
		BEGIN
		IF @peaks = '"peaks":['
			BEGIN
				SET @peaks = @peaks + '{"name":"' + @pname + '","elevation":' + @pelevation + '}' 
			END
		ELSE
			BEGIN
				SET @peaks = @peaks + ',{"name":"' + @pname + '","elevation":' + @pelevation + '}'
			END
		FETCH NEXT FROM peakCursor INTO @pname, @pelevation
		END
		SET @peaks = @peaks + ']'
		SET @mountain = @mountain + @peaks + '}'
		IF @result = '{"mountains":['
			BEGIN
				SET @result = @result + @mountain
			END
		ELSE
			BEGIN
				SET @result = @result + ',' + @mountain
			END
		CLOSE peakCursor
		DEALLOCATE peakCursor
	FETCH NEXT FROM mountCursor INTO @mname
	END
	SET @result = @result + ']}'
	CLOSE mountCursor
	DEALLOCATE mountCursor
return @result
end 

GO
SELECT dbo.fn_MountainsPeaksJSON()