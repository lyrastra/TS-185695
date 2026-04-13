import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    KontragentAccountCode: null,
    IsMediation: null,
    MediationCommission: ``,
    MediationCommissionNdsType: null,
    MediationCommissionNdsSum: null,
    IncludeMediationCommissionNds: null,
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
    Kontragent: {
        KontragentId: 0,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``
    },
    IsMainContractor: true,
    Bills: [],
    Documents: [],
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``,
        HasPostings: false
    },
    Mediation: {
        CommissionSum: 0,
        IsMediation: null
    },
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    AccountingPostings: {},
    ProvideInAccounting: true,
    TaxationSystemType: null,
    PatentId: null,
    ReserveSum: null
};
