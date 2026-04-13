select
	Id,
	Designation,
	Description,
	Code
	from dbo.AccountingBudgetaryPaymentReason
	where IsActive = 1;