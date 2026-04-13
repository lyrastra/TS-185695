insert into dbo.FinancialOperation
(
	firm_id,
	type,
	des,
	number,
	operation_date,
	DocumentBaseId,
	TaxPostingType)
values
(
	@FirmId,
	@OperationType,
	'',			-- des
	@Number,
	@Date,
	@DocumentBaseId,
	0 -- TaxPostingType - smallint
);
declare @foId int = scope_identity();

insert into dbo.MoneyTransferOperation
(
	id,
	money_bay_type,
	number_of_document,
	SettlementAccountId,
	summ)
values
(
	@foId,
	@Number,
	@MoneyBayType,
	@SettlementAccountId,
	@Sum
);
select @foId;
