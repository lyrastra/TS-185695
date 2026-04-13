select 1
	from dbo.Accounting_PaymentOrder
where FirmId = @FirmId
	and DocumentBaseId = @DocumentBaseId
	and (Date < @InitialDate or OperationState = @OutsourceApprovedState)