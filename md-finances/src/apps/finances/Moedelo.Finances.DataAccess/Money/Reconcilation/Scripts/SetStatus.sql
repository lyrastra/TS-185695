update
	dbo.BalanceReconcilation
	set
	Status = @status
	where FirmId = @firmId
		and SessionId = @sessionId;