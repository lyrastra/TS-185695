update dbo.FinancialOperation
	set
		operation_date = @Date,
		number = @Number,
		DocumentBaseId = @DocumentBaseId
	where id = @Id;

update dbo.MoneyTransferOperation
	set
		number_of_document = @Number,
		money_bay_type = @MoneyBayType,
		SettlementAccountId = @SettlementAccountId,
		summ = @Sum
	where id = @Id;
