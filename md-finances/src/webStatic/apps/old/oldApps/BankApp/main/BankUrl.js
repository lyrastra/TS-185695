(function(url) {

    var prefix = '/Accounting/';

    url.RootPathPage = '';
    url.BaseTemplate = 'ClientSideApps/BankApp/templates/';

    url.GetSettlementAccountsForFirm = prefix + 'SettlementAccounts/GetSettlementAccountsOfBankForFirm';
    url.GetSettlementAccountsOfBankWithDeleted = prefix + 'SettlementAccounts/GetSettlementAccountsOfBankWithDeleted';
    url.GetBankAccountTableInfo = prefix + 'Banks/GetBankAccountTableInfo';
    url.GetBankAccountsShortInfo = prefix + 'Banks/GetBankAccountsShortInfo';

    url.GetOutgoingOperationDictionary = prefix + 'PaymentOrders/GetOutgoingOperationTypes';
    url.GetIncomingOperationDictionary = prefix + 'PaymentOrders/GetIncomingOperationTypes';
    url.GetBudgetaryPaymentReasons = prefix + 'PaymentOrders/GetBudgetaryPaymentReasons';
    url.GetBudgetaryPaymentTypes = prefix + 'PaymentOrders/GetBudgetaryPaymentTypes';
    url.GetBudgetaryStatusesOfPayer = prefix + 'PaymentOrders/GetBudgetaryStatusesOfPayer';
    url.GetBudgetaryTaxesFeesContributions = prefix + 'PaymentOrders/GetBudgetaryTaxesFeesContributions';
    url.GetBudgetaryPaymentSequences = prefix + 'PaymentOrders/GetBudgetaryPaymentSequences';

    url.GetKontragentsForAccountingPaymentOrderAutocomplete = prefix + 'Kontragents/GetKontragentsForAccountingPaymentOrderAutocomplete';
    url.GetCashForFirm = prefix + 'FirmCash/GetFirmCashAutocomplete';

    url.PaymentOrders = {};
    url.PaymentOrders.Save = prefix + 'PaymentOrders/Save';
    url.PaymentOrders.Get = prefix + 'PaymentOrders/GetPaymentOrderForFirmById';
    url.PaymentOrders.GetByBaseId = prefix + 'PaymentOrders/GetByBaseId/';
    url.PaymentOrders.GetDefaultModel = prefix + 'PaymentOrders/GetBudgetaryPaymentOrder';
    url.PaymentOrders.GetKbkFieldsModel = prefix + 'PaymentOrders/GetDefaultFieldsByKbk';
    url.PaymentOrders.GetIncomingPaymentOrder = prefix + 'PaymentOrders/GetIncomingPaymentOrder';
    url.PaymentOrders.GetOutgoingPaymentOrder = prefix + 'PaymentOrders/GetOutgoingPaymentOrder';
    url.PaymentOrders.SavePaymentOrder = prefix + 'PaymentOrders/SavePaymentOrder';
    url.PaymentOrders.GetCopyPaymentOrder = prefix + 'PaymentOrders/GetCopyPaymentOrder';
    url.PaymentOrders.GetMemorealOrder = prefix + 'PaymentOrders/GetMemorialWarrant';
    url.PaymentOrders.GetNextPaymentOrderNumberForYear = prefix + 'PaymentOrders/GetNextPaymentOrderNumberForYear';
    url.PaymentOrders.GetOperationType = prefix + 'PaymentOrders/GetOperationTypeByPaymentOrderId';
    url.PaymentOrders.GetByOperation = prefix + 'PaymentOrders/GetPaymentOrderIdByOperation';
    url.PaymentOrders.GetTradingObjectsList = prefix + 'PaymentOrders/GetTradingObjectsList';

    url.IncomingPP = {};
    url.IncomingPP.Edit = 'documents/incomingpp/edit/';
    url.IncomingPP.Copy = 'documents/incomingpp/copy/';

    url.OutgoingPP = {};
    url.OutgoingPP.Edit = 'documents/outgoingpp/edit/';
    url.OutgoingPP.Copy = 'documents/outgoingpp/copy/';

    url.BudgetaryPayment = {};
    url.BudgetaryPayment.Edit = 'documents/outgoingpp/edit/';
    url.BudgetaryPayment.Copy = 'documents/outgoingpp/copy/';

    url.GetPaymentOrdersForFirm = prefix + 'PaymentOrders/GetPaymentOrdersForFirm';
    url.GetAccountSummaryInfo = prefix + 'PaymentOrders/GetAccountSummaryInfo';

    url.GetIncomingReasonDocumentAutocomplete = prefix + 'BankAutocompletes/GetIncomingReasonDocumentAutocomplete';
    url.GetOutgoingReasonDocumentAutocomplete = prefix + 'BankAutocompletes/GetOutgoingReasonDocumentAutocomplete';
    url.GetMediationFeeReasonDocumentAutocomplete = prefix + 'MiddlemanContract/GetMiddlemanDocumentsAutocomplete';

    url.PaymentAutomation = {};
    url.PaymentAutomation.OutgoingReasonDocuments = prefix + 'PaymentAutomation/GetOutgoingReasonDocuments';
    url.PaymentAutomation.IncomingReasonDocuments = prefix + 'PaymentAutomation/GetIncomingReasonDocuments';

    url.GetBankAutocomplete = prefix + 'Banks/GetBanksAutocomplete';

    url.DeleteOrders = prefix + 'PaymentOrders/DeleteOrders';

    url.GetFile = prefix + 'PaymentOrders/GetFile';

    url.GetBankForSettlementAccount = prefix + 'Banks/GetBankForSettlementAccount';

    url.GetKontragentAccountsByPaymentOrderOperationType = prefix + 'ChartOfAccounts/GetKontragentAccountsByPaymentOrderOperationType';

    url.ImportWizard = {};
    url.ImportWizard.ParsePaymentOrdersFormFile = prefix + 'Import/ParsePaymentOrdersFromFile';
    url.ImportWizard.SaveImportedPayments = prefix + 'Import/SaveImportedPayments';
    url.ImportWizard.SendMovementListRequest = prefix + 'Integrations/SendMovementListRequest';
    url.ImportWizard.GetMovementListForSettlementAccount = prefix + 'Import/GetMovementListForSettlementAccount';
    url.ImportWizard.SendPaymentsToIntegrationPartner = prefix + 'Integrations/SendPaymentsToIntegrationPartner';
    url.ImportWizard.SendPaymentToIntegrationPartner = prefix + 'Integrations/SendPaymentToIntegrationPartner';
})(BankUrl);