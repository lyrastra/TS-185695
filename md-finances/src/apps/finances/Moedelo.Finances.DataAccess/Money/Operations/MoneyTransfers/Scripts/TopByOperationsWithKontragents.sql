select top (@count)
	mto.kontragent_id as KontragentId,
	cast(sum(mto.summ) as decimal(20, 4))  as TotalSum
from dbo.MoneyTransferOperation as mto
	left join dbo.FinancialOperation as fo
		on fo.id = mto.id
where fo.firm_id = @firmId
	and fo.operation_date between @startDate and @endDate
	and mto.kontragent_id is not null
group by mto.kontragent_id
order by TotalSum desc