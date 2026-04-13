select top 1
	Id,
	DocumentBaseId,
	Date,
	CashId,
	Number,
	SalaryWorkerId,
	Comments,
	Destination,
	DestinationName,
	Sum,
	IsProvideInAccounting	as ProvideInAccounting,
	PostingsAndTaxMode,
	TaxPostingType,
	PaidCardSum,
	OperationType,
	TaxationSystemType,
	SyntheticAccountTypeId,
	CreateDate,
	ModifyDate,
	NdsType,
	NdsSum,
	IncludeNds,
	ZReportNumber,
	Direction,
	PatentId
	from dbo.Accounting_CashOrder
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId