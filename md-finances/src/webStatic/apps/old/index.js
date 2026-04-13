/* eslint-disable */

import './oldApps/Core/resources/AjaxErrors';
import './oldApps/Core/resources/UrlResources';
import './oldApps/Core/libs/jquery/jquery.validate';
import './oldApps/Core/libs/jquery/jquery.watermark';
import './oldApps/Core/libs/jquery/jquery.qtip';
import './oldApps/Core/libs/jquery/jquery.json-2.3';
import './oldApps/Core/libs/jquery/json';
import './oldApps/Core/libs/jquery/jquery.activity-indicator-1.0.0';
import './oldApps/Core/libs/jquery/jquery.tmpl';
import './oldApps/Core/libs/jquery/jquery.customselect';
import './oldApps/Core/libs/jquery/jquery.mdsticky-scroll';
import './oldApps/Core/libs/jquery/jquery.mdDatepicker';
import './oldApps/Core/libs/jquery/jquery.download';
import './oldApps/Core/libs/jquery/jquery.validate.unobtrusive.min';
import './oldApps/Core/libs/jquery/jquery.validate.unobtrusive.parseDynamicContent';
import './oldApps/Core/libs/jquery/jquery.placeholder';
import './oldApps/Core/libs/jquery/jquery.number_format';
import './oldApps/Core/libs/jquery/jquery.monthpicker';
import './oldApps/Core/libs/jquery/jquery.form';
import './oldApps/Core/libs/jquery/jquery.fileDownload';
import './oldApps/Core/libs/jquery/md.validate.unobtrusive';
import './oldApps/Core/libs/jquery/md/jquery-md-search';
import './oldApps/Core/libs/jquery/md/jquery-md-select';
import './oldApps/Core/libs/jquery/md/jquery.decimalmask';
import './oldApps/Core/libs/jquery/md/jquery.mdNumberInputMask';
import './oldApps/Core/libs/jquery/md/jquery-md-fieldValueConverter';
import './oldApps/Core/libs/jquery/md/jquery.mdTabControl';
import './oldApps/Core/libs/jquery/md/jquery-md-resizableTextarea';
import './oldApps/Core/libs/jquery/md/jquery.spanTransformer';
import './oldApps/Core/libs/jquery/md/jquery.mdSelectUls';
import './oldApps/Core/libs/jquery/md/jquery.mdButton-group';
import './oldApps/Core/libs/backbone/BackbonePaginator';
import './oldApps/Core/libs/backbone/extensions/collection';
import './oldApps/Core/libs/backbone/backbone.stickit';
import './oldApps/Core/libs/template/transparency.min';
import './oldApps/Core/libs/localStorage/amplify.store.min';
import './oldApps/Core/libs/localStorage/LocalStorageDispatcher';
import './oldApps/Core/libs/utils/Guid';
import './oldApps/Core/libs/utils/autocomplete';
import './oldApps/Core/libs/utils/ValueCrusher';
import './oldApps/Core/libs/utils/ToolsTips';
import './oldApps/Core/libs/utils/MoneyTools';
import './oldApps/Core/libs/utils/Converter';
import './oldApps/Core/libs/utils/MathOperations';
import './oldApps/Core/libs/utils/TemplateManager';
import './oldApps/Core/libs/utils/HistoryManager';
import './oldApps/Core/libs/utils/AjaxGlobalConfig';
import './oldApps/Core/libs/backbone/mdModelBinder';
import './oldApps/Core/libs/backbone/mdCollectionBinder';
import './oldApps/Core/libs/utils/string.format';
import './oldApps/Core/libs/jquery/jquery.string-truncate';
import './oldApps/Core/applicationCore/scripts/index';
import './oldApps/Core/applicationCore/scripts/engines/templateEngine';

import './oldApps/Core/scripts/settings/backboneValidation/closedPeriod';
import './oldApps/Core/scripts/settings/backboneValidation/format';
import './oldApps/Core/scripts/settings/backboneValidation/numbers';
import './oldApps/Core/libs/backbone/extensions/backbone-validationSettings';
import './oldApps/Core/libs/backbone/extensions/backbone.view.bind';
import './oldApps/Core/libs/backbone/extensions/backbone.model.ajaxOptions';
import './oldApps/Core/components/documentButton/scripts/index';
import './oldApps/Core/components/documentContextMenu/scripts/index';
import './oldApps/Core/components/linkedDocuments/scripts/collections/linkedDocumentsCollection';
import './oldApps/Core/components/linkedDocuments/scripts/paymentAutomationBehavior';
import './oldApps/Core/components/linkedDocuments/scripts/views/linkedDocumentRow';
import './oldApps/Core/components/linkedDocuments/scripts/views/linkedDocumentsTable';
import './oldApps/Core/scripts/controls/mdDropDown';
import './oldApps/Core/scripts/data/workerDocumentType';
import './oldApps/Core/scripts/settings/backbone.validations.callbacks';
import './oldApps/Core/scripts/settings/bindingHandlers';
import './oldApps/Core/libs/jquery/md/jquery.collapsible';
import './oldApps/Core/components/pageLoader/scripts/index';

