select
    iif(PaidStatus = @truePaidStatus, 1, 0) as IsPaid
from 
    dbo.Accounting_PaymentOrder
where 
    FirmId = @firmId
    and DocumentBaseId = @documentBaseId;