set nocount on;

update dbo.Accounting_PaymentOrder
	set OperationState = @OperationStateDefault
	output inserted.DocumentBaseId
	where FirmId = @FirmId
		and isnull(OperationState, @OperationStateDefault) = @OperationStateImported
		and (coalesce(@SettlementAccountId, 0) = 0 or SettlementAccountId = @SettlementAccountId)
		and (coalesce(@Skipline, '0081-01-01') = '0081-01-01' or CreateDate < @Skipline);
