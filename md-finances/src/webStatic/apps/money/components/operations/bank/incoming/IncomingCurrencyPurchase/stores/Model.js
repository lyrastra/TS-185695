import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    FromSettlementAccountId: null,
    Sum: 0,
    Description: `Поступление от покупки валюты. НДС не облагается.`,
    OperationType: null,
    TaxPostings: {},
    AccountingPostings: {},
    ProvideInAccounting: true,
    TaxPostingsMode: ProvidePostingType.Auto
};
