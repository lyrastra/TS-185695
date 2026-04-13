import {getUrlWithId} from '@moedelo/frontend-core-v2/helpers/companyId';

var WebApp = {
    root: '/Accounting'
};

window.WebApp = WebApp;

WebApp.AccountingPolicies = {};
WebApp.AccountingPolicies.GetRequsites = '/AccountingPolicies/GetRequsites';

WebApp.AccountingPolicy = {};
WebApp.AccountingPolicy.GetAccountingPolicy = '/AccountingPolicy/GetAccountingPolicy';

WebApp.AdditionalDocuments = {};
WebApp.AdditionalDocuments.GetTable = '/AdditionalDocuments/GetTable';
WebApp.AdditionalDocuments.Delete = '/AdditionalDocuments/Delete';

WebApp.AdvanceStatements = {};
WebApp.AdvanceStatements.GetAdvanceStatementsAutocomplete = '/AdvanceStatements/GetAdvanceStatementsAutocomplete';
WebApp.AdvanceStatements.GetAdvanceStatement = '/AdvanceStatements/GetAdvanceStatement';

WebApp.AgentIncoming = {};
WebApp.AgentIncoming.Get = '/AgentIncoming/Get';
WebApp.AgentIncoming.Save = '/AgentIncoming/Save';

WebApp.Banks = {};
WebApp.Banks.GetBanksAutocomplete = '/Banks/GetBanksAutocomplete';
WebApp.Banks.GetBankName = '/Banks/GetBankName';

WebApp.BankFeeOutgoing = {};
WebApp.BankFeeOutgoing.Get = '/BankFeeOutgoing/Get';
WebApp.BankFeeOutgoing.Save = '/BankFeeOutgoing/Save';

WebApp.BalanceAndIncomeReport = {};
WebApp.BalanceAndIncomeReport.AccountingNumberAutocomplete = '/ManuallyPostingStepOfBalanceAndIncomeWizard/GetAccountingNumberAutocomplete';
WebApp.BalanceAndIncomeReport.SubcontoAutocomplete = '/ManuallyPostingStepOfBalanceAndIncomeWizard/GetSubcontoAutocomplete';
WebApp.BalanceAndIncomeReport.CreateSubsidySubconto = '/ManuallyPostingStepOfBalanceAndIncomeWizard/CreateSubsidySubconto';
WebApp.BalanceAndIncomeReport.ProductAutocomplete = '/ManuallyPostingStepOfBalanceAndIncomeWizard/GetProductSubcontoAutocomplete';
WebApp.BalanceAndIncomeReport.StockAutocomplete = '/ManuallyPostingStepOfBalanceAndIncomeWizard/GetStockAutocomplete';

WebApp.Bills = {};

WebApp.Bills.GetBillsForOutgoingAutocomplete = '/Bills/GetBillsForOutgoingAutocomplete';
WebApp.Bills.GetBillsForIncomingAutocomplete = '/Bills/GetBillsForIncomingAutocomplete';
WebApp.Bills.GetBillNumber = '/Bills/GetBillNumber';
WebApp.Bills.GetByDocumentBaseId = '/Bills/GetByDocumentBaseId';
WebApp.Bills.SaveBill = '/Bills/SaveBill';
WebApp.Bills.GetBillOnline = '/Bills/GetBillOnline';
WebApp.Bills.GetIncomings = '/Bills/GetIncomings';
WebApp.Bills.SetCoveredStatus = '/Bills/SetCoveredStatus';
WebApp.Bills.GetPaidStatus = '/Bills/GetPaidStatus';
WebApp.Bills.GetBillItemAutocomplete = '/Bills/GetBillItemAutocomplete';
WebApp.Bills.GetBillWithKontragentAutocomplete = '/Bills/GetBillWithKontragentAutocomplete';
WebApp.Bills.GetBillByDocument = '/Bills/GetBillByDocument';

WebApp.BudgetaryPaymentOutgoing = {};
WebApp.BudgetaryPaymentOutgoing.Get = '/BudgetaryPaymentOutgoing/Get';
WebApp.BudgetaryPaymentOutgoing.Save = '/BudgetaryPaymentOutgoing/Save';

WebApp.Cash = {};
WebApp.Cash.CheckClosedGroupForDate = '/Cash/CheckClosedGroupForDate';

WebApp.CashIncoming = {};
WebApp.CashIncoming.Get = '/CashIncoming/Get';
WebApp.CashIncoming.Save = '/CashIncoming/Save';

WebApp.ConfirmingStatements = {};
WebApp.ConfirmingStatements.GetConfirmingStatementsByFinancialOperationId = '/ConfirmingStatements/GetConfirmingStatementsByFinancialOperationId';

WebApp.ConfirmingWaybills = {};
WebApp.ConfirmingWaybills.GetConfirmingWaybillsByFinancialOperationId = '/ConfirmingWaybills/GetConfirmingWaybillsByFinancialOperationId';

WebApp.ContributionOfOwnFundsIncoming = {};
WebApp.ContributionOfOwnFundsIncoming.Get = '/ContributionOfOwnFundsIncoming/Get';
WebApp.ContributionOfOwnFundsIncoming.Save = '/ContributionOfOwnFundsIncoming/Save';

WebApp.ClosingDocumentsOperation = {};
WebApp.ClosingDocumentsOperation.Delete = '/ClosingDocumentsOperation/Delete';
WebApp.ClosingDocumentsOperation.DocumentNumberNotBusy = '/ClosingDocumentsOperation/DocumentNumberNotBusy';
WebApp.ClosingDocumentsOperation.GetDocumentByBills = '/ClosingDocumentsOperation/GetDocumentByBills';
WebApp.ClosingDocumentsOperation.GetDocumentByProject = '/ClosingDocumentsOperation/GetDocumentByProject';
WebApp.ClosingDocumentsOperation.GetDocument = '/ClosingDocumentsOperation/GetDocument';
WebApp.ClosingDocumentsOperation.GetByDocumentBaseId = '/ClosingDocumentsOperation/GetByDocumentBaseId';
WebApp.ClosingDocumentsOperation.SaveComment = '/ClosingDocumentsOperation/SaveComment';
WebApp.ClosingDocumentsOperation.GetDocumentNumber = '/ClosingDocumentsOperation/GetDocumentNumber';
WebApp.ClosingDocumentsOperation.SaveInvoice = '/OutgoingInvoice/Save';
WebApp.ClosingDocumentsOperation.SaveStatement = '/Statements/SaveStatement';
WebApp.ClosingDocumentsOperation.SaveWaybill = '/Waybills/SaveWaybill';
WebApp.ClosingDocumentsOperation.SaveStatus = '/ClosingDocumentsOperation/SaveStatus';
WebApp.ClosingDocumentsOperation.GetUnitAutocomplete = '/ClosingDocumentsOperation/GetUnitAutocomplete';
WebApp.ClosingDocumentsOperation.GetProductAutocomplete = '/ClosingDocumentsOperation/GetProductAutocomplete';
WebApp.ClosingDocumentsOperation.GetCountryAutocomplete = '/ClosingDocumentsOperation/GetCountryAutocomplete';
WebApp.ClosingDocumentsOperation.GetLinkedDocuments = '/ClosingDocumentsOperation/GetLinkedDocuments';
WebApp.ClosingDocumentsOperation.GetRelatedDocuments = '/ClosingDocumentsOperation/GetRelatedDocuments';
WebApp.ClosingDocumentsOperation.GetPostingsForInvoice = '/OutgoingInvoice/GetPostings';
WebApp.ClosingDocumentsOperation.GetTemporaryPostings = '/OutgoingInvoice/GetAllPostings';
WebApp.ClosingDocumentsOperation.GetPrimaryItemsWithStockAutocomplete = '/ClosingDocumentsOperation/GetPrimaryItemsWithStockAutocomplete';
WebApp.ClosingDocumentsOperation.GetReletedDocumentsForDelete = '/ClosingDocumentsOperation/GetRelatedDocumentsForDelete';
WebApp.ClosingDocumentsOperation.GetDocumentCopies = '/ClosingDocumentsOperation/GetDocumentCopies';
WebApp.ClosingDocumentsOperation.SaveDocumentCopies = '/ClosingDocumentsOperation/SaveDocumentCopies';
WebApp.ClosingDocumentsOperation.GetBillsNumbers = WebApp.ClosingDocumentsOperation.GetDocumentNumber;
WebApp.ClosingDocumentsOperation.CreateBillsByDocs = '/ClosingDocumentsOperation/GenerateBills';
WebApp.ClosingDocumentsOperation.GetDataForDelete = '/ClosingDocumentsOperation/GetDataForDelete';

WebApp.AccountingDocuments = {};
WebApp.AccountingDocuments.Delete = '/AccountingDocuments/Delete';

WebApp.IncomingDocumentsOperation = {};
WebApp.IncomingDocumentsOperation.DocumentNumberNotBusy = '/IncomingDocumentsOperation/DocumentNumberNotBusy';
WebApp.IncomingDocumentsOperation.GetDocumentNumber = '/IncomingDocumentsOperation/GetDocumentNumber';

WebApp.IncomingDocumentsTable = {};
WebApp.IncomingDocumentsTable.GetTable = '/IncomingDocumentsTable/GetTable';
WebApp.IncomingDocumentsTable.GetInvoicesTable = '/IncomingDocumentsTable/GetInvoicesTable';
WebApp.IncomingDocumentsTable.GetFile = '/IncomingDocumentsTable/GetFile';
WebApp.IncomingDocumentsTable.GetFiles = '/IncomingDocumentsTable/GetFiles';

WebApp.OutgoingDocumentsTable = {};
WebApp.OutgoingDocumentsTable.GetTable = '/OutgoingDocumentsTable/GetTable';
WebApp.OutgoingDocumentsTable.GetInvoicesTable = '/OutgoingDocumentsTable/GetInvoicesTable';

WebApp.IncomingWaybill = {};
WebApp.IncomingWaybill.Get = '/IncomingWaybill/Get';
WebApp.IncomingWaybill.GetByBaseId = '/IncomingWaybill/GetByBaseId';
WebApp.IncomingWaybill.Save = '/IncomingWaybill/Save';
WebApp.IncomingWaybill.GetPostingsForOperation = '/IncomingWaybill/GetPostingsForOperation';
WebApp.IncomingWaybill.GetAllPostings = '/IncomingWaybill/GetAllPostings';
WebApp.IncomingWaybill.GetAllTaxPostings = '/IncomingWaybill/GetAllTaxPostings';
WebApp.IncomingWaybill.GetDocumentCopies = '/IncomingWaybill/GetDocumentCopies';
WebApp.IncomingWaybill.SaveDocumentCopies = '/IncomingWaybill/SaveDocumentCopies';

WebApp.IncomingInvoice = {};
WebApp.IncomingInvoice.Get = '/IncomingInvoice/Get';
WebApp.IncomingInvoice.GetByDocumentBaseId = '/IncomingInvoice/GetByDocumentBaseId';
WebApp.IncomingInvoice.Save = '/IncomingInvoice/Save';
WebApp.IncomingInvoice.GetReasonAutocomplete = '/IncomingInvoice/GetReasonAutocomplete';
WebApp.IncomingInvoice.GetPostingsForOperation = '/IncomingInvoice/GetPostingsForOperation';
WebApp.IncomingInvoice.GetAllPostings = '/IncomingInvoice/GetAllPostings';
WebApp.IncomingInvoice.GetAllTaxPostings = '/IncomingInvoice/GetAllTaxPostings';
WebApp.IncomingInvoice.GetDocumentCopies = '/IncomingInvoice/GetDocumentCopies';
WebApp.IncomingInvoice.SaveDocumentCopies = '/IncomingInvoice/SaveDocumentCopies';

WebApp.IncomingStatement = {};
WebApp.IncomingStatement.Get = '/IncomingStatement/Get';
WebApp.IncomingStatement.GetByBaseId = '/IncomingStatement/GetByDocumentBaseId';
WebApp.IncomingStatement.Save = '/IncomingStatement/Save';
WebApp.IncomingStatement.GetPostingsForOperation = '/IncomingStatement/GetPostingsForOperation';
WebApp.IncomingStatement.GetAllTaxPostings = '/IncomingStatement/GetAllTaxPostings';
WebApp.IncomingStatement.GetAllPostings = '/IncomingStatement/GetAllPostings';
WebApp.IncomingStatement.GetDocumentCopies = '/IncomingStatement/GetDocumentCopies';
WebApp.IncomingStatement.SaveDocumentCopies = '/IncomingStatement/SaveDocumentCopies';

WebApp.ActivityAccounts = {};
WebApp.ActivityAccounts.GetAccountsToStatement = '/ActivityAccounts/GetAccountsToStatement';

WebApp.ClosingDocumentsTable = {};
WebApp.ClosingDocumentsTable.GetTable = '/ClosingDocumentsTable/GetTable';
WebApp.ClosingDocumentsTable.GetPositionsInDoc = '/ClosingDocumentsTable/GetPositionsInDoc';
WebApp.ClosingDocumentsTable.GetFile = '/ClosingDocumentsTable/GetFile';

WebApp.ElectronicOutgoing = {};
WebApp.ElectronicOutgoing.Get = '/ElectronicOutgoing/Get';
WebApp.ElectronicOutgoing.Save = '/ElectronicOutgoing/Save';

WebApp.FinancialOperations = {};
WebApp.FinancialOperations.GetOutgoingMoneyTransferOperationsAutocomplete = '/FinancialOperations/GetOutgoingMoneyTransferOperationsAutocomplete';
WebApp.FinancialOperations.GetOutgoingMoneyTransferOperation = '/FinancialOperations/GetOutgoingMoneyTransferOperation';

WebApp.FirmTaxationSystems = {};
WebApp.FirmTaxationSystems.GetAll = '/FirmTaxationSystems/GetAll';

WebApp.FromCashToSettlementMovement = {};
WebApp.FromCashToSettlementMovement.Get = '/FromCashToSettlementMovement/Get';
WebApp.FromCashToSettlementMovement.Save = '/FromCashToSettlementMovement/Save';

WebApp.FromSettlementToCashMovement = {};
WebApp.FromSettlementToCashMovement.Get = '/FromSettlementToCashMovement/Get';
WebApp.FromSettlementToCashMovement.Save = '/FromSettlementToCashMovement/Save';

WebApp.FromSettlementToSettlementMovement = {};
WebApp.FromSettlementToSettlementMovement.Get = '/FromSettlementToSettlementMovement/Get';
WebApp.FromSettlementToSettlementMovement.Save = '/FromSettlementToSettlementMovement/Save';

WebApp.GetMoneyForEmployeeOutgoing = {};
WebApp.GetMoneyForEmployeeOutgoing.Get = '/GetMoneyForEmployeeOutgoing/Get';
WebApp.GetMoneyForEmployeeOutgoing.Save = '/GetMoneyForEmployeeOutgoing/Save';

WebApp.Kbk = {};
WebApp.Kbk.GetAutocomplete = '/Kbk/GetAutocomplete';

WebApp.KontragentBill = {};
WebApp.KontragentBill.GetAutocomplete = '/KontragentBill/GetAutocomplete';

WebApp.KontragentClosingDocs = {};
WebApp.KontragentClosingDocs.GetAutocomplete = '/KontragentClosingDocs/GetAutocomplete';
WebApp.KontragentClosingDocs.GetAutocompleteForWaybill = '/KontragentClosingDocs/GetAutocompleteForWaybill';

WebApp.KontragentsAutocomplete = '/Kontragents/Autocomplete/KontragentWithSettlementAccountAutocomplete';

WebApp.Kontragents = {};
WebApp.Kontragents.Save = '/Kontragents/Save';
WebApp.Kontragents.GetFoundersAutocomplete = '/Kontragents/GetFoundersAutocomplete';
WebApp.Kontragents.GetKontragentsAndWorkersAutocomplete = '/Kontragents/GetKontragentsAndWorkersAutocomplete';
WebApp.Kontragents.GetWorkersFromMoneyAutocomplete = '/Kontragents/GetWorkersFromMoneyAutocomplete';
WebApp.Kontragents.GetWorkersAndIpAutocomplete = '/Kontragents/GetWorkersAndIpAutocomplete';
WebApp.Kontragents.GetWorkersAutocompleteForMoneyBalanceMaster = '/Kontragents/GetWorkersAutocompleteForMoneyBalanceMaster';
WebApp.Kontragents.GetFounderName = '/Kontragents/GetFounderName';
WebApp.Kontragents.GetKontragentOrWorkerName = '/Kontragents/GetKontragentOrWorkerName';
WebApp.Kontragents.GetKontragentsByEmailAutocomplete = '/Kontragents/GetKontragentsByEmailAutocomplete';
WebApp.Kontragents.GetWorkerSyntheticAccountCodeOnDate = '/Kontragents/GetWorkerSyntheticAccountCodeOnDate';

WebApp.LoanParentIncoming = {};
WebApp.LoanParentIncoming.Get = '/LoanParentIncoming/Get';
WebApp.LoanParentIncoming.Save = '/LoanParentIncoming/Save';

WebApp.LoanParentOutgoing = {};
WebApp.LoanParentOutgoing.Get = '/LoanParentOutgoing/Get';
WebApp.LoanParentOutgoing.Save = '/LoanParentOutgoing/Save';

WebApp.LoansThirdPartiesIncoming = {};
WebApp.LoansThirdPartiesIncoming.Get = '/LoansThirdPartiesIncoming/Get';
WebApp.LoansThirdPartiesIncoming.Save = '/LoansThirdPartiesIncoming/Save';

WebApp.LoansThirdPartiesOutgoing = {};
WebApp.LoansThirdPartiesOutgoing.Get = '/LoansThirdPartiesOutgoing/Get';
WebApp.LoansThirdPartiesOutgoing.Save = '/LoansThirdPartiesOutgoing/Save';

WebApp.MailingDocuments = {};
WebApp.MailingDocuments.CreateDocsForMail = '/MailingDocuments/CreateDocsForMail';
WebApp.MailingDocuments.SendDocumentByMail = '/MailingDocuments/SendDocumentByMail';
WebApp.MailingDocuments.GetAttachmentFile = '/MailingDocuments/GetAttachmentFile';

WebApp.MainActivityOutgoing = {};
WebApp.MainActivityOutgoing.Get = '/MainActivityOutgoing/Get';
WebApp.MainActivityOutgoing.Save = '/MainActivityOutgoing/Save';

WebApp.MaterialAidIncoming = {};
WebApp.MaterialAidIncoming.Get = '/MaterialAidIncoming/Get';
WebApp.MaterialAidIncoming.Save = '/MaterialAidIncoming/Save';

WebApp.MoneyBalanceMaster = {};
WebApp.MoneyBalanceMaster.GetFixedAssetsBalance = '/MoneyBalanceMaster/GetFixedAssetsBalance';
WebApp.MoneyBalanceMaster.SaveFixedAssetsBalance = '/MoneyBalanceMaster/SaveFixedAssetsBalance';
WebApp.MoneyBalanceMaster.GetCashBalance = '/MoneyBalanceMaster/GetCashBalance';
WebApp.MoneyBalanceMaster.SaveCashBalance = '/MoneyBalanceMaster/SaveCashBalance';
WebApp.MoneyBalanceMaster.GetPurses = '/MoneyBalanceMaster/GetPurses';
WebApp.MoneyBalanceMaster.SavePurses = '/MoneyBalanceMaster/SavePurses';
WebApp.MoneyBalanceMaster.GetBuyersAndCustomersBalance = '/MoneyBalanceMaster/GetBuyersAndCustomersBalance';
WebApp.MoneyBalanceMaster.SaveBuyersAndCustomersBalance = '/MoneyBalanceMaster/SaveBuyersAndCustomersBalance';
WebApp.MoneyBalanceMaster.GetFounders = '/MoneyBalanceMaster/GetFounders';
WebApp.MoneyBalanceMaster.SaveFounders = '/MoneyBalanceMaster/SaveFounders';
WebApp.MoneyBalanceMaster.GetFundsBalance = '/MoneyBalanceMaster/GetFundsBalance';
WebApp.MoneyBalanceMaster.SaveFundsBalance = '/MoneyBalanceMaster/SaveFundsBalance';
WebApp.MoneyBalanceMaster.GetLoansBalance = '/MoneyBalanceMaster/GetLoansBalance';
WebApp.MoneyBalanceMaster.SaveLoansBalance = '/MoneyBalanceMaster/SaveLoansBalance';
WebApp.MoneyBalanceMaster.GetCurrentStep = '/MoneyBalanceMaster/GetCurrentStep';
WebApp.MoneyBalanceMaster.GetSuppliersAndContractorsBalance = '/MoneyBalanceMaster/GetSuppliersAndContractorsBalance';
WebApp.MoneyBalanceMaster.SaveSuppliersAndContractorsBalance = '/MoneyBalanceMaster/SaveSuppliersAndContractorsBalance';
WebApp.MoneyBalanceMaster.GetOtherKontragentsBalance = '/MoneyBalanceMaster/GetOtherKontragentsBalance';
WebApp.MoneyBalanceMaster.SaveOtherKontragentsBalance = '/MoneyBalanceMaster/SaveOtherKontragentsBalance';
WebApp.MoneyBalanceMaster.GetEmployeeBalance = '/MoneyBalanceMaster/GetEmployeeBalance';
WebApp.MoneyBalanceMaster.SaveEmployeeBalance = '/MoneyBalanceMaster/SaveEmployeeBalance';
WebApp.MoneyBalanceMaster.GetSalaryBalance = '/MoneyBalanceMaster/GetSalaryBalance';
WebApp.MoneyBalanceMaster.SaveSalaryBalance = '/MoneyBalanceMaster/SaveSalaryBalance';
WebApp.MoneyBalanceMaster.GetStoreBalance = '/MoneyBalanceMaster/GetStoreBalance';
WebApp.MoneyBalanceMaster.SaveStoreBalance = '/MoneyBalanceMaster/SaveStoreBalance';
WebApp.MoneyBalanceMaster.SaveWelcomeStep = '/MoneyBalanceMaster/SaveWelcomeStep';
WebApp.MoneyBalanceMaster.IsExistCashOperationsBeforeYear = '/MoneyBalanceMaster/IsExistCashOperationsBeforeYear';
WebApp.MoneyBalanceMaster.GetSettlementAccountsBalance = '/MoneyBalanceMaster/GetSettlementAccountsBalance';
WebApp.MoneyBalanceMaster.SaveSettlementAccountsBalance = '/MoneyBalanceMaster/SaveSettlementAccountsBalance';

