update dbo.Accounting_PaymentOrder
	set OperationState = @OperationStateDefault
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId
		and Date >= @InitialDate and OperationState = @OutsourceApprovedState
