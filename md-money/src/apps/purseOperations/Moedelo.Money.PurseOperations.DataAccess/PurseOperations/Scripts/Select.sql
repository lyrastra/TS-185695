select top 1
	OperationType,
	DocumentDate as Date
	from docs.PurseOperation
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId