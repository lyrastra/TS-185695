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
    Status: ``,
    OperationType: null,
    Contract: {
        ProjectId: null,
        ProjectNumber: ``,
        KontragentId: null,
        ContractBaseId: null
    },
    IsMainContractor: true,
    Kontragent: {
        KontragentId: null,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``
    },
    Documents: [],
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    Bills: [],
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    TaxPostingsInManualMode: false,
    AccountingPostings: {},
    ProvideInAccounting: true,
    ReserveSum: null
};
