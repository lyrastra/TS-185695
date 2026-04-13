import DocumentStatusEnum from '../../../../../../../../enums/DocumentStatusEnum';
import ProvidePostingType from '../../../../../../../../enums/ProvidePostingTypeEnum';

export default {
    Number: ``,
    Date: ``,
    SettlementAccountId: null,
    Sum: 0,
    TotalSum: 0,
    CentralBankRate: 0,
    Description: ``,
    Status: DocumentStatusEnum.Payed,
    OperationType: null,
    Contract: {
        ProjectId: null,
        ProjectNumber: ``,
        KontragentId: null,
        ContractBaseId: null
    },
    Kontragent: {
        KontragentId: null,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``
    },
    Documents: [],
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    AccountingPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    ProvideInAccounting: true,
    TaxPostingsMode: ProvidePostingType.Auto,
    NdsType: null,
    NdsSum: null,
    IncludeNds: null
};
