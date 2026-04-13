select count(1)
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and isnull(OperationState, @RegularOperationState) = @ImportedOperationState
		and isnull(OutsourceState, 0) <> @UnconfirmedOutsourceState
		and (@SourceId is null or SettlementAccountId = @SourceId);
