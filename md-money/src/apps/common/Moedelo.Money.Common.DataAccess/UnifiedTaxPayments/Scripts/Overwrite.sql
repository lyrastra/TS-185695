set nocount on;

update utp
set
    utp.KbkNumberId     = src.KbkNumberId,
    utp.PeriodType      = src.PeriodType,
    utp.PeriodNumber    = src.PeriodNumber,
    utp.PeriodYear      = src.PeriodYear,
    utp.PaymentSum      = src.PaymentSum,
    utp.PatentId        = src.PatentId,
    utp.TradingObjectId = src.TradingObjectId,
    utp.TaxPostingType  = src.TaxPostingType
    from dbo.UnifiedTaxPayment utp
join #Payments src on src.DocumentBaseId = utp.DocumentBaseId  
where utp.ParentDocumentId = @ParentBaseId
      and utp.FirmId = @FirmId;

insert into dbo.UnifiedTaxPayment
(FirmId, ParentDocumentId, DocumentBaseId, KbkNumberId, PeriodType, PeriodNumber, PeriodYear, PaymentSum, PatentId, TradingObjectId, TaxPostingType)
select
    @FirmId,
    @ParentBaseId,
    src.DocumentBaseId,
    src.KbkNumberId,
    src.PeriodType,
    src.PeriodNumber,
    src.PeriodYear,
    src.PaymentSum,
    src.PatentId,
    src.TradingObjectId,
    src.TaxPostingType
from #Payments src
left join dbo.UnifiedTaxPayment utp on utp.DocumentBaseId   = src.DocumentBaseId
                                       and utp.ParentDocumentId = @ParentBaseId
                                       and utp.FirmId = @FirmId
where utp.DocumentBaseId is null;

delete utp
output deleted.DocumentBaseId
from dbo.UnifiedTaxPayment utp
left join #Payments src on src.DocumentBaseId = utp.DocumentBaseId  
where src.DocumentBaseId is null
  and utp.ParentDocumentId = @ParentBaseId
  and utp.FirmId           = @FirmId;
