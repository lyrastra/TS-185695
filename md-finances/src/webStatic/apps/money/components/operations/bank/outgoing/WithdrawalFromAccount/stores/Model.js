import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    Sum: 0,
    Description: ``,
    Status: ``,
    CashOrder: {
        DocumentId: 0,
        DocumentName: null
    },
    OperationType: null,
    TaxationSystemType: null,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``,
        HasPostings: false
    },
    PostingsAndTaxMode: ProvidePostingType.ByHand,
    AccountingPostings: {},
    ProvideInAccounting: true
};
