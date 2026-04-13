import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    FromSettlementAccountId: null,
    Sum: null,
    Description: `Поступление от продажи валюты. НДС не облагается.`,
    OperationType: null,
    TaxPostings: {},
    AccountingPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    ProvideInAccounting: true,
    TaxPostingsMode: ProvidePostingType.Auto
};
