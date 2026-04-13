/*
    CountCondition - используется для получения кол-ва платежей не проведённых в БУ
    DocumentCondition - используется для получения самих платежей не проведённых в БУ
    Используются разные наборы полей. И группировка.
*/

select /*--CountCondition-- 
        count(*) as Count, 
        --CountCondition--*/
/*--DocumentCondition-- 
        p.PaymentNumber AS Number,
		p.Date,
		p.Sum,
		p.DocumentBaseId,
--DocumentCondition--*/ 
		p.Direction as Direction,
		@paymentOrderType as Type,
		p.OrderType as DocumentType
	from dbo.Accounting_PaymentOrder as p
	where p.FirmId = @FirmId
		and p.ProvideInAccounting = 0
		and p.Date between @StartDate and @EndDate
		and isnull(p.OperationState, @DefaultOperationState) not in (select Id from @BadOperationStates)
	/*--CountCondition--
	group by p.Direction,
		p.OrderType
	--CountCondition--*/
union all
select /*--CountCondition--
        count(*) as Count,
        --CountCondition--*/
/*--DocumentCondition--
        c.Number,
		c.Date,
		c.Sum,
		c.DocumentBaseId,
--DocumentCondition--*/ 
		c.Direction as Direction,
        iif(c.Direction = 2, @incomingCashOrder, @outcomingCashOrder) as Type,
		@emptyDocumentType as DocumentType
	from dbo.Accounting_CashOrder as c
	where c.FirmId = @FirmId
		and c.IsProvideInAccounting = 0
		and c.Date between @StartDate and @EndDate
        and c.OperationType not in @excludeCashOperationTypes
	/*--CountCondition--
	group by c.Direction
	--CountCondition--*/
