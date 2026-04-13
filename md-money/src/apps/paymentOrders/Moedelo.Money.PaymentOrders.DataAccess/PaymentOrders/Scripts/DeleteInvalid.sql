set nocount on;

delete from dbo.Accounting_PaymentOrder
	output deleted.DocumentBaseId
	where FirmId = @firmId
		and DocumentBaseId in (select Id from #BaseIds)
		and coalesce(OperationState, @OperationStateDefault) in (select Id from #BadStates);
