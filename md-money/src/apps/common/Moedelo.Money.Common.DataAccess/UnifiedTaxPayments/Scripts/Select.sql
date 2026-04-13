set nocount on;

select
    utp.DocumentBaseId,
    utp.ParentDocumentId,
    utp.KbkNumberId,
    utp.PeriodType,
    utp.PeriodNumber,
    utp.PeriodYear,
    utp.PaymentSum,
    utp.PatentId,
    utp.TradingObjectId,
    utp.TaxPostingType
    from dbo.UnifiedTaxPayment utp
    --ParentBaseIdsFilter-- join #BaseIds as ids on ids.Id = utp.ParentDocumentId
    --DocumentBaseIdsFilter-- join #BaseIds as ids on ids.Id = utp.DocumentBaseId
where utp.FirmId = @FirmId
    --DocumentBaseIdFilter-- and utp.DocumentBaseId = @documentBaseId
    --ParentBaseIdFilter-- and utp.ParentDocumentId = @parentBaseId