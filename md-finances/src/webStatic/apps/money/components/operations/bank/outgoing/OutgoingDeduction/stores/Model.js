import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import PaymentPriority from '../enums/PaymentPriority';
import PayerStatus from '../enums/PayerStatus';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    KontragentAccountCode: null,
    Sum: 0,
    Status: null,
    Description: ``,
    OperationType: null,
    Contract: {
        ProjectId: null,
        ProjectNumber: ``,
        KontragentId: null,
        ContractBaseId: null
    },
    ContractorType: null,
    Kontragent: {
        KontragentId: 0,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``,
        SalaryWorkerId: null
    },
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    AccountingPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    ProvideInAccounting: true,
    PaymentPriority: PaymentPriority.First,
    IsBudgetaryDebt: false,
    Kbk: ``,
    Oktmo: ``,
    Uin: `0`,
    DeductionWorkerId: null,
    DeductionWorkerInn: ``,
    DeductionWorkerFio: ``,
    DeductionWorkerDocumentNumber: ``,
    PayerStatus: PayerStatus.None
};
