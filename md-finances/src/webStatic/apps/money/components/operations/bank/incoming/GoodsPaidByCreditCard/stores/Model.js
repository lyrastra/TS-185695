import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    Sum: 0,
    AcquiringCommission: 0,
    AcquiringCommissionDate: ``,
    SaleDate: ``,
    NdsType: null,
    NdsSum: null,
    IncludeNds: null,
    Description: ``,
    OperationType: null,
    TaxPostings: {
        Postings: [],
        LinkedDocuments: [],
        ExplainingMessage: ``
    },
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    AccountingPostings: {},
    ProvideInAccounting: true,
    TaxationSystemType: null,
    IsMediation: false,
    PatentId: null
};
