select
	po.DocumentBaseId,
	case
		when po.Date < @InitialDate then 1 
		when po.Date >= @InitialDate and po.OperationState = @OutsourceApprovedState then 1
		else 0
	end as IsApproved
	from #temp_ids as ids
		left join dbo.Accounting_PaymentOrder as po
			on po.DocumentBaseId = ids.Id
where po.FirmId = @FirmId;