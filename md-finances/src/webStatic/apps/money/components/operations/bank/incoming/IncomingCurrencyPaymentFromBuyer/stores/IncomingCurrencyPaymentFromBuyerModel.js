import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    CentralBankRate: 1,
    Sum: 0,
    TotalSum: 0,
    Description: ``,
    OperationType: null,
    NdsType: null,
    NdsSum: null,
    IncludeNds: null,
    Contract: {
        ProjectId: null,
        ProjectNumber: ``,
        KontragentId: null,
        ContractBaseId: null
    },
    Kontragent: {
        KontragentId: 0,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``
    },
    Documents: [],

    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``,
        HasPostings: false
    },

    TaxPostingsMode: ProvidePostingType.Auto,
    AccountingPostings: {},
    ProvideInAccounting: true,
    TaxationSystemType: null,
    PatentId: null
};