WebApp.MoneyTransferOperation = {};
WebApp.MoneyTransferOperation.GetBillsSum = '/MoneyTransferOperation/GetBillsSum';
WebApp.MoneyTransferOperation.UnbindAdvanceStatement = '/MoneyTransferOperation/UnbindAdvanceStatement';
WebApp.MoneyTransferOperation.GetBankFeeOperationNextNumber = '/MoneyTransferOperation/GetBankFeeOperationNextNumber';
WebApp.MoneyTransferOperation.GetOutgoingMoneyTransferOperationNextNumber = '/MoneyTransferOperation/GetOutgoingMoneyTransferOperationNextNumber';
WebApp.MoneyTransferOperation.GetIncomingMoneyTransferOperationNextNumber = '/MoneyTransferOperation/GetIncomingMoneyTransferOperationNextNumber';
WebApp.MoneyTransferOperation.GetTaxAllotment = '/MoneyTransferOperation/GetTaxAllotment';

WebApp.MoneyTransferTable = {};
WebApp.MoneyTransferTable.GetBlockMainAccount = '/MoneyTransferTable/GetBlockMainAccount';

WebApp.OperatingExpensesOutgoing = {};
WebApp.OperatingExpensesOutgoing.Get = '/OperatingExpensesOutgoing/Get';
WebApp.OperatingExpensesOutgoing.Save = '/OperatingExpensesOutgoing/Save';

WebApp.OtherIncoming = {};
WebApp.OtherIncoming.Get = '/OtherIncoming/Get';
WebApp.OtherIncoming.Save = '/OtherIncoming/Save';


WebApp.CurrencySellBuy = {
    Get: '/CurrencyPurchaseAndSaleIncoming/Get',
    Save: '/CurrencyPurchaseAndSaleIncoming/Save'
};

WebApp.OtherOutgoing = {};
WebApp.OtherOutgoing.Get = '/OtherOutgoing/Get';
WebApp.OtherOutgoing.Save = '/OtherOutgoing/Save';

WebApp.OutgoingStatement = {};
WebApp.OutgoingStatement.GetByDocumentBaseId = '/OutgoingStatement/GetByDocumentBaseId';
WebApp.OutgoingStatement.Save = '/OutgoingStatement/Save';
WebApp.OutgoingStatement.GetAllTaxPostings = '/OutgoingStatement/GetAllTaxPostings';
WebApp.OutgoingStatement.GetAllPostings = '/OutgoingStatement/GetAllPostings';
WebApp.OutgoingStatement.GetDocumentCopies = '/OutgoingStatement/GetDocumentCopies';
WebApp.OutgoingStatement.SaveDocumentCopies = '/OutgoingStatement/SaveDocumentCopies';

WebApp.OutgoingInvoice = {};
WebApp.OutgoingInvoice.GetDocumentCopies = '/OutgoingInvoice/GetDocumentCopies';
WebApp.OutgoingInvoice.SaveDocumentCopies = '/OutgoingInvoice/SaveDocumentCopies';

WebApp.OutgoingWaybill = {};
WebApp.OutgoingWaybill.Get = '/OutgoingWaybill/GetByBaseId';
WebApp.OutgoingWaybill.Save = '/OutgoingWaybill/Save';
WebApp.OutgoingWaybill.GetPostingsForOperation = '/OutgoingWaybill/GetPostingsForOperation';
WebApp.OutgoingWaybill.GetAllPostings = '/OutgoingWaybill/GetAllPostings';
WebApp.OutgoingWaybill.GetAllTaxPostings = '/OutgoingWaybill/GetAllTaxPostings';
WebApp.OutgoingWaybill.GetDocumentCopies = '/OutgoingWaybill/GetDocumentCopies';
WebApp.OutgoingWaybill.SaveDocumentCopies = '/OutgoingWaybill/SaveDocumentCopies';

WebApp.PayDaysOutgoing = {};
WebApp.PayDaysOutgoing.Get = '/PayDaysOutgoing/Get';
WebApp.PayDaysOutgoing.Save = '/PayDaysOutgoing/Save';

WebApp.Projects = {};
WebApp.Projects.GetProjectsAutocomplete = '/Projects/GetProjectsAutocomplete';
WebApp.Projects.GetProjectsOfFoundersAutocomplete = '/Projects/GetProjectsOfFoundersAutocomplete';
WebApp.Projects.GetProjectNumber = '/Projects/GetProjectNumber';



WebApp.ProvisionOfServivesIncoming = {};
WebApp.ProvisionOfServivesIncoming.Get = '/ProvisionOfServivesIncoming/Get';
WebApp.ProvisionOfServivesIncoming.Save = '/ProvisionOfServivesIncoming/Save';

WebApp.PurchaseOfFixedAssetsOutgoing = {};
WebApp.PurchaseOfFixedAssetsOutgoing.GetNextInventoryNumber = '/PurchaseOfFixedAssetsOutgoing/GetNextInventoryNumber';
WebApp.PurchaseOfFixedAssetsOutgoing.Get = '/PurchaseOfFixedAssetsOutgoing/Get';
WebApp.PurchaseOfFixedAssetsOutgoing.Save = '/PurchaseOfFixedAssetsOutgoing/Save';

WebApp.PrimaryDocuments = {};
WebApp.PrimaryDocuments.InvoiceAutocomplete = '/PrimaryDocumentAutocomplete/GetInvoiceAutocomplete';

WebApp.PurseIncoming = {};
WebApp.PurseIncoming.Get = '/PurseIncoming/Get';
WebApp.PurseIncoming.Save = '/PurseIncoming/Save';

WebApp.PurseOutgoing = {};
WebApp.PurseOutgoing.Get = '/PurseOutgoing/Get';
WebApp.PurseOutgoing.Save = '/PurseOutgoing/Save';

WebApp.Purses = {};
WebApp.Purses.GetBankSettlementPurses = '/Purses/GetBankSettlementPursesForFirm';
WebApp.Purses.GetSettlementPurses = '/Purses/GetSettlementPursesForFirm';

WebApp.RefundFromEmployeeIncoming = {};
WebApp.RefundFromEmployeeIncoming.Get = '/RefundFromEmployeeIncoming/Get';
WebApp.RefundFromEmployeeIncoming.Save = '/RefundFromEmployeeIncoming/Save';

WebApp.RefundFromBudgetIncoming = {};
WebApp.RefundFromBudgetIncoming.Get = '/RefundFromBudgetIncoming/Get';
WebApp.RefundFromBudgetIncoming.Save = '/RefundFromBudgetIncoming/Save';

WebApp.RefundToCustomerOutgoing = {};
WebApp.RefundToCustomerOutgoing.Get = '/RefundToCustomerOutgoing/Get';
WebApp.RefundToCustomerOutgoing.Save = '/RefundToCustomerOutgoing/Save';

WebApp.RemovingTheProfitOutgoing = {};
WebApp.RemovingTheProfitOutgoing.Get = '/RemovingTheProfitOutgoing/Get';
WebApp.RemovingTheProfitOutgoing.Save = '/RemovingTheProfitOutgoing/Save';

WebApp.ReturnFalseMeansIncoming = {};
WebApp.ReturnFalseMeansIncoming.Get = '/ReturnFalseMeansIncoming/Get';
WebApp.ReturnFalseMeansIncoming.Save = '/ReturnFalseMeansIncoming/Save';

WebApp.ReturnFromSupplierIncoming = {};
WebApp.ReturnFromSupplierIncoming.Get = '/ReturnFromSupplierIncoming/Get';
WebApp.ReturnFromSupplierIncoming.Save = '/ReturnFromSupplierIncoming/Save';

WebApp.ReturnFromBuyer = {};
WebApp.ReturnFromBuyer.Get = '/ReturnFromBuyer/Get';
WebApp.ReturnFromBuyer.Save = '/ReturnFromBuyer/Save';
WebApp.ReturnFromBuyer.GetPostingsForOperation = '/ReturnFromBuyer/GetPostingsForOperation';
WebApp.ReturnFromBuyer.GetLinkedDocuments = '/ReturnFromBuyer/GetLinkedDocuments';

WebApp.Requisites = {};
WebApp.Requisites.Get = '/Requisites/Get';
WebApp.Requisites.GetActiveIntegrations = '/Requisites/Integrations/GetActiveIntegrations';

WebApp.SaleProductIncoming = {};
WebApp.SaleProductIncoming.Get = '/SaleProductIncoming/Get';
WebApp.SaleProductIncoming.Save = '/SaleProductIncoming/Save';

WebApp.Statements = {};
WebApp.Statements.GetStatementsForIncomingAutocomplete = '/Statements/GetStatementsForIncomingAutocomplete';
WebApp.Statements.GetStatementsForOutgoingAutocomplete = '/Statements/GetStatementsForOutgoingAutocomplete';

WebApp.SettlementAccounts = {};
WebApp.SettlementAccounts.GetSettlementAccounts = '/SettlementAccounts/GetSettlementAccountsForFirmWithDeleted';
WebApp.SettlementAccounts.GetSettlementAccountByBillId = '/SettlementAccounts/GetSettlementAccountByBillId';
WebApp.SettlementAccounts.GetMoneyBayFilter = '/MoneyTransferTable/GetMoneyBayFilter';

WebApp.Subcontos = {};
WebApp.Subcontos.GetSubcontosAutocomplete = '/Subcontos/GetSubcontosAutocomplete';
WebApp.Subcontos.GetSubcontosAutocompleteForPostings = '/Subcontos/GetSubcontosAutocompleteForPostings';
WebApp.Subcontos.GetMainCashSubcontoAutocomplete = '/Subcontos/GetMainCashSubcontoAutocomplete';
WebApp.Subcontos.GetOtherCashSubcontoAutocomplete = '/Subcontos/GetOtherCashSubcontoAutocomplete';

WebApp.UkInpamentIncoming = {};
WebApp.UkInpamentIncoming.Get = '/UkInpamentIncoming/Get';
WebApp.UkInpamentIncoming.Save = '/UkInpamentIncoming/Save';

