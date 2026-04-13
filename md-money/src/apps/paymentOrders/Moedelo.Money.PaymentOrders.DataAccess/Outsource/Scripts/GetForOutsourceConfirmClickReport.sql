select
    apo.DocumentBaseId,
    apo.FirmId,
    apo.CreateDate,
    apo.PaymentNumber as Number,
    apo.Date,
    apo.KontragentName,
    apo.Description,
    apo.OperationType,
    apo.Sum
from Accounting_PaymentOrder as apo
inner join #BaseIds as i on apo.DocumentBaseId = i.Id