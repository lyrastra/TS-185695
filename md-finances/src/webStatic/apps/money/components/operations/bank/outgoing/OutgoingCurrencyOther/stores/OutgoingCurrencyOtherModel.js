import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';
import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    CentralBankRate: 0,
    Sum: 0,
    TotalSum: 0,
    Description: null,
    OperationType: null,
    Status: DocumentStatusEnum.Payed,
    Contract: {
        ProjectId: null,
        ProjectNumber: ``,
        KontragentId: null,
        ContractBaseId: null
    },
    Kontragent: {
        KontragentId: 0,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``
    },
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    TaxPostingsMode: ProvidePostingType.Auto,
    PostingsAndTaxMode: ProvidePostingType.Auto,
    ProvideInAccounting: true,
    AccountingPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    isOther: true,
    NdsType: null,
    NdsSum: null,
    IncludeNds: null
};
