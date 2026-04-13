update apo
    set apo.OutsourceState = @state
output inserted.DocumentBaseId, inserted.FirmId, inserted.OutsourceState
from Accounting_PaymentOrder as apo
inner join #BaseIds as i
    on apo.DocumentBaseId = i.Id