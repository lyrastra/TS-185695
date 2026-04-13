import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    KontragentAccountCode: null,
    Sum: 0,
    Nds: {
        IncludeNds: false,
        Type: null,
        Sum: 0
    },
    /* deprecated */
    NdsType: null,
    NdsSum: 0,
    IncludeNds: false,
    /* deprecated END */
    Description: ``,
    Status: ``,
    OperationType: null,
    Contract: {
        ProjectId: null,
        ProjectNumber: ``,
        KontragentId: null,
        ContractBaseId: null
    },
    Kontragent: {
        KontragentId: null,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``
    },
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``,
        HasPostings: false
    },
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    TaxPostingsInManualMode: false,
    AccountingPostings: {},
    ProvideInAccounting: true,
    TaxationSystemType: null,
    PatentId: null
};
