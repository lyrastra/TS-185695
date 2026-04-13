import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    Sum: 0,
    Description: ``,
    Status: ``,
    OperationType: null,
    TaxationSystemType: null,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``,
        HasPostings: false
    },
    PostingsAndTaxMode: ProvidePostingType.ByHand,
    AccountingPostings: {},
    ProvideInAccounting: true,
    PatentId: null
};