import './oldApps/Common/CommonEnums';
import './oldApps/Common/CommonUrl';
import './oldApps/Common/data/Applications';
import './oldApps/Common/data/Enums';
import './oldApps/Common/data/KeyCodeEnum';
import './oldApps/Common/data/WebAppUrl';
import './oldApps/Common/data/resources/ApplicationUrlsResources';
import './oldApps/Common/data/helpers/Mixer';
import './oldApps/Common/data/ProvidePostingType';
import './oldApps/Common/data/TariffModes';
import './oldApps/Common/data/TaxPostingStatus';
import './oldApps/Common/data/NdsTypes';
import './oldApps/Common/data/InvoiceType';
import './oldApps/Common/data/DocumentStatuses';
import './oldApps/Common/data/TaxationSystemType';
import './oldApps/Common/data/UsnTypes';
import './oldApps/Common/data/DocumentTypes';
import './oldApps/Common/data/StockProductTypeEnum';
import './oldApps/Common/data/StockTypeEnum';
import './oldApps/Common/data/PrimaryDocumentsMoneyDirection';
import './oldApps/Common/data/TaxPostingsDirection';
import './oldApps/Common/data/NdsOperationTypes';
import './oldApps/Common/data/SubcontoType';
import './oldApps/Common/data/ExtendedErrorStatuses';
import './oldApps/Common/data/OsnoTransferTypeLabels';
import './oldApps/Common/data/OsnoNormalizedCostTypeLabels';
import './oldApps/Common/data/OsnoTransferKindLabels';
import './oldApps/Common/data/OsnoTransferType';
import './oldApps/Common/data/OsnoTransferKind';
import './oldApps/Common/utils/pageMaxZIndex';
import './oldApps/Common/data/helpers/DocumentTypeHelper';
import './oldApps/Common/data/helpers/TaxationSystemTypeHelper';
import './oldApps/Common/utils/mdFilter';
import './oldApps/Common/utils/Converter';
import './oldApps/Common/utils/CommonDataLoader';
import './oldApps/Common/utils/DirectiveHelper';
import './oldApps/Common/utils/HtmlCreator';
import './oldApps/Common/utils/AjaxTools';
import './oldApps/Common/utils/EventHelper';
import './oldApps/Common/utils/autocompletes/projectAutocomplete';
import './oldApps/Common/utils/autocompletes/kbkAutocomplete';
import './oldApps/Common/utils/autocompletes/reasonDocumentAutocomplete';
import './oldApps/Common/models/paginator/BasePaginator';
import './oldApps/Common/filters/BaseFilterModel';
import './oldApps/Common/filters/DocumentsDateFilterModel';
import './oldApps/Common/views/documents/documentStateMixin';
import './oldApps/Common/utils/commonAutocompleteSettings';
import './oldApps/Common/collections/AccountingPolicyCollection';
import './oldApps/Common/models/FirmRequisites';
import './oldApps/Common/models/Dialogs/Kontragent';
import './oldApps/Common/models/Dialogs/Settlement';
import './oldApps/Common/views/BaseView';
import './oldApps/Common/views/BaseTableView';
import './oldApps/Common/views/BaseCommonView';
import './oldApps/Common/views/Dialogs/ConfirmDialogView';
import './oldApps/Common/views/Dialogs/KontragentDialogView';
import './oldApps/Common/mixins/OnChangeFieldMixin';
import './oldApps/Common/mixins/ListControlMixin';
import './oldApps/Common/controls/BaseControl';
import './oldApps/Common/controls/DocumentHeaderControl';
import './oldApps/Common/controls/taxationSystemSwitcherControl';
import './oldApps/Common/controls/SwitchesChainControl';
import './oldApps/Common/views/documents/statusChangerControl';
import './oldApps/Common/utils/managers/TableManager';
import './oldApps/Common/utils/WebPageHelper';
import './oldApps/Common/utils/HintHelper';
import './oldApps/Common/utils/attachmentLoader';
import './oldApps/Common/utils/mdAutocomplete';
import './oldApps/Common/mixins/directivesMixin';
import './oldApps/Common/mixins/documentViewMixin';
import './oldApps/Common/mixins/validation/modelValidationMixin';
import './oldApps/Common/mixins/validation/functionForValidationMixin';
import './oldApps/Common/mixins/documentPageTitleMixin';
import './oldApps/Common/mixins/mdCustomControlMixin';
import './oldApps/Common/mixins/DeletingTableElementsMixin';
import './oldApps/Common/mixins/documentDownloadMixin';
import './oldApps/Common/mixins/views/documentFieldsReadonlyMixin';
import './oldApps/Common/utils/LogoBalance';
import './oldApps/Common/utils/PostingsAndTax';
import './oldApps/Common/utils/OsnoTypesForSelectGetter';
import './oldApps/Common/mixins/postingsAndTax/TaxCollectionSupportMixin';
import './oldApps/Common/mixins/postingsAndTax/FunctionForPostingsAndTaxValidationMixin';
import './oldApps/Common/mixins/postingsAndTax/PostingAndTaxListenMixin';
import './oldApps/Common/mixins/postingsAndTax/PostingsAndTaxTools';
import './oldApps/Common/models/postingsAndTax/SubcontoModel';
import './oldApps/Common/models/postingsAndTax/PostingModel';
import './oldApps/Common/models/postingsAndTax/TaxModel';
import './oldApps/Common/models/postingsAndTax/OsnoTaxModel';
import './oldApps/Common/models/postingsAndTax/PostingsAndTaxOperationModel';
import './oldApps/Common/collections/postingsAndTaxControl/PostingsCollection';
import './oldApps/Common/collections/postingsAndTaxControl/TaxCollection';
import './oldApps/Common/collections/postingsAndTaxControl/OsnoTaxCollection';
import './oldApps/Common/collections/postingsAndTaxControl/PostingsAndTaxOperationsCollection';
import './oldApps/Common/collections/SyntheticAccountAutocompleteCollection';
import './oldApps/Common/controls/postingsAndTax/subconto/ImplementedSubconto';
import './oldApps/Common/controls/postingsAndTax/subconto/SubcontoAccountBaseControl';
import './oldApps/Common/controls/postingsAndTax/subconto/AccountOnDebitControl';
import './oldApps/Common/controls/postingsAndTax/subconto/AccountOnCreditControl';
import './oldApps/Common/controls/postingsAndTax/content/BaseRowOperationControl';
import './oldApps/Common/controls/postingsAndTax/content/TaxRowOperationControl';
import './oldApps/Common/controls/postingsAndTax/content/OsnoTaxRowOperationControl';
import './oldApps/Common/controls/postingsAndTax/content/PostingsRowOperationControl';
import './oldApps/Common/controls/postingsAndTax/PostingsAndTaxBaseControl';
import './oldApps/Common/controls/postingsAndTax/PostingsAndTaxControl';
import './oldApps/Common/controls/postingsAndTax/PostingsControl';
import './oldApps/Common/controls/postingsAndTax/TaxControl';
import './oldApps/Common/controls/TaxationSystemControl';
import './oldApps/Common/data/ApplicationUrls';
import './oldApps/Common/data/BudgetaryPaymentBase';
import './oldApps/Common/data/AccountingDocumentType';
import './oldApps/Common/data/WaybillType';
import './oldApps/Common/utils/autocompletes/WorkerAutocomplete';
import './oldApps/Common/utils/autocompletes/kontragentAutocomplete';
import './oldApps/Common/utils/autocompletes/kontragentAndWorkerAutocomplete';
import './oldApps/Common/utils/autocompletes/cashOrderAutocomplete';
import './oldApps/Common/utils/autocompletes/billsWithKontragentInfoAutocomplete';
import './oldApps/Common/utils/UserFirmAccessRule';
import './oldApps/Common/utils/Account';
import './oldApps/Common/services/urlGetter';
import './oldApps/Common/behaviours/AutoDocNumberBehaviour';
import './oldApps/Common/data/MiddlemanContractType';
import './oldApps/Common/utils/autocompletes/AdvanceStatementAutocompletes';
import './oldApps/Common/data/AccessType';

import './oldApps/SalesApp/SalesModule';
import './oldApps/SalesApp/utils/SaleAutocompletes';
import './oldApps/SalesApp/utils/SaleKontragentWaybillAutocomplete';
import './oldApps/SalesApp/utils/saleBillAutocomplete';
import './oldApps/SalesProApp/models/HarvestModel';
import './oldApps/SalesApp/models/RelatedDocsToolTipModel';
import './oldApps/SalesApp/views/main/table/operations/RelatedDocsToolTip';
import './oldApps/SalesApp/collections/SalesBaseCollection';
import './oldApps/SalesApp/models/CommentModel';
import './oldApps/SalesApp/models/StatusChanger';
import './oldApps/SalesApp/models/Email';
import './oldApps/SalesApp/models/StatusModel';
import './oldApps/SalesApp/views/main/table/operations/Commenting';
import './oldApps/SalesApp/views/main/table/operations/StatusChanger';

import './oldApps/MoneyTransferApp/models/common/AccessToService';
import './oldApps/MoneyTransferApp/models/common/AdvanceStatement';
import './oldApps/MoneyTransferApp/models/common/ConfirmingStatement';
import './oldApps/MoneyTransferApp/models/common/InventoryNumber';
import './oldApps/MoneyTransferApp/models/common/OrderNumber';
import './oldApps/MoneyTransferApp/models/common/Waybill';
import './oldApps/MoneyTransferApp/models/common/Bill';
import './oldApps/MoneyTransferApp/models/common/Bank';
import './oldApps/MoneyTransferApp/models/common/Founder';
import './oldApps/MoneyTransferApp/models/common/Kontragent';
import './oldApps/MoneyTransferApp/models/common/Project';
import './oldApps/MoneyTransferApp/models/common/TaxationSystem';
import './oldApps/MoneyTransferApp/models/common/CashContent';
import './oldApps/MoneyTransferApp/models/common/RelatedOutgoingOperation';
import './oldApps/MoneyTransferApp/models/common/IntegrationsAndDischarges';
import './oldApps/MoneyTransferApp/models/common/ExtendedFiltration';
import './oldApps/MoneyTransferApp/models/common/MainModel';
import './oldApps/MoneyTransferApp/models/common/BillSumForIncoming';
import './oldApps/MoneyTransferApp/models/common/BillSettlement';
import './oldApps/MoneyTransferApp/collections/common/SettlementCollection';
import './oldApps/MoneyTransferApp/collections/common/TaxationSystemCollection';
import './oldApps/MoneyTransferApp/views/ApplicationBaseView';
import './oldApps/MoneyTransferApp/views/dialogs/BaseView';

import './oldApps/StockApp/stockModule';
import './oldApps/StockApp/main/stockUrl';
import './oldApps/StockApp/models/filters/StockFilterModel';
import './oldApps/StockApp/helpers/templateHelper';
import './oldApps/StockApp/helpers/baseValidationHelper';
import './oldApps/StockApp/helpers/stockMessageHelper';
import './oldApps/StockApp/helpers/stockMenuEvents/stockContextMenu';
import './oldApps/StockApp/helpers/stockMenuEvents/stockMenuEvents';
import './oldApps/StockApp/helpers/stockMenuTemplateRowHelper';
import './oldApps/StockApp/models/main/baseModel';
import './oldApps/StockApp/collections/main/baseCollection';
import './oldApps/StockApp/models/main/tableModel';
import './oldApps/StockApp/models/stockMenu/stockRowModel';
import './oldApps/StockApp/collections/stockMenu/stockCollection';
import './oldApps/StockApp/models/stockMenu/nomenclatureRowModel';
import './oldApps/StockApp/collections/stockMenu/nomenclatureCollection';
import './oldApps/StockApp/models/stockMenu/stockModel';
import './oldApps/StockApp/models/stockMenu/nomenclatureModel';
import './oldApps/StockApp/helpers/template/transparencyDirectivesHelper';
import './oldApps/StockApp/helpers/paginator/paginatorModel';
import './oldApps/StockApp/helpers/paginator/paginatorView';

import './oldApps/ClosingPeriodValidation/ClosingPeriodValidationModule';
import './oldApps/ClosingPeriodValidation/models/OpenClosedPeriodDialog';
import './oldApps/ClosingPeriodValidation/views/OpenClosedPeriodView';
import './oldApps/ClosingPeriodValidation/utils/ClosedPeriodValidation';
import './oldApps/ClosingPeriodValidation/utils/Decorator';

import './oldApps/AccountingPostingApp/accountingPostingModule';
import './oldApps/AccountingPostingApp/utils/TooltipHelper';
import './oldApps/AccountingPostingApp/collections/AccountingDocumentPostingCollection';
import './oldApps/AccountingPostingApp/views/TooltipPostingsView';
import './oldApps/AccountingPostingApp/utils/TooltipTaxPostingHelper';
import './oldApps/AccountingPostingApp/collections/TaxPostingCollection';
import './oldApps/AccountingPostingApp/views/TooltipTaxPostingsView';


import './oldApps/PrimaryDocumentsLib/PrimaryDocumentsModule';
import './oldApps/PrimaryDocumentsLib/utils/table/TaxPostingTooltipMixin';
import './oldApps/PrimaryDocumentsLib/enums/SalesWaybillTypes';
import './oldApps/PrimaryDocumentsLib/views/BaseApplicationView';
import './oldApps/PrimaryDocumentsLib/views/documents/Actions/DeleteDialogView';
import './oldApps/PrimaryDocumentsLib/controls/MiddlemanContract/MiddlemanContractUtils';
import './oldApps/PrimaryDocumentsLib/controls/MiddlemanContract/MiddlemanContractModel';
import './oldApps/PrimaryDocumentsLib/controls/MiddlemanContract/MiddlemanContractControl';
import './oldApps/PrimaryDocumentsLib/controls/MiddlemanContract/middlemanAutocompletes';
import './oldApps/PrimaryDocumentsLib/PrimaryDocumentsUrl';
import './oldApps/PrimaryDocumentsLib/enums/BuyWaybillTypes';
import './oldApps/PrimaryDocumentsLib/models/ReasonModel';
import './oldApps/PrimaryDocumentsLib/views/documents/BaseDocumentView';
import './oldApps/PrimaryDocumentsLib/controls/SumControl';
import './oldApps/PrimaryDocumentsLib/controls/NdsRateControl';
import './oldApps/PrimaryDocumentsLib/blocks/mainMenu/MenuView';
import './oldApps/PrimaryDocumentsLib/blocks/mainMenu/MenuModel';
import './oldApps/PrimaryDocumentsLib/blocks/fakeDocsDialog/fakeDialogCollection';
import './oldApps/PrimaryDocumentsLib/blocks/fakeDocsDialog/fakeDialogModel';
import './oldApps/PrimaryDocumentsLib/blocks/fakeDocsDialog/fakeDialogRowView';
import './oldApps/PrimaryDocumentsLib/blocks/fakeDocsDialog/fakeDialogView';
import './oldApps/PrimaryDocumentsLib/models/AccountsLoader';
import './oldApps/PrimaryDocumentsLib/utils/TaxationSystemHelper';
import './oldApps/PrimaryDocumentsLib/utils/StockProductDialogHelper';
import './oldApps/PrimaryDocumentsLib/utils/paymentAutocomplete';

import './oldApps/ClosingPeriodWizardApp/data/WebAppUrl';

import './oldApps/TaxPosting/TaxPostingModules';
import './oldApps/TaxPosting/views/TaxPostingTableView';

import './oldApps/BuyApp/BuyModule';
import './oldApps/BuyApp/models/documents/BaseDocument';
import './oldApps/BuyApp/views/BaseApplicationView';
import './oldApps/BuyApp/views/documents/BaseDocumentView';
import './oldApps/BuyApp/collections/postingsAndTax/AccountingStatementPostingsCollection';
import './oldApps/BuyApp/collections/postingsAndTax/AccountingStatementTaxCollection';
import './oldApps/BuyApp/views/documents/AccountingStatementsView';
import './oldApps/BuyApp/data/WebAppUrl';

import './oldApps/AccountingStatementsApp/accountingStatementsModule';
import './oldApps/AccountingStatementsApp/ImplementedSubconto';
import './oldApps/AccountingStatementsApp/collections/SubcontoInfoCollection';
import './oldApps/AccountingStatementsApp/models/SyntheticAccountAutocompleteModel';
import './oldApps/AccountingStatementsApp/collections/SyntheticAccountAutocompleteCollection';

import './oldApps/CashApp/CashModule';
import './oldApps/CashApp/main/CashUrl';
import './oldApps/CashApp/main/CashEnums';
import './oldApps/CashApp/utils/PrimaryDocumentsFilter';
import './oldApps/CashApp/models/filters/CashFilterModel';
import './oldApps/CashApp/models/BaseApplicationModel';
import './oldApps/CashApp/models/tables/DeletingTableModel';
import './oldApps/CashApp/models/tables/BaseTableModel';
import './oldApps/CashApp/models/tables/TotalInformationModel';
import './oldApps/CashApp/models/CashMenuModel';
import './oldApps/CashApp/models/CashMainModel';
import './oldApps/CashApp/models/DownloadReportModel';
import './oldApps/CashApp/collections/postingsAndTax/CashPostingsCollection';
import './oldApps/CashApp/collections/postingsAndTax/CashTaxCollection';
import './oldApps/CashApp/collections/CashCollection';
import './oldApps/CashApp/views/main/baseView';
import './oldApps/CashApp/scripts/components/WorkerPaymentList/collections/workerPaymentsCollection';
import './oldApps/CashApp/scripts/components/WorkerPaymentList';
import './oldApps/CashApp/scripts/components/WorkerPaymentList/views/workerPayrollPaymentListControl';
import './oldApps/CashApp/scripts/components/WorkerPaymentList/views/workerPayrollPaymentRow';
import './oldApps/CashApp/scripts/data/workerDocumentType';
import './oldApps/CashApp/scripts/models/documents/baseCashOrder';
import './oldApps/CashApp/scripts/models/documents/incomingCashOrder';
import './oldApps/CashApp/scripts/models/documents/outgoingCashOrder';
import './oldApps/CashApp/scripts/utils/mapper';
import './oldApps/CashApp/scripts/views/documents/baseCashOrderView';
import './oldApps/CashApp/scripts/views/documents/incomingCashOrderView';
import './oldApps/CashApp/scripts/views/documents/incomingOperationList';
import './oldApps/CashApp/scripts/views/documents/mixins/initBillAutocomplete';
import './oldApps/CashApp/scripts/views/documents/mixins/initWorkerAutocomplete';
import './oldApps/CashApp/scripts/views/documents/mixins/ndsUsnMessage';
import './oldApps/CashApp/scripts/views/documents/mixins/setMaxSumTooltip';
import './oldApps/CashApp/scripts/views/documents/operations/accountablePerson';
import './oldApps/CashApp/scripts/views/documents/operations/fromOtherCash';
import './oldApps/CashApp/scripts/views/documents/operations/incomingCashContributing';
import './oldApps/CashApp/scripts/views/documents/operations/incomingLoanObtaining';
import './oldApps/CashApp/scripts/views/documents/operations/incomingMaterialAid';
import './oldApps/CashApp/scripts/views/documents/operations/kontragentPayment';
import './oldApps/CashApp/scripts/views/documents/operations/mediationFee';
import './oldApps/CashApp/scripts/views/documents/operations/moneyCollection';
import './oldApps/CashApp/scripts/views/documents/operations/other';
import './oldApps/CashApp/scripts/views/documents/operations/outgoingLoanRepayment';
import './oldApps/CashApp/scripts/views/documents/operations/outgoingPaymentUnderAgency';
import './oldApps/CashApp/scripts/views/documents/operations/outgoingReturnToBuyer';
import './oldApps/CashApp/scripts/views/documents/operations/retailRevenue';
import './oldApps/CashApp/scripts/views/documents/operations/MiddlemanRetailRevenue';
import './oldApps/CashApp/scripts/views/documents/operations/CashBudgetaryPayment';
import './oldApps/CashApp/scripts/views/documents/operations/salary';
import './oldApps/CashApp/scripts/views/documents/operations/settlementAccountPayment';
import './oldApps/CashApp/scripts/views/documents/operations/CashOrderOutgoingProfitWithdrawing';
import './oldApps/CashApp/scripts/views/documents/operations/CashOrderIncomingContributionOfOwnFunds';
import './oldApps/CashApp/scripts/views/documents/operations/toOtherCash';
import './oldApps/CashApp/scripts/views/documents/outgoingCashOrderView';
import './oldApps/CashApp/scripts/views/documents/outgoingOperationList';

import './oldApps/BankApp/main/BankModule';
import './oldApps/BankApp/scripts/services/KbkAutoFieldsService';
import './oldApps/BankApp/scripts/services/purseGetter';
import './oldApps/BankApp/scripts/services/purseOperationGetter';
import './oldApps/BankApp/scripts/services/purseOperationService';
import './oldApps/BankApp/scripts/services/settlementGetter';
import './oldApps/BankApp/scripts/services/taxSystemGetter';
import './oldApps/BankApp/scripts/services/TradingObjectService';
import './oldApps/BankApp/BankApp';
import './oldApps/BankApp/main/BankEnums';
import './oldApps/BankApp/main/BankUrl';
import './oldApps/BankApp/helpers/TemplateHelper';
import './oldApps/BankApp/helpers/TransparencyDirectivesHelper';
import './oldApps/BankApp/helpers/OperationTypesFactory';
import './oldApps/BankApp/helpers/OperationAccessHelper';
import './oldApps/BankApp/utils/BankAutocompletes';
import './oldApps/BankApp/utils/BankInlineFilter';
import './oldApps/BankApp/utils/bankKontragentAutocomplete';
import './oldApps/BankApp/utils/BankUtils';
import './oldApps/BankApp/utils/CommonDataLoader';
import './oldApps/BankApp/collections/postingsAndTax/PPostingCollection';
import './oldApps/BankApp/collections/postingsAndTax/PpTaxCollection';
import './oldApps/BankApp/collections/postingsAndTax/PursePostingCollection';
import './oldApps/BankApp/collections/postingsAndTax/PurseTaxCollection';
import './oldApps/BankApp/collections/AccountsCollection';
import './oldApps/BankApp/collections/BaseDocumentFillingCollection';
import './oldApps/BankApp/models/tools/TripleCalendar';
import './oldApps/BankApp/models/documents/BudgetaryPaymentKbkAutoFields';
import './oldApps/BankApp/models/documents/DocumentNumber';
import './oldApps/BankApp/models/importWizard/OperationTypeCollection';
import './oldApps/BankApp/views/main/BaseView';
import './oldApps/BankApp/views/tools/TripleCalendar';
import './oldApps/BankApp/views/documents/BaseDocumentView';
import './oldApps/BankApp/views/controls/BaseControl';
import './oldApps/BankApp/views/controls/budgetaryPayment/SelectControl';
import './oldApps/BankApp/scripts/utils/bindingUtils';
import './oldApps/BankApp/scripts/models/documents/incomingPurseDocument';
import './oldApps/BankApp/scripts/models/documents/outgoingPurseDocument';
import './oldApps/BankApp/scripts/views/behaviors/postingControlsBehavior';
import './oldApps/BankApp/scripts/views/behaviors/taxationSystemChoiceBehavior';
import './oldApps/BankApp/scripts/views/documents/purse/incomingPurseDocument';
import './oldApps/BankApp/scripts/views/documents/purse/operations/commissionHold';
import './oldApps/BankApp/scripts/views/documents/purse/operations/movementToSettlement';
import './oldApps/BankApp/scripts/views/documents/purse/operations/otherOutgoing';
import './oldApps/BankApp/scripts/views/documents/purse/outgoingPurseDocument';
import './oldApps/BankApp/scripts/views/documents/purse/purseDocumentHelper';
import './oldApps/BankApp/scripts/components/billAutocomplete';
import './oldApps/BankApp/scripts/components/closedPeriodIcon';
import './oldApps/BankApp/scripts/components/documentActions';
import './oldApps/BankApp/scripts/components/payersControl/layoutView';
import './oldApps/BankApp/scripts/components/ndsControl';
import './oldApps/BankApp/scripts/components/purseKontragent';
import './oldApps/BankApp/scripts/components/sumControl';
import './oldApps/BankApp/scripts/components/textControl';
import './oldApps/BankApp/scripts/handlers/purseOperationTypes';
import './oldApps/BankApp/scripts/services/BudgetaryPaymentService';

// ######################################### CSS
import './oldApps/Core/styles/main.less';
import './oldApps/Core/styles/table.css';
import './oldApps/Core/styles/js-link.css';
import './oldApps/Common/public/stylesheets/Base.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/newdesign.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/Balance.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/Buttons.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/GlobalStyles.less';
import './oldApps/Common/public/stylesheets/oldBizStyles/icons.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/MasterPage.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/Masters.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/Techsupport.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/WarningAndErrors.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/Table.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/Filter.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/Dialogs.css';
import './oldApps/Common/public/stylesheets/oldBizStyles/jquery.qtip.css';
import './oldApps/Common/public/stylesheets/mdTable.css';
import './oldApps/Common/public/stylesheets/mdTableCommonStyles.css';
import './oldApps/Common/public/stylesheets/jquery.qtip.css';
import './oldApps/Common/public/stylesheets/mdCommon.css';
import './oldApps/Common/public/stylesheets/hints.css';
import './oldApps/Common/public/stylesheets/variables.css';
import './oldApps/Common/public/stylesheets/mixins.css';
import './oldApps/Common/public/stylesheets/status-info.css';
import './oldApps/Common/public/stylesheets/mdMainPage.css';
import './oldApps/Common/public/stylesheets/mdCustomSelect.css';
import './oldApps/Common/public/stylesheets/mdAutocomplete.css';
import './oldApps/Common/public/stylesheets/mdDocumentForm.css';
import './oldApps/Common/public/stylesheets/certainAutocomplete.css';
import './oldApps/Common/public/stylesheets/mdBreadcrumb.css';
import './oldApps/Common/public/stylesheets/mdSeparatedTimeFilter.css';
import './oldApps/Common/public/stylesheets/confirmDialog.css';
import './oldApps/Common/public/stylesheets/tabDialog.css';
import './oldApps/Common/public/stylesheets/mdDialog.css';
import './oldApps/Components/productDialog/styles/less/productDialog.css';
import './oldApps/Common/public/stylesheets/mdPostingsAndTaxControl.css';
import './oldApps/Common/public/stylesheets/mdCustomUlSelect.css';
import './oldApps/Common/public/stylesheets/paginator.css';
import './oldApps/Common/public/stylesheets/mdLMenu.css';
import './oldApps/Common/public/stylesheets/mdTree.css';
import './oldApps/Common/public/stylesheets/mdContextMenu.css';
import './oldApps/Common/public/stylesheets/settlementDialog.css';
import './oldApps/Common/public/stylesheets/mdTab.css';
import './oldApps/Common/public/stylesheets/integrations.css';
import './oldApps/Common/public/stylesheets/mdWizard.css';
import './oldApps/Common/public/stylesheets/mdSpanTransformer.css';
import './oldApps/AccountingPostingApp/public/stylesheets/accountingPosting.css';
import './oldApps/AccountingPostingApp/public/stylesheets/postingTable.css';
import './oldApps/AccountingPostingApp/public/stylesheets/PostingTooltip.css';
import './oldApps/Common/public/stylesheets/filter/mdFilter.css';
import './oldApps/Common/public/stylesheets/filter/extendedFilterDialog.css';
import './oldApps/MoneyTransferApp/public/stylesheets/NewMoney.css';
import './oldApps/MoneyTransferApp/public/stylesheets/autocompleteExtension.css';
import './oldApps/MoneyTransferApp/public/stylesheets/FirmMoneyTransfer.css';
import './oldApps/MoneyTransferApp/public/stylesheets/MoneyTransferDialog.css';
import './oldApps/ClosingPeriodValidation/public/stylesheets/OpenClosedPeriod.css';
import './oldApps/BankApp/styles/less/BankApp.css';
import './oldApps/BankApp/styles/less/ImportWizard.css';
import './oldApps/BankApp/styles/less/bank.css';
import './oldApps/Core/components/linkedDocuments/styles/less/linkedDocumentTable.css';
import './oldApps/BankApp/components/kontragentSettlements/styles.css';
import './oldApps/StockApp/public/stylesheets/less/stockDialog.css';
import './oldApps/StockApp/public/stylesheets/less/integrableStockStyle.css';
import './oldApps/CashApp/styles/less/CashApp.css';

