import { paymentOrderOperationResources } from '../../../../../../../../resources/MoneyOperationTypeResources';

export default {
    Date: ``,
    Number: ``,
    SettlementAccountId: null,
    Kontragent: {
        KontragentId: null,
        KontragentName: ``,
        KontragentBankName: ``,
        KontragentINN: ``,
        KontragentKPP: ``,
        KontragentSettlementAccount: ``
    },
    Contract: {
        ProjectId: null,
        ProjectNumber: ``,
        KontragentId: null,
        ContractBaseId: null
    },
    Sum: 0,
    Description: ``,
    IsLongTermLoan: false,
    ProvideInAccounting: true,
    TaxPostingsInManualMode: false,
    Status: null,
    OperationType: paymentOrderOperationResources.PaymentOrderOutgoingLoanRepayment,
    LoanInterestSum: null,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    AccountingPostings: {}
};
