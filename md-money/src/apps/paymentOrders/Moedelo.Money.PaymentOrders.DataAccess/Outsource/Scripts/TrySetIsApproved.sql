update po
	set OperationState = @OutsourceApprovedState
	from dbo.Accounting_PaymentOrder as po
		join #temp_ids as ids
			on ids.Id = po.DocumentBaseId
	where coalesce(po.OperationState, @OperationStateDefault) in (@OperationStateDefault, @OperationStateImported)