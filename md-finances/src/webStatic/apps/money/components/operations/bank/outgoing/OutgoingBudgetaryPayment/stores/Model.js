import KontragentsFormEnum from '@moedelo/frontend-enums/mdEnums/KontragentsForm';
import KbkTypesEnum from '../../../../../../../../enums/newMoney/budgetaryPayment/BudgetaryKbkTypesEnum';

export default {
    AccountType: 0,
    AccountCode: 0,
    Date: ``,
    Number: ``,
    Sum: 0,
    Uin: `0`,
    Description: ``,
    Period: {
        Type: null,
        CanEditCalendarType: null,
        Date: null,
        MinDate: null,
        Month: null,
        Year: null,
        readOnly: null,
        HalfYear: null,
        Quarter: null
    },
    PayerStatus: null,
    PaymentBase: null,
    DocumentDate: null,
    DocumentNumber: 0,
    PatentId: null,
    KbkPaymentType: KbkTypesEnum.TaxAndFee,
    TaxationSystemType: null,
    Kbk: {
        Id: null,
        Number: ``
    },
    Recipient: {
        Name: ``,
        Inn: null,
        Kpp: null,
        Okato: null,
        Oktmo: null,
        Form: KontragentsFormEnum.UL,
        SettlementAccount: null,
        BankName: null,
        BankBik: null,
        BankCorrespondentAccount: 0
    },
    ProvideInAccounting: true,
    AccountingPostings: {
        Postings: [],
        ExplainingMessage: ``
    },
    TradingObjectId: null,
    TaxPostings: {
        Postings: [],
        ExplainingMessage: ``,
        TaxStatus: null
    },
    Status: false, // todo: заменить на IsPaid,
    CurrencyInvoices: [],
    isComplexDocumentNumber: false, // техническое поле, не участвующее в CRUD непосредтсвенно. только UI и валидация
    complexNumber: {
        literalCode: 0,
        value: ``
    },
    SettlementAccountId: 0,
    OperationState: null,
    SubPayments: [] // виды налогов для ЕНП
};
