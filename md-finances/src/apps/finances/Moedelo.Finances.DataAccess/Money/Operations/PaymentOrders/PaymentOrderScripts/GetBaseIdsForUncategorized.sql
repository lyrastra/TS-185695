select DocumentBaseId
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and isnull(OperationState, @RegularOperationState) in (select Id from @UnrecognizedOperationStates)
		and isnull(OutsourceState, 0) <> @UnconfirmedOutsourceState
		and (coalesce(@sourceId, 0) = 0 or SettlementAccountId = @sourceId);
