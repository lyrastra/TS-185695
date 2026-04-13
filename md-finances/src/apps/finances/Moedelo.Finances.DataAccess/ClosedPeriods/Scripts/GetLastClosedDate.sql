select top 1 EndDate
	from dbo.ClosedPeriod
	where FirmId = @FirmId
	order by EndDate desc;