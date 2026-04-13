set nocount on;

update
	dbo.Accounting_CashOrder
	set
	Date = @Date,
	CashId = @CashId,
	Number = @Number,
	SalaryWorkerId = @SalaryWorkerId,
	Comments = @Comments,
	Destination = @Destination,
	DestinationName = @DestinationName,
	Sum = @Sum,
	IsProvideInAccounting = @ProvideInAccounting,
	PostingsAndTaxMode = @PostingsAndTaxMode,
	TaxPostingType = @TaxPostingType,
	PaidCardSum = @PaidCardSum,
	OperationType = @OperationType,
	TaxationSystemType = @TaxationSystemType,
	SyntheticAccountTypeId = @SyntheticAccountTypeId,
	ModifyDate = @ModifyDate,
	NdsType = @NdsType,
	NdsSum = @NdsSum,
	IncludeNds = @IncludeNds,
	ZReportNumber = @ZReportNumber,
	PatentId = @PatentId
	from dbo.Accounting_CashOrder
	where FirmId = @FirmId
		and DocumentBaseId = @DocumentBaseId;