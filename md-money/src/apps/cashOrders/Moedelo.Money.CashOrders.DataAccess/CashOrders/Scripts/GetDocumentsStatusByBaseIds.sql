select
    b.Id as DocumentBaseId,
    1 as IsValid
from #BaseIds as b
join dbo.Accounting_CashOrder as co on co.DocumentBaseId = b.Id;