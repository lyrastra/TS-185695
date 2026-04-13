select
	br.SettlementAccountId,
	br.CreateDate,
	br.[Status]
	from
		(select
			row_number() over (partition by br.SettlementAccountId order by br.CreateDate desc, br.Id desc) as Ordinal,
			br.SettlementAccountId,
			br.CreateDate,
			br.[Status]
			from dbo.BalanceReconcilation as br
				join #firmIds as fids on
					br.FirmId = fids.Id) as br
	where br.Ordinal = 1
