;with
/* BudgetaryTypeExtraFilter
BudgetaryTypeExtraFilterKbk as (select
    KbkNumber.Id
from dbo.KbkNumber 
where KbkNumber.AccountCode >= @StartAccountCode 
    and KbkNumber.AccountCode < @EndAccountCode
    and KbkNumber.KbkType 
    /* AllInsuranceFeeFilter
    not 
    AllInsuranceFeeFilter */
    in (select Id from @FixedFeesKbkTypes)
),
BudgetaryTypeExtraFilter */
operations as (select po.Id,
           po.DocumentBaseId,
           po.Date,
           po.ModifyDate,
           po.PaymentNumber as Number,
           po.Direction,
           kontragent.Id as KontragentId,
           kontragent.Name as KontragentName,
           po.OperationType,
           po.PaidStatus,
           po.Sum,
           sa.id_currency as Currency,
           sa.SettlementAccountType as SettlementType,
           po.Description
    from dbo.Accounting_PaymentOrder as po
    inner join dbo.SettlementAccount as sa
        on sa.id = po.SettlementAccountId
    /* BudgetaryTypeExtraFilter
    left join BudgetaryTypeExtraFilterKbk as kf 
        on kf.Id = po.Id
    BudgetaryTypeExtraFilter */
    outer apply 
        (select coalesce(po.KontragentId, po.SalaryWorkerId) as Id,
                po.KontragentName as Name,
                case 
                     when po.KontragentId is not null then @KontragentTypeKontragent
                     when po.SalaryWorkerId is not null then @KontragentTypeWorker
                     else @KontragentTypeAll
                 end as Type
         ) as kontragent
    where po.FirmId = @FirmId
      and po.OutsourceState = @UnconfirmedOutsourceState
      and po.Date between @StartDate and @EndDate
      /* SourceIdFilter
      and po.SettlementAccountId = @SourceId
      SourceIdFilter */
      /* QueryFilter
      and (
         (kontragent.Name like '%!' + @Query + '%' escape '!' or po.Description like '%!' + @Query + '%' escape '!' or po.PaymentNumber like '%!' + @Query + '%' escape '!')
         or try_cast(replace(replace(@Query, ',', '.'), ' ', '') as decimal(20, 2)) = convert(decimal(20, 2), po.Sum)
       )
      QueryFilter */
      /* KontragentTypeFilter
      and kontragent.Type = @KontragentType
      KontragentTypeFilter */
      /* KontragentIdFilter
      and kontragent.Id = @KontragentId
      KontragentIdFilter */
      /* DirectionFilter
      and po.Direction = @Direction
      DirectionFilter */
      /* OperationTypeFilter
      and po.OperationType in (select Id from @OperationTypes)
      OperationTypeFilter */
      /* BudgetaryTypeFilter
      and (
          po.BudgetaryTaxesAndFees = @BudgetaryType
          or exists (select * 
                     from dbo.UnifiedTaxPayment
                     inner join dbo.KbkNumber on KbkNumber.Id = UnifiedTaxPayment.KbkNumberId
                     where UnifiedTaxPayment.ParentDocumentId = po.DocumentBaseId
                     and KbkNumber.AccountCode = @BudgetaryType)
      )
      BudgetaryTypeFilter */
      /* BudgetaryTypeExtraFilter
      and (
          kf.Id is not null
          or exists (select * 
                     from dbo.UnifiedTaxPayment
                     inner join BudgetaryTypeExtraFilterKbk on BudgetaryTypeExtraFilterKbk.Id = UnifiedTaxPayment.KbkNumberId
                     where UnifiedTaxPayment.ParentDocumentId = po.DocumentBaseId)
      )
      BudgetaryTypeExtraFilter */
      /* SumRangeFilter
      and po.Sum between @SumFrom and @SumTo
      SumRangeFilter */
      /* SumLessFilter
      and po.Sum < @Sum
      SumLessFilter */
      /* SumEqualFilter
      and po.Sum = @Sum
      SumEqualFilter */
      /* SumGreatFilter
      and po.Sum > @Sum
      SumGreatFilter */
    ),
summary as (
    select count(1) as TotalCount from operations
)

select
    1 as isSummary,
    0 as Id,
    0 as DocumentBaseId,
    null as Date,
    null as ModifyDate,
    null as Number,
    0 as Direction,
    null as KontragentId,
    null as KontragentName,
    0 as OperationType,
    0 as PaidStatus,
    0 as Sum,
    0 as Currency,
    0 as SettlementType,
    null as Description,
    summary.TotalCount
from summary

--NotSelectPage-- /* 
union all

select
    0 as isSummary,
    ops.Id,
    ops.DocumentBaseId,
    ops.Date,
    ops.ModifyDate,
    ops.Number,
    ops.Direction,
    ops.KontragentId,
    ops.KontragentName,
    ops.OperationType,
    ops.PaidStatus,
    ops.Sum,
    ops.Currency,
    ops.SettlementType,
    ops.Description,
    0 as TotalCount
from (select *
      from operations
      order by Date
      offset @Offset rows fetch next @Count rows only) as ops;
--NotSelectPage-- */
