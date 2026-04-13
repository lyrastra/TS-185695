import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    ToSettlementAccountId: null,
    CentralBankRate: null,
    Sum: null,
    TotalSum: null,
    ExchangeRate: null,
    ExchangeRateDiff: null,
    Description: `Покупка валюты. НДС не облагается.`,
    OperationType: null,
    TaxPostings: {},
    AccountingPostings: {},
    ProvideInAccounting: true,
    TaxPostingsMode: ProvidePostingType.Auto,
    Status: DocumentStatusEnum.Payed
};
