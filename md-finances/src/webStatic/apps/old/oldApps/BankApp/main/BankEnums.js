import OrderType from '../../../../../enums/OrderTypeEnum';

window.BankEnums.OperationDirection = {
    Other: 0,
    Outgoing: 1,
    Incoming: 2
};

window.BankEnums.menuOperationTypes = {
    All: 1,
    Incoming: 2,
    Outgoing: 3
};

window.BankEnums.OrderType = OrderType;

window.BankEnums.ReasonDocumentTypes = {
    Unknown: 0,
    OutgoingWaybill: 1,
    IncomingWaybill: 2,
    OutgoingStatement: 3,
    IncomingStatement: 4,
    ReturnToSupplier: 5,
    ReturnFromBuyer: 6,
    AcceptanceIncomingStatement: 8,
    AdvanceStatement: 9
};

window.BankEnums.ExtendedFilterStatus = {
    Number: 0,
    Sum: 1,
    Kontragent: 2,
    Purpose: 3
};

window.BankEnums.ReturnTypes = {
    AdvanceReturn: 1,
    CostReturn: 2
};

window.BankEnums.IncomingReasonDocuments = {
    акт: window.BankEnums.ReasonDocumentTypes.OutgoingStatement,
    накладную: window.BankEnums.ReasonDocumentTypes.OutgoingWaybill,
    // 'возврат поставщику': window.BankEnums.ReasonDocumentTypes.ReturnToSupplier,
    'другой документ': ``
};

window.BankEnums.OutgoingReasonDocuments = {
    акт: window.BankEnums.ReasonDocumentTypes.IncomingStatement,
    'акт приема-передачи ОС': window.BankEnums.ReasonDocumentTypes.AcceptanceIncomingStatement,
    накладную: window.BankEnums.ReasonDocumentTypes.IncomingWaybill,
    // 'возврат от покупателя': window.BankEnums.ReasonDocumentTypes.ReturnFromBuyer
    'другой документ': ``
};

window.BankEnums.OutgoingPaymentToSupplierForPropertyDocuments = {
    'акт приема-передачи': window.BankEnums.ReasonDocumentTypes.AcceptanceIncomingStatement
};

window.BankEnums.TripleCalendarTypes = {
    Year: 1,
    HalfYear: 2,
    Quarter: 3,
    Month: 4,
    NoPeriod: 8,
    Date: 9
};

window.BankEnums.PPStatus = {
    NotPaid: 0,
    Paid: 1
};

window.BankEnums.StepTypes = {
    Completed: `completed`,
    Current: `current`,
    Future: `future`,
    Disabled: `disabled`
};

window.BankEnums.ImportPaymentStatuses = {
    Default: 0,
    Added: 1,
    StatusChange: 2,
    Dublicate: 3
};

window.BankEnums.ImportErrors = {
    NotError: 0,
    CreateKontragent: 1,
    UpdateKontragent: 2,
    ChangeStatus: 3,
    CreatePaymentOrder: 4,
    CreateSettlementAccount: 5
};

window.BankEnums.ImportSource = {
    File: 1,
    Server: 2
};

window.BankEnums.ImportAutocompleteSettings = {
    SearchByInn: 1,
    SearchByDescription: 2,
    DisableAutocomplete: 3
};