WebApp.UserFirmRule = {};
WebApp.UserFirmRule.GetMoneyPermissions = '/UserFirmRule/GetMoneyPermissions';
WebApp.UserFirmRule.GetAdditionalDocumentsPermissions = '/UserFirmRule/GetAdditionalDocumentsPermissions';

WebApp.Waybills = {};
WebApp.Waybills.GetWaybillsForIncomingAutocomplete = '/Waybills/GetWaybillsForIncomingAutocomplete';
WebApp.Waybills.GetWaybillsForOutgoingAutocomplete = '/Waybills/GetWaybillsForOutgoingAutocomplete';

WebApp.AccountingStatements = {};

WebApp.AccountingStatements.Get = '/AccountingStatements/GetAccountingStatements';
WebApp.AccountingStatements.GetByBaseId = '/AccountingStatements/GetAccountingStatementsByBaseId';
WebApp.AccountingStatements.GetForFixedAsset = '/AccountingStatements/GetAccountingStatementsForFixedAsset';
WebApp.AccountingStatements.Save = '/AccountingStatements/SaveAccountingStatements';
WebApp.AccountingStatements.Delete = '/AccountingStatements/DeleteAccountingStatements';
WebApp.AccountingStatements.GetAccountingStatementsNumber = '/AccountingStatements/GetAccountingStatementsNumber';
WebApp.AccountingStatements.GetAccessToAccountingStatments = '/AccountingStatements/GetAccessToAccoiuntingStatements';
WebApp.AccountingStatements.GetAllPostings = '/AccountingStatements/GetAllPostings';
WebApp.AccountingStatements.GetAllTaxPostings = '/AccountingStatements/GetAllTaxPostings';
WebApp.AccountingStatements.GetAccountingStatementsFromUsnDeclaration = '/AccountingStatements/GetAccountingStatementsFromUsnDeclaration';
WebApp.AccountingStatements.GetAccountingStatementsFromEnvdDeclaration = '/AccountingStatements/GetAccountingStatementsFromEnvdDeclaration';

WebApp.ChartOfAccount = {};
WebApp.ChartOfAccount.SubcontoLevelForAccount = '/ChartOfAccounts/GetSubcontoLevelForAccount';
WebApp.ChartOfAccount.GetSupplierAccounts = '/ChartOfAccounts/GetSupplierAccounts';
WebApp.ChartOfAccount.GetClientAccounts = '/ChartOfAccounts/GetClientAccounts';

WebApp.SyntheticAccount = {};
WebApp.SyntheticAccount.GetSyntheticAccountAutocomplete = '/ChartOfAccounts/GetSyntheticAccountAutocomplete';

WebApp.AccountingPaymentOrder = {};
WebApp.AccountingPaymentOrder.GetPaymentNumber = '/PaymentOrders/GetNextNumberForAccountingPaymentOrder';
WebApp.AccountingPaymentOrder.GetPostings = '/PaymentOrders/GetPostings';
WebApp.AccountingPaymentOrder.GetPaymentOrderPostings = '/PaymentOrders/GetPaymentOrderPostings';
WebApp.AccountingPaymentOrder.GetAllPostings = '/PaymentOrders/GetAllPostings';
WebApp.AccountingPaymentOrder.GetAllTaxPostings = '/PaymentOrders/GetAllTaxPostings';
WebApp.AccountingPaymentOrder.GetPaymentOrderTaxPostings = '/PaymentOrders/GetAllTaxPostings';
WebApp.AccountingPaymentOrder.DocumentNumberNotBusy = '/PaymentOrders/DocumentNumberNotBusy';
WebApp.AccountingPaymentOrder.GetMemorialWarrantAutocomplete = '/PaymentOrders/GetMemorialWarrantAutocomplete';
WebApp.AccountingPaymentOrder.GetLinkedDocuments = '/PaymentOrders/GetLinkedDocuments';

WebApp.CashOrder = {};
WebApp.CashOrder.GetPostings = '/FirmCash/GetPostings';
WebApp.CashOrder.GetCashOrderWithRetailAutocomplete = '/FirmCash/GetCashOrderWithRetailAutocomplete';
WebApp.CashOrder.GetLinkedDocuments = '/FirmCash/GetLinkedDocuments';
WebApp.CashOrder.GetAllPostings = '/FirmCash/GetAllPostings';
WebApp.CashOrder.GetAllTaxPostings = '/FirmCash/GetAllTaxPostings';
WebApp.CashOrder.GetCashOrderIncomeFromCashAutocomplete = '/BankAutocompletes/GetCashOrderIncomeFromCashAutocomplete';
WebApp.CashOrder.GetCashOrderRemoveFromSettlementAccountAutocomplete = '/BankAutocompletes/GetCashOrderRemoveFromSettlementAccountAutocomplete';

WebApp.AccountingAdvanceStatement = {};
WebApp.AccountingAdvanceStatement.GetWorkerWithAdvanceAutocomplete = '/AccountingAdvanceStatement/GetWorkerWithAdvanceAutocomplete';
WebApp.AccountingAdvanceStatement.GetAdvancePaymentDocument = '/AccountingAdvanceStatement/GetAdvancePaymentDocuments';
WebApp.AccountingAdvanceStatement.GetAccountsToAdvanceStatement = '/ActivityAccounts/GetAccountsToAdvanceStatement';
WebApp.AccountingAdvanceStatement.GetPostingsForOperation = '/AccountingAdvanceStatement/GetPosting';
WebApp.AccountingAdvanceStatement.GetAdvanceStatementFile = '/IncomingDocumentsTable/GetFile';
WebApp.AccountingAdvanceStatement.GetAccountingAdvanceStatementAutocomplete = '/AccountingAdvanceStatement/GetAccountingAdvanceStatementAutocomplete';

WebApp.BizAdvanceStatement = {};
WebApp.BizAdvanceStatement.UnbindDebtCashOrder = '/BizAdvanceStatement/UnbindDebtCashOrder';

WebApp.BizRetailReport = {};
WebApp.BizRetailReport.Save = '/RetailReport/Save';
WebApp.BizRetailReport.GetByDocumentBaseId = '/RetailReport/GetByDocumentBaseId';
WebApp.BizRetailReport.GetTable = '/RetailReport/GetTable';
WebApp.BizRetailReport.GetNextNumber = '/RetailReport/GetNextNumber';
WebApp.BizRetailReport.GetReasonDocuments = '/BizFirmCash/GetZOrders';
WebApp.BizRetailReport.DocumentNumberNotBusy = '/ClosingDocumentsOperation/DocumentNumberNotBusy';
WebApp.BizRetailReport.GetLinkedDocuments = '/RetailReport/GetRelatedDocuments';
WebApp.BizRetailReport.Delete = '/AccountingDocuments/Delete';
WebApp.BizRetailReport.isCrossedWithPreviousReports = '/RetailReport/PeriodIsCrossedWithPreviousReports';

WebApp.ImportToExcel = {};
WebApp.ImportToExcel.GetSaleFile = '/ImportToExcel/SaleImport';
WebApp.ImportToExcel.GetBuyFile = '/ImportToExcel/BuyImport';

WebApp.MiddlemanContract = {};
WebApp.MiddlemanContract.GetContractNumberAutocomplete = '/MiddlemanContract/GetMiddlemanContractNumberAutocomplete';
WebApp.MiddlemanContract.GetMiddlemanPaymentsAutocomplete = '/MiddlemanContract/GetMiddlemanPaymentsAutocomplete';

WebApp.LinkedDocuments = {};
WebApp.LinkedDocuments.GetReasonDocumentId = '/LinkedDocuments/GetReasonDocumentId';

//------------------------------ WebApp extension --------------------------------------
mergerPrefix(WebApp);


var Contracts = {
    root: '/Contract',
    Autocomplete: '/Contract/Autocomplete'
};


var PayrollApp = {
    root: '/Payroll'
};

PayrollApp.MoneyBalanceMaster = {};
PayrollApp.MoneyBalanceMaster.GetSalaryBalance = '/MoneyBalanceMaster/GetSalaryBalanceUpToYear';
PayrollApp.MoneyBalanceMaster.GetFundsBalanceUpToYear = '/MoneyBalanceMaster/GetFundsBalanceUpToYear';

PayrollApp.DocumentLoad = {};
PayrollApp.DocumentLoad.PaymentOrderToFund = '/DocumentLoad/PaymentOrderToFundFromMoney';

PayrollApp.PaymentCalendar = {};
PayrollApp.PaymentCalendar.CompleteEvent = '/PaymentCalendar/CompleteEvent';

PayrollApp.MoneyTransferTable = {};
PayrollApp.MoneyTransferTable.WorkerAutocomplete = '/Workers/GetWorkersForMoneyTableAutocomplete';

mergerPrefix(PayrollApp);

var RequisitesApp = {
    root: '/Requisites'
};

RequisitesApp.Services = {};
RequisitesApp.Services.SaveSettlementAccount = '/SaveSettlementAccount';
RequisitesApp.Services.IsNumberExist = '/IsNumberExist';
mergerPrefix(RequisitesApp);

window.App = {
    root: '/App'
};

App.AccountingAdvanceStatement = {};
App.AccountingAdvanceStatement.Edit = '/AdvanceStatement?id=';
mergerPrefix(App);

window.StockApp = {
    root: '/Stock',
};

StockApp.Stock = {};
StockApp.Stock.GetProductAutocomplete = '/StockProduct/GetProductAutocomplete';
StockApp.Stock.GetStockAutocomplete = '/FirmStock/GetStockAutocomplete';
StockApp.Stock.GetDebitPostings = '/StockOperation/GetPostingsForDebitOperation';
mergerPrefix(StockApp);

window.KontragentsApp = {
    root: '/Kontragents',
};

KontragentsApp.KontragentMoneyTable = {};
KontragentsApp.KontragentMoneyTable.GetAutocomplete = '/Autocomplete/KontragentWithoutIdsAutocomplete';

KontragentsApp.Autocomplete = {};
KontragentsApp.Autocomplete.GetKontragentFoundersAutocomplete = '/Autocomplete/GetKontragentFounderAutocomplete';
KontragentsApp.Autocomplete.KontragentWithTypeAutocomplete = '/Autocomplete/KontragentWithTypeAutocomplete';
KontragentsApp.Autocomplete.MiddlemanAutocomplete = '/Autocomplete/MiddlemanAutocomplete';

mergerPrefix(KontragentsApp);

window.OfficeApp = {
    root: '/Office'
};

OfficeApp.MoneyBalanceMaster = {};
OfficeApp.MoneyBalanceMaster.GetEgfReport = '/Rest/OrderingEgrulEgrip/GetEgrReport';
mergerPrefix(OfficeApp);

function mergerPrefix(module) {
    for (var controller in module) {
        if (typeof module[controller] === 'object') {
            for (var action in module[controller]) {
                if (module[controller].hasOwnProperty(action)) {
                    module[controller][action] = getUrlWithId(module.root + module[controller][action]);
                }
            }
        }
    }
}
