import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    ToSettlementAccountId: null,
    Sum: 0,
    Description: ``,
    Status: ``,
    OperationType: null,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``,
        HasPostings: false
    },
    PostingsAndTaxMode: ProvidePostingType.ByHand,
    AccountingPostings: {},
    ProvideInAccounting: true
};
