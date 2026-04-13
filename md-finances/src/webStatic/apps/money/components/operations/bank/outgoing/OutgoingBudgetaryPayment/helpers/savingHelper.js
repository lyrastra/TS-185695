import { toJS } from 'mobx';
import dateHelper from '@moedelo/frontend-core-v2/helpers/dateHelper';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import { mapCurrencyInvoicesToBackendModel } from '../../../../../../../../mappers/currencyInvoicesMapper';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import MoneyOperationTypeResources from '../../../../../../../../resources/MoneyOperationTypeResources';

export function getUnifiedBudgetaryPaymentSaveModel(budgetaryPaymentStore) {
    const { model, UnifiedBudgetaryPaymentStore } = budgetaryPaymentStore;

    return {
        DocumentBaseId: model.DocumentBaseId,
        Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
        Number: model.Number,
        SettlementAccountId: model.SettlementAccountId,
        Sum: model.Sum,
        Description: budgetaryPaymentStore.description,
        Recipient: toJS(model.Recipient),
        ProvideInAccounting: model.ProvideInAccounting,
        PayerStatus: model.PayerStatus,
        IsPaid: model.Status === DocumentStatusEnum.Payed,
        SubPayments: UnifiedBudgetaryPaymentStore.subPaymentsForSave,
        CurrencyInvoices: mapCurrencyInvoicesToBackendModel(model.CurrencyInvoices),
        OperationType: MoneyOperationTypeResources.UnifiedBudgetaryPayment.value,
        Uin: model.Uin
    };
}

export function getCommonBudgetaryPaymentSaveModel(store) {
    const {
        model, TaxationSystem, description, documentNumberForSave
    } = store;
    const customPostings = model.TaxPostingsMode === ProvidePostingType.ByHand ? model.TaxPostings.Postings : [];
    const TaxPostings = store.getTaxPostingForSave(customPostings, model.TaxPostingsMode, TaxationSystem);
    const documentDate = dateHelper(model.DocumentDate, `DD.MM.YYYY`).isValid() ? dateHelper(model.DocumentDate, `DD.MM.YYYY`).format(`YYYY-MM-DD`) : 0;

    return {
        DocumentBaseId: model.DocumentBaseId,
        Date: dateHelper(model.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`),
        Number: model.Number,
        SettlementAccountId: model.SettlementAccountId,
        Sum: model.Sum,
        Description: description,
        AccountCode: model.AccountCode,
        KbkPaymentType: model.KbkPaymentType,
        TaxationSystemType: model.TaxationSystemType,
        PatentId: model.PatentId,
        Kbk: model.Kbk,
        Period: {
            Type: model.Period.Type,
            Date: dateHelper(model.Period.Date, `DD.MM.YYYY`).isValid() ? dateHelper(model.Period.Date, `DD.MM.YYYY`).format(`YYYY-MM-DD`) : null,
            Month: model.Period.Month,
            Quarter: model.Period.Quarter,
            HalfYear: model.Period.HalfYear,
            Year: model.Period.Year
        },
        PayerStatus: model.PayerStatus,
        PaymentBase: model.PaymentBase,
        DocumentDate: documentDate,
        DocumentNumber: documentNumberForSave,
        Uin: model.Uin,
        Recipient: {
            Name: model.Recipient.Name,
            Inn: model.Recipient.Inn,
            Kpp: model.Recipient.Kpp,
            Okato: model.Recipient.Okato,
            Oktmo: model.Recipient.Oktmo,
            SettlementAccount: model.Recipient.SettlementAccount,
            BankCorrespondentAccount: model.Recipient.BankCorrespondentAccount,
            BankName: model.Recipient.BankName,
            BankBik: model.Recipient.BankBik
        },
        ProvideInAccounting: model.ProvideInAccounting,
        TaxPostings,
        IsPaid: model.Status === DocumentStatusEnum.Payed,
        TradingObjectId: model.TradingObjectId,
        OperationType: MoneyOperationTypeResources.BudgetaryPayment.value,
        CurrencyInvoices: mapCurrencyInvoicesToBackendModel(model.CurrencyInvoices)
    };
}
