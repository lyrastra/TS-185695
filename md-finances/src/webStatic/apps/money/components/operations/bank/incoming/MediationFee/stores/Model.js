import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    KontragentAccountCode: null,
    IsMediation: false,
    MediationCommission: ``,
    NdsType: null,
    NdsSum: null,
    IncludeNds: null,
    Sum: 0,
    Description: ``,
    OperationType: null,
    MiddlemanContract: {},
    Kontragent: {
        KontragentId: 0,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``
    },
    Bills: [],
    Documents: [],
    TaxPostings: {},
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    AccountingPostings: {},
    ProvideInAccounting: true
};
