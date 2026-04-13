select @MoneySourceTypePurse as Type,
		PurseId as Id,
		isnull(sum(iif(Direction = @IncomingDirection, Sum, -Sum)), 0) as Balance
	from docs.PurseOperation as po
	where FirmId = @FirmId
		and po.[DocumentDate] between @BalancesMasterDate and @OnDate
		/* SourceFilter */
	group by PurseId
