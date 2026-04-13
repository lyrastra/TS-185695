import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    KontragentAccountCode: null,
    NdsType: null,
    NdsSum: null,
    IncludeNds: null,
    Sum: 0,
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
    Bills: [],
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
    TaxationSystemType: null,
    PatentId: null,
    isOther: true
};
