import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    ToSettlementAccountId: null,
    Sum: 0,
    Description: ``,
    Status: DocumentStatusEnum.Payed,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    AccountingPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    ProvideInAccounting: true
};
