import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Id: ``,
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    CentralBankRate: 0,
    TotalSum: 0,
    NdsType: null,
    NdsSum: null,
    IncludeNds: true,
    Description: null,
    OperationType: null,
    Status: null,
    Contract: {
        ProjectId: null,
        ProjectNumber: ``,
        KontragentId: null,
        ContractBaseId: null
    },
    Kontragent: {
        KontragentId: 0,
        KontragentName: ``,
        KontragentSettlementAccount: ``,
        KontragentINN: ``,
        KontragentKPP: ``
    },
    FixedAsset: {
        Id: 0,
        Number: ``,
        Name: ``,
        DocumentBaseId: ``,
        ContractId: 0,
        KontragentId: 0,
        IsRentRemains: false
    },
    FirstPeriodId: 0,
    Periods: [{
        Id: 0,
        Sum: 0,
        PaymentType: null,
        DefaultSum: ``,
        Description: ``,
        PaymentRequiredSum: ``
    }],
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
    isOther: true
};
