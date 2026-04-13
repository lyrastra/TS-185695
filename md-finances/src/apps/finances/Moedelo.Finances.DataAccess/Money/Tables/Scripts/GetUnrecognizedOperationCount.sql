select count(1)
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and isnull(OperationState, @RegularOperationState) in (select Id from @UnrecognizedOperationStates)
		and isnull(OutsourceState, 0) <> @UnconfirmedOutsourceState
		and (@SourceId is null or SettlementAccountId = @SourceId);