// ################################# NEW

import '@moedelo/md-frontendcore/mdUi';
import '@moedelo/md-frontendcore/mdUi/controls/md-icon';

import MenuItem from './components/MenuItem';
import DocumentAutoNumerationService from './services/DocumentAutoNumerationService';
import BankIntegrationButton from './components/BankIntegrationButton';
import PaymentOrderService from './../../services/Bank/PaymentOrderService';
import KontragentService from './services/KontragentService';
import TruncateStringHelper from '@moedelo/md-frontendcore/mdCommon/helpers/TruncateStringHelper';
import addDialogHelper from '@moedelo/frontend-common/helpers/addDialogHelper';
import Direction from '@moedelo/frontend-enums/mdEnums/Direction';
import Telemetry from '../../services/Telemetry';
import MdTab from '@moedelo/md-frontendcore/mdCommon/components/MdTab';
import MdTooltip from '@moedelo/md-frontendcore/mdCommon/components/MdTooltip';
import MdLoader from '@moedelo/md-frontendcore/mdCommon/components/MdLoader';
import IntegrationTurnOtpDialog from '@moedelo/frontend-common/components/IntegrationTurnOtpDialog';
import ContractKind from '@moedelo/frontend-enums/mdEnums/ContractKind';
import IntegrationStatusCode from '@moedelo/frontend-enums/mdEnums/IntegrationStatusCode';
import DialogHelper from '@moedelo/md-frontendcore/mdCommon/helpers/DialogHelper';
import WorkerCharges from './components/WorkerCharges';
import ndsTypeConverter from '../../helpers/ndsTypeConverter';
import NdsTypesEnum from '../../enums/newMoney/NdsTypesEnum';
import mrkStatService from '@moedelo/frontend-common-v2/apps/marketing/services/mrkStatService';
import TaxationSystemFieldHelper from './helpers/TaxationSystemFieldHelper';
import MoneyOperationHelper from '../../helpers/MoneyOperationHelper';

import bankTemplates from './bankTemplates.hbs';
import cashTemplates from './cashTemplates.hbs';

window.mdNew = window.mdNew || {};

Object.assign(window.mdNew, {
    ContractKind,
    MenuItem,
    MoneyOperationHelper,
    DocumentAutoNumerationService,
    PaymentOrderService,
    BankIntegrationButton,
    TruncateStringHelper,
    KontragentService,
    Direction,
    Telemetry,
    MdTab,
    MdTooltip,
    MdLoader,
    DialogHelper,
    IntegrationTurnOtpDialog,
    WorkerCharges,
    ndsTypeConverter,
    mrkStatService,
    TaxationSystemFieldHelper,
    MoneyNdsType: NdsTypesEnum,

    Enums: {
        IntegrationStatusCode
    },

    Contracts: {
        addDialogHelper
    }
});

$('body').append(bankTemplates).append(cashTemplates);
window.Common.Utils.CommonDataLoader.loadFirmRequisites();
window.Common.Utils.CommonDataLoader.loadTaxationSystems();
window.Common.Utils.CommonDataLoader.loadNdsRatesFromAccPol();
