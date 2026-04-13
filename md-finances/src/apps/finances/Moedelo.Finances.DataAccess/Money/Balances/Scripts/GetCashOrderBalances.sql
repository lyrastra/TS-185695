select @MoneySourceTypeCash as Type,
		CashId as Id,
		isnull(sum(iif(Direction = @IncomingDirection, Sum, -Sum)), 0) as Balance
	from dbo.Accounting_CashOrder
	where FirmId = @FirmId
		and Date between @BalancesMasterDate and @OnDate
		/* SourceFilter */
	group by CashId
