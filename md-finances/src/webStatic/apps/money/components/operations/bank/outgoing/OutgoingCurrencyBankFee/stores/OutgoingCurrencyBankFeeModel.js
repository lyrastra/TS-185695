import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    CentralBankRate: 1,
    Sum: 0,
    TotalSum: 0,
    Description: ``,
    OperationType: null,
    TaxPostingsMode: ProvidePostingType.Auto,
    Status: DocumentStatusEnum.Payed,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    AccountingPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    ProvideInAccounting: false
};
