select po.Id
	from dbo.Accounting_PaymentOrder as po
	where po.FirmId = @FirmId
		and po.PaymentNumber = @PaymentOrderNumber
		and po.Sum = @Sum
		and po.Direction = @Direction
		and po.Date between @StartDate and @EndDate
		and isnull(po.OperationState, @RegularOperationState) not in (select Id from @BadOperationStates)
	order by iif(po.Date = @Date, 0, 1),
		po.Id desc;