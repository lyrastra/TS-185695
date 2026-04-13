import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    Sum: 0,
    CashOrder: {
        DocumentId: 0,
        DocumentName: null
    },
    Description: ``,
    OperationType: null,
    TaxationSystemType: null,
    TaxPostingsInManualMode: false,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    AccountingPostings: {},
    ProvideInAccounting: true
};
