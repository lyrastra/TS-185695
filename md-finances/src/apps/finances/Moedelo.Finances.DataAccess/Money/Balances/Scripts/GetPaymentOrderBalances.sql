select @MoneySourceTypeSettlementAccount as Type,
	SettlementAccountId as Id,
	isnull(sum(iif(Direction = @IncomingDirection, Sum, -Sum)), 0) as Balance
	from dbo.Accounting_PaymentOrder
	where FirmId = @FirmId
		and PaidStatus = @PayedDocumentStatus
		and Date between @BalancesMasterDate and @OnDate
		and isnull(OperationState, @DefaultOperationState) not in (select Id from @BadOperationStates)
		/* SourceFilter */
	group by SettlementAccountId
