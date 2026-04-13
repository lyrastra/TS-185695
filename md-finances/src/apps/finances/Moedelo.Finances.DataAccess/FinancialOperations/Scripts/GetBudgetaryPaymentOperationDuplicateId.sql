select fo.id as Id
from dbo.FinancialOperation as fo
    inner join dbo.MoneyTransferOperation as mto
        on mto.id = fo.id
    inner join dbo.MoneyTransferOperationType as mtot
        on fo.type = mtot.name
where fo.firm_id = @FirmId
      and fo.type = @BudgetaryPaymentOperationType
      and fo.operation_date
      between @StartDate and @EndDate
      and mto.summ = @Sum
      and mto.budgetary_payment_type = @BudgetaryPaymentType
      and
      (
          @BudgetaryPaymentSubtype is null
          or mto.BudgetaryPaymentSubtype = @BudgetaryPaymentSubtype
      )
      and
      (
          @PaymentOrderNumber is null
          or isnull(mto.number_of_document, '') = isnull(@PaymentOrderNumber, '')
      )
      and
      (
          @SettlementAccountId is null
          or mto.SettlementAccountId = @SettlementAccountId
      )
order by fo.id desc;
