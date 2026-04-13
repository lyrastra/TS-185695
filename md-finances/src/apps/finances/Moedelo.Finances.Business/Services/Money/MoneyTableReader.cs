using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.CommonV2.Extensions.System;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Services.Money.Operations;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.BalanceMaster;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Interfaces.Business.Money.PaymentOrders;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money;
using Moedelo.Finances.Domain.Models.BalanceMaster;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.Money.Client.BankBalanceHistory;
using Moedelo.Money.Client.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Dto.BankBalanceHistory;
using Moedelo.PaymentImport.Client;
using Moedelo.RequisitesV2.Client.FirmTaxationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperations;
using Moedelo.BankIntegrations.Enums.Invoices;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Extensions.PostingEngine;
using Moedelo.CommonV2.UserContext.Domain.ExtraData;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.Money.Client.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment.SubPayment;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.PaymentImport.Dto;
using Moedelo.RequisitesV2.Client.FirmRequisites;

namespace Moedelo.Finances.Business.Services.Money
{
    [InjectAsSingleton]
    public class MoneyTableReader : IMoneyTableReader
    {
        private const string TAG = nameof(MoneyTableReader);
        private readonly IBalanceMasterService balanceMasterService;
        private readonly IMoneyBalancesReader balancesReader;
        private readonly IMoneyOperationLinkedDocumentsReader linkedDocumentsReader;
        private readonly IMoneyOperationSalaryChargePaymentsReader salaryChargePaymentsReader;
        private readonly IMoneyOperationTaxPostingsReader taxPostingsReader;
        private readonly IPaymentOrderOperationReader paymentOrderOperationReader;
        private readonly IMoneyTableDao moneyTableDao;
        private readonly IFirmTaxationSystemClient taxationSystemClient;
        private readonly IMoneyBankBalanceApiClient moneyBankBalanceApiClient;
        private readonly IPaymentImportRulesClient paymentImportRulesClient;
        private readonly IPaymentToNaturalPersonsClient paymentToNaturalPersonsClient;
        private readonly CanSendToBankReader canSendToBankReader;
        private readonly IBankOperationsApiClient bankOperationsApiClient;
        private readonly IFirmRequisitesClient firmRequisitesClient;
        private readonly IUnifiedBudgetarySubPaymentClient unifiedBudgetarySubPaymentClient;
        private readonly IOutsourceImportRulesClient outsourceImportRulesClient;
        private readonly ILogger logger;

        // Не могут иметь никаких связанных документов
        private static readonly ISet<OperationType> WithoutRelatedDocs = new HashSet<OperationType>
        {
            OperationType.PaymentOrderOutgoingBankFee,
            OperationType.PaymentOrderIncomingAccrualOfInterest,
            OperationType.PaymentOrderIncomingRetailRevenue,
            OperationType.PaymentOrderOutgoingProfitWithdrawing,
            OperationType.PaymentOrderIncomingMaterialAid,
            OperationType.CashOrderIncomingMaterialAid,
            OperationType.PaymentOrderIncomingContributionAuthorizedCapital,
            OperationType.CashOrderIncomingContributionAuthorizedCapital,
            OperationType.BudgetaryPayment,
            OperationType.CashOrderOutgoingCollectionOfMoney,
            OperationType.PurseOperationTransferToSettlement,
            OperationType.PurseOperationComission,
            OperationType.CashOrderOutgoingProfitWithdrawing,
            OperationType.CashOrderIncomingContributionOfOwnFunds,
            OperationType.PaymentOrderIncomingFromPurse,
            OperationType.CashOrderBudgetaryPayment,
            OperationType.PaymentOrderOutgoingPurchaseCurrency,
            OperationType.PaymentOrderIncomingPurchaseCurrency,
            OperationType.PaymentOrderOutgoingSaleCurrency,
            OperationType.PaymentOrderIncomingSaleCurrency,
            OperationType.CurrencyBankFee,
            OperationType.PaymentOrderOutgoingCurrencyTransferToAccount,
            OperationType.PaymentOrderIncomingCurrencyTransferFromAccount,
            OperationType.CashOrderOutgoingUnifiedBudgetaryPayment,
            OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment
        };

        // Документы, которые учитываются при покрытии суммы платежа
        private static readonly ISet<AccountingDocumentType> LinkedCoveringDocsTypes = new HashSet<AccountingDocumentType>
        {
            AccountingDocumentType.Upd,
            AccountingDocumentType.SalesUpd,
            AccountingDocumentType.Statement,
            AccountingDocumentType.Waybill,
            AccountingDocumentType.AccountingAdvanceStatement,
            AccountingDocumentType.MiddlemanReport,
            AccountingDocumentType.InventoryCard,
            AccountingDocumentType.ReceiptStatement,
            AccountingDocumentType.SalesCurrencyInvoice,
            AccountingDocumentType.PurchasesCurrencyInvoice,
            AccountingDocumentType.PaymentReserve
        };

        // Не могут иметь первичных документов
        private static readonly ISet<OperationType> WithoutPrimaryDocs = new HashSet<OperationType>
        {
            OperationType.PaymentOrderIncomingContributionOfOwnFunds,
            OperationType.PaymentOrderIncomingOther,
            OperationType.PaymentOrderIncomingRefundToSettlementAccount,
            OperationType.CashOrderIncomingMiddlemanRetailRevenue,
            OperationType.CashOrderIncomingFromRetailRevenue,
            OperationType.CashOrderOutgoingReturnToBuyer,
            OperationType.PaymentOrderOutgoingLoanRepayment,
            OperationType.CashOrderOutgoingLoanRepayment,
            OperationType.PaymentOrderOutgoingPaymentAgencyContract,
            OperationType.CashOrderOutgoingPaymentAgencyContract,
            OperationType.PaymentOrderOutgoingTransferToOtherAccount,
            OperationType.CashOrderOutgoingTranslatedToOtherCash,
            OperationType.CashOrderOutgoingOther,
            OperationType.CashOrderIncomingOther,
            OperationType.CashOrderIncomingLoanObtaining,
            OperationType.MemorialWarrantCreditingCollectedFunds,
            OperationType.PaymentOrderOutgoingOther,
            OperationType.PurseOperationOtherOutgoing,
            OperationType.PaymentOrderOutgoingReturnToBuyer,
            OperationType.PaymentOrderIncomingFromAnotherAccount,
            OperationType.CashOrderIncomingFromAnotherCash,
            OperationType.PaymentOrderIncomingLoanObtaining,
            OperationType.PaymentOrderIncomingLoanReturn,
            OperationType.CashOrderIncomingLoanObtaining,
            OperationType.PaymentOrderOutgoingtWithdrawalFromAccount,
            OperationType.CashOrderIncomingFromSettlementAccount,
            OperationType.PaymentOrderIncomingTransferFromCash,
            OperationType.CashOrderOutcomingToSettlementAccount,
            OperationType.PaymentOrderIncomingCurrencyOther,
            OperationType.PaymentOrderOutgoingCurrencyOther,
            OperationType.PaymentOrderOutgoingLoanIssue,
            OperationType.PaymentOrderIncomingIncomeFromCommissionAgent,
            OperationType.PaymentOrderOutgoingDeduction
        };

        // Cвязь с АО
        private static readonly ISet<OperationType> WithAdvanceDocs = new HashSet<OperationType>
        {
            OperationType.PaymentOrderIncomingReturnFromAccountablePerson,
            OperationType.PaymentOrderOutgoingIssuanceAccountablePerson,
            OperationType.CashOrderIncomingReturnFromAccountablePerson,
            OperationType.CashOrderOutgoingIssuanceAccountablePerson
        };

        private static readonly ISet<OperationType> UnifiedBudgetaryPayments = new HashSet<OperationType>
        {
            OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment,
            OperationType.CashOrderOutgoingUnifiedBudgetaryPayment
        };
        
        private static readonly ISet<OperationType> SendInvoiceOperationTypes = new HashSet<OperationType>
        {
            OperationType.PaymentOrderOutgoingReturnToBuyer,
            OperationType.PaymentOrderOutgoingIssuanceAccountablePerson,
            OperationType.PaymentOrderOutgoingOther,
            OperationType.PaymentOrderOutgoingPaymentSuppliersForGoods,
            OperationType.PaymentOrderOutgoingTransferToOtherAccount,
            OperationType.PaymentOrderOutgoingForTransferSalary,
            OperationType.BudgetaryPayment,
            OperationType.PaymentOrderOutgoingLoanRepayment,
            OperationType.PaymentOrderOutgoingPaymentAgencyContract,
            OperationType.PaymentOrderOutgoingProfitWithdrawing,
            OperationType.PaymentOrderOutgoingLoanIssue,
            OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment,
            OperationType.PaymentOrderOutgoingDeduction
        };

        private const int IpAsWorkerId = -1;

        public MoneyTableReader(
            IBalanceMasterService balanceMasterService,
            IMoneyBalancesReader balancesReader,
            IMoneyOperationLinkedDocumentsReader linkedDocumentsReader,
            IMoneyOperationSalaryChargePaymentsReader salaryChargePaymentsReader,
            IMoneyOperationTaxPostingsReader taxPostingsReader,
            IPaymentOrderOperationReader paymentOrderOperationReader,
            IMoneyTableDao moneyTableDao,
            IFirmTaxationSystemClient taxationSystemClient,
            IMoneyBankBalanceApiClient moneyBankBalanceApiClient,
            IPaymentImportRulesClient paymentImportRulesClient,
            IPaymentToNaturalPersonsClient paymentToNaturalPersonsClient,
            CanSendToBankReader canSendToBankReader,
            IBankOperationsApiClient bankOperationsApiClient,
            IFirmRequisitesClient firmRequisitesClient,
            IUnifiedBudgetarySubPaymentClient unifiedBudgetarySubPaymentClient,
            IOutsourceImportRulesClient outsourceImportRulesClient,
            ILogger logger)
        {
            this.balanceMasterService = balanceMasterService;
            this.balancesReader = balancesReader;
            this.linkedDocumentsReader = linkedDocumentsReader;
            this.salaryChargePaymentsReader = salaryChargePaymentsReader;
            this.taxPostingsReader = taxPostingsReader;
            this.paymentOrderOperationReader = paymentOrderOperationReader;
            this.moneyTableDao = moneyTableDao;
            this.taxationSystemClient = taxationSystemClient;
            this.moneyBankBalanceApiClient = moneyBankBalanceApiClient;
            this.paymentImportRulesClient = paymentImportRulesClient;
            this.paymentToNaturalPersonsClient = paymentToNaturalPersonsClient;
            this.canSendToBankReader = canSendToBankReader;
            this.bankOperationsApiClient = bankOperationsApiClient;
            this.firmRequisitesClient = firmRequisitesClient;
            this.unifiedBudgetarySubPaymentClient = unifiedBudgetarySubPaymentClient;
            this.outsourceImportRulesClient = outsourceImportRulesClient;
            this.logger = logger;
        }

        public async Task<UnrecognizedMoneyTableResponse> GetUnrecognizedAsync(IUserContext userContext,
            MoneyTableRequest request, CancellationToken cancellationToken)
        {
            var initialDate = await GetInitialDateAsync(userContext, cancellationToken).ConfigureAwait(false);
            var response = await moneyTableDao.GetUnrecognizedAsync(userContext.FirmId, initialDate, request, cancellationToken).ConfigureAwait(false);
            await LoadBaseOperationsAsync(userContext.FirmId, response.Operations).ConfigureAwait(false);
            await LoadImportRulesAsync(userContext, response.Operations).ConfigureAwait(false);
            await LoadOutsourceImportRulesAsync(userContext, response.Operations).ConfigureAwait(false);
            return response;
        }

        public async Task<int> GetUnrecognizedOperationsCountAsync(IUserContext userContext, MoneySourceType sourceType,
            long? sourceId, CancellationToken cancellationToken)
        {
            var initialDate = await GetInitialDateAsync(userContext, cancellationToken).ConfigureAwait(false);
            return await moneyTableDao
                .GetUnrecognizedOperationsCountAsync(userContext.FirmId, initialDate, sourceType, sourceId, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<ImportedMoneyTableResponse> GetImportedAsync(
            IUserContext userContext,
            MoneyTableRequest request,
            CancellationToken cancellationToken)
        {
            var initialDate = await GetInitialDateAsync(userContext, cancellationToken).ConfigureAwait(false);
            var response = await moneyTableDao.GetImportedAsync(userContext.FirmId, initialDate, request, cancellationToken).ConfigureAwait(false);
            var linkedDocumentsMap = await GetLinkedDocumentsMapAsync(userContext, response.Operations).ConfigureAwait(false);
            LoadLinkedDocumentsCount(response.Operations, linkedDocumentsMap);
            await LoadSalaryChargePaymentsAsync(userContext, response.Operations).ConfigureAwait(false);
            await LoadTaxSumsAsync(userContext, response.Operations).ConfigureAwait(false);
            await SetDocumentsPaidStatus(userContext, response.Operations, linkedDocumentsMap).ConfigureAwait(false);
            await LoadImportRulesAsync(userContext, response.Operations).ConfigureAwait(false);

            var paymentsToNaturalPersons = await LoadPaymentsToNaturalPersonsAsync(
                userContext, response.Operations, cancellationToken)
                .ConfigureAwait(false);
            await ApplyCanSendToBankAsync(userContext, response.Operations, paymentsToNaturalPersons).ConfigureAwait(false);
            ApplyCanDownload(response.Operations, paymentsToNaturalPersons);

            return response;
        }

        public async Task<int> GetImportedCountAsync(IUserContext userContext, MoneySourceType sourceType,
            long? sourceId, CancellationToken ctx)
        {
            var initialDate = await GetInitialDateAsync(userContext, ctx).ConfigureAwait(false);
            return await moneyTableDao.GetImportedCountAsync(userContext.FirmId, initialDate, sourceType, sourceId).ConfigureAwait(false);
        }

        public async Task<OutsourceProcessingMoneyTableResponse> GetOutsourceProcessingAsync(IUserContext userContext,
            OutsourceProcessingTableRequest request, CancellationToken ctx)
        {
            var initialDate = await GetInitialDateAsync(userContext, ctx).ConfigureAwait(false);
            var response = await moneyTableDao.GetOutsourceProcessingAsync(userContext.FirmId, initialDate, request, ctx).ConfigureAwait(false);
            var linkedDocumentsMap = await GetLinkedDocumentsMapAsync(userContext, response.Operations).ConfigureAwait(false);
            LoadLinkedDocumentsCount(response.Operations, linkedDocumentsMap);
            await LoadSalaryChargePaymentsAsync(userContext, response.Operations).ConfigureAwait(false);
            await LoadTaxSumsAsync(userContext, response.Operations).ConfigureAwait(false);
            await SetDocumentsPaidStatus(userContext, response.Operations, linkedDocumentsMap).ConfigureAwait(false);
            await LoadImportRulesAsync(userContext, response.Operations).ConfigureAwait(false);

            var paymentsToNatrualPersons = await LoadPaymentsToNaturalPersonsAsync(
                userContext, response.Operations, ctx)
                .ConfigureAwait(false);
            ApplyCanDownload(response.Operations, paymentsToNatrualPersons);

            return response;
        }

        public async Task<int> GetOutsourceProcessingCountAsync(IUserContext userContext,
            OutsourceProcessingTableRequest request, CancellationToken cancellationToken)
        {
            var initialDate = await GetInitialDateAsync(userContext, cancellationToken).ConfigureAwait(false);
            return await moneyTableDao
                .GetOutsourceProcessingCountAsync(userContext.FirmId, initialDate, request)
                .ConfigureAwait(false);
        }

        public async Task<MainMoneyMultiCurrencyTableResponse> GetMultiCurrencyTableAsync(IUserContext userContext,
            MainMoneyTableRequest request, CancellationToken cancellationToken)
        {
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            var taxationSystem = await taxationSystemClient
                .GetTaxationSystemAsync(userContext.FirmId, DateTime.Now.Year, cancellationToken)
                .ConfigureAwait(false);
            request.IsMultiCurrency = taxationSystem.IsUSN;

            var balanceMaster = await balanceMasterService
                .GetStatusAsync(userContext, cancellationToken)
                .ConfigureAwait(false);
            var initialDate = GetInitialDate(contextExtraData.FirmRegistrationDate, balanceMaster);
            var initialSummaryDate = GetInitialSummaryDate(contextExtraData.FirmRegistrationDate, balanceMaster);

            var response = await moneyTableDao
                .GetMultiCurrencyTableAsync(userContext.FirmId, initialDate, initialSummaryDate, request, cancellationToken)
                .ConfigureAwait(false);
            var operations = response.Operations.ToArray();

            if (!request.IsMultiCurrency)
            {
                AllCurrencyinRUB(operations);
            }

            OverwriteCurrencyCodes(operations.ToList());
            await LoadSalaryChargePaymentsAsync(userContext, operations).ConfigureAwait(false);
            await LoadTaxSumsAsync(userContext, operations).ConfigureAwait(false);
            await LoadImportRulesAsync(userContext, operations).ConfigureAwait(false);
            await LoadOutsourceImportRulesAsync(userContext, operations).ConfigureAwait(false);

            await ApplyBalanceSumAsync(userContext, request, balanceMaster, response.Summaries).ConfigureAwait(false);
            await ApplyCurrencyBalanceSumAsync(userContext, request, balanceMaster, response.Summaries).ConfigureAwait(false);
            response.BankBalance = await LoadBankBalanceAsync(userContext, request, initialDate).ConfigureAwait(false);

            var paymentsToNaturalPersons = await LoadPaymentsToNaturalPersonsAsync(
                userContext, operations, cancellationToken).ConfigureAwait(false);
            await ApplyCanSendToBankAsync(userContext, operations, paymentsToNaturalPersons).ConfigureAwait(false);
            await LoadPassThruPaymentStateAsync(userContext, operations).ConfigureAwait(false);

            ApplyCanDownload(operations, paymentsToNaturalPersons);

            await SetIpAsWorkerAsync(userContext, contextExtraData, operations).ConfigureAwait(false);

            var linkedDocumentsMap = await GetLinkedDocumentsMapAsync(userContext, operations).ConfigureAwait(false);
            LoadLinkedDocumentsCount(operations, linkedDocumentsMap);
            await SetDocumentsPaidStatus(userContext, operations, linkedDocumentsMap).ConfigureAwait(false);

            return response;
        }

        private async Task SetIpAsWorkerAsync(
            IUserContext userContext,
            IUserContextExtraData extraData,
            MainMoneyTableOperation[] operations)
        {
            var operationWithIpAsWorker = operations.Where(o => o.KontragentId == IpAsWorkerId).ToArray();
            if (extraData.IsOoo || operationWithIpAsWorker.Length == 0)
            {
                return;
            }

            var firm = await firmRequisitesClient.GetFirmByIdAsync(userContext.FirmId).ConfigureAwait(false);
            foreach (var operation in operationWithIpAsWorker)
            {
                operation.KontragentName = $"{firm.IpSurname.Trim()} {firm.IpName.Trim()} {firm.IpPatronymic.Trim()}".Trim();
            }
        }

        private async Task LoadPassThruPaymentStateAsync(IUserContext userContext, IReadOnlyCollection<MainMoneyTableOperation> operations)
        {
            var filterOperations = operations
                .Where(x => SendInvoiceOperationTypes.Contains(x.OperationType))
                .ToList();
            var filterDocumentBaseIds = filterOperations.Select(x => x.DocumentBaseId).ToList();

            try
            {
                var listInvoiceDetailByDocumentBaseIds = await bankOperationsApiClient
                    .GetListInvoiceDetailByDocumentBaseIdsAsync(
                        userContext.FirmId,
                        userContext.UserId,
                        filterDocumentBaseIds).ConfigureAwait(false);

                foreach (var operation in operations)
                {
                    if (listInvoiceDetailByDocumentBaseIds.Data.Any(x => x.DocumentBaseId == operation.DocumentBaseId))
                    {
                        var invoiceDetailResponseDto =
                            listInvoiceDetailByDocumentBaseIds.Data.OrderByDescending(x => x.CreateDate)
                                .First(x => x.DocumentBaseId == operation.DocumentBaseId);
                        
                        var bankStatus = invoiceDetailResponseDto.Status;
                        var mdStatus = operation.PaidStatus;
                        
                        // Если в МД статус не оплачено и ошибочные статусы из банка по сквозному(прямому) платежу
                        if (mdStatus == DocumentStatus.NotPayed)
                        {
                            if (bankStatus == InvoiceStatus.Failed || bankStatus == InvoiceStatus.Canceled)
                            {
                                operation.PassThruPaymentState = new PassThruPaymentState
                                {
                                    Message = invoiceDetailResponseDto.DescriptionStatus,
                                    Status = invoiceDetailResponseDto.Status,
                                    PartnerId = invoiceDetailResponseDto.IntegrationPartnerId
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(TAG, ex.Message, ex);
            }
        }

        private void OverwriteCurrencyCodes(IReadOnlyCollection<MainMoneyTableOperation> operations)
        {
            foreach (var op in operations)
            {
                op.Currency = op.SettlementType == SettlementAccountType.Default
                    ? Currency.RUB
                    : op.Currency;
            }
        }

        private async Task LoadBaseOperationsAsync(int firmId, IReadOnlyCollection<UnrecognizedMoneyTableOperation> operations)
        {
            var duplicates = operations
                .Where(x => x.OperationState == OperationState.Duplicate && x.BaseOperationId.HasValue)
                .ToList();
            var baseIds = duplicates
                .Where(x => x.BaseOperationId.HasValue)
                .Select(x => x.BaseOperationId.Value)
                .Distinct()
                .ToList();
            var baseOperations = (await paymentOrderOperationReader.GetByBaseIdsAsync(firmId, baseIds).ConfigureAwait(false))
                .ToDictionary(x => x.DocumentBaseId);
            foreach (var duplicate in duplicates)
            {
                if (!duplicate.BaseOperationId.HasValue ||
                    !baseOperations.ContainsKey(duplicate.BaseOperationId.Value))
                {
                    continue;
                }
                var paymentOrder = baseOperations[duplicate.BaseOperationId.Value];
                duplicate.BaseOperation = new MoneyOperation
                {
                    DocumentBaseId = paymentOrder.DocumentBaseId,
                    Date = paymentOrder.Date,
                    Number = paymentOrder.Number,
                    Direction = paymentOrder.Direction,
                    KontragentName = paymentOrder.KontragentName,
                    OperationType = paymentOrder.OperationType,
                    OperationState = paymentOrder.OperationState,
                    Sum = paymentOrder.Sum,
                    Currency = duplicate.Currency,
                    Description = paymentOrder.Description
                };
            }
        }

        private void LoadLinkedDocumentsCount(IReadOnlyCollection<MoneyTableOperation> operations,
            IReadOnlyDictionary<long, List<LinkedDocument>> linkedDocumentsMap)
        {
            foreach (var operation in operations)
            {
                var operationLinkedDocuments = linkedDocumentsMap.GetValueOrDefault(operation.DocumentBaseId);
                operation.LinkedDocumentsCount = operationLinkedDocuments?.Count ?? 0;
            }
        }

        private Task<Dictionary<long, List<LinkedDocument>>> GetLinkedDocumentsMapAsync(IUserContext userContext, IReadOnlyCollection<MoneyTableOperation> operations)
        {
            var baseIds = operations
                .Select(x => x.DocumentBaseId)
                .Distinct()
                .ToList();
            return linkedDocumentsReader.GetMapByBaseIdsAsync(userContext, baseIds);
        }

        private async Task LoadSalaryChargePaymentsAsync(IUserContext userContext, IReadOnlyCollection<MoneyTableOperation> operations)
        {
            var baseIds = operations
                .Where(x => x.OperationType == OperationType.PaymentOrderOutgoingForTransferSalary ||
                            x.OperationType == OperationType.CashOrderOutgoingPaymentForWorking)
                .Select(x => x.DocumentBaseId)
                .Distinct()
                .ToList();
            if (baseIds.Count == 0)
            {
                return;
            }
            var salaryChargePaymentSumDict = await salaryChargePaymentsReader.GetSalaryChargePaymentSumByIdsAsync(userContext, baseIds).ConfigureAwait(false);
            foreach (var operation in operations)
            {
                salaryChargePaymentSumDict.TryGetValue(operation.DocumentBaseId, out var salaryChargePaymentSum);
                operation.HasUnbindedSalaryChargePayments = operation.Sum > salaryChargePaymentSum;
            }
        }

        private async Task SetDocumentsPaidStatus(IUserContext userContext,
            IReadOnlyCollection<MoneyTableOperation> operations,
            IReadOnlyDictionary<long, List<LinkedDocument>> linkedDocumentsMap)
        {
            if (operations.Count == 0)
            {
                return;
            }

            // операции, кторые не имеют связей с любыми документами
            var operationsWithoutRelatedDocs = operations.Where(x => WithoutRelatedDocs.Contains(x.OperationType)).ToArray();
            foreach (var operation in operationsWithoutRelatedDocs)
            {
                operation.PrimaryDocStatus = PrimaryDocsStatus.CantHaveAnyDocs;
            }
            // костыль для Оплаты поставщику в валюте для ООО
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            var isOoo = contextExtraData.IsOoo;
            if (isOoo)
            {
                var currencyPaymentToSupplierOperations = operations
                    .Where(x => x.OperationType == OperationType.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods)
                    .ToArray();
                foreach (var operation in currencyPaymentToSupplierOperations)
                {
                    operation.PrimaryDocStatus = PrimaryDocsStatus.CantHaveAnyDocs;
                }
            }

            // операции, кторые не имеют связей с первичными документами
            var operationsWithoutPrimaryDocs = operations.Where(x => WithoutPrimaryDocs.Contains(x.OperationType)).ToArray();
            foreach (var operation in operationsWithoutPrimaryDocs)
            {
                operation.PrimaryDocStatus = PrimaryDocsStatus.CantHavePrimaryDocs;
            }

            // операции, кторые имеют связи с авансовыми отчетами
            var advanceOperations = operations.Where(x => WithAdvanceDocs.Contains(x.OperationType)).ToArray();
            foreach (var operation in advanceOperations)
            {
                operation.PrimaryDocStatus = linkedDocumentsMap.GetValueOrDefault(operation.DocumentBaseId)?.Any(y => y.Type == AccountingDocumentType.AccountingAdvanceStatement) == true
                    ? PrimaryDocsStatus.PaidPrimaryDocs
                    : PrimaryDocsStatus.UnpaidPrimaryDocs;
            }

            // операции, по которым наро посчитать сумму покрытия
            var coverableOperations = operations
                .Except(operationsWithoutRelatedDocs)
                .Except(operationsWithoutPrimaryDocs)
                .Except(advanceOperations)
                .ToArray();
            // костыль для Оплаты поставщику в валюте для ООО
            if (isOoo)
            {
                coverableOperations = coverableOperations
                .Where(co => co.OperationType != OperationType.PaymentOrderOutgoingCurrencyPaymentSuppliersForGoods)
                .ToArray();
            }

            foreach (var operation in coverableOperations)
            {
                var coveredSum = linkedDocumentsMap.GetValueOrDefault(operation.DocumentBaseId)
                    ?.Where(y => LinkedCoveringDocsTypes.Contains(y.Type))
                    .Sum(y => y.PayedSum) ?? 0;
                operation.PrimaryDocStatus = coveredSum >= operation.Sum
                    ? PrimaryDocsStatus.PaidPrimaryDocs
                    : PrimaryDocsStatus.UnpaidPrimaryDocs;

                operation.UncoveredSum = operation.Sum > coveredSum
                    ? operation.Sum - coveredSum
                    : 0;
            }
        }

        private async Task LoadTaxSumsAsync(IUserContext userContext, IReadOnlyCollection<MoneyTableOperation> operations)
        {
            var baseIds = operations
                .Select(x => x.DocumentBaseId)
                .Distinct()
                .ToList();
            if (baseIds.Count == 0)
            {
                return;
            }
            var taxSumsDict = await taxPostingsReader.GetSumsByBaseIdsAsync(userContext, baseIds).ConfigureAwait(false);

            foreach (var operation in operations)
            {
                if (taxSumsDict.TryGetValue(operation.DocumentBaseId, out var taxSums))
                {
                    operation.Taxes = taxSums;
                }
            }

            await LoadTaxSumsForUnifiedBudgetaryPaymentAsync(userContext, operations).ConfigureAwait(false);
        }

        private async Task LoadTaxSumsForUnifiedBudgetaryPaymentAsync(IUserContext userContext, IReadOnlyCollection<MoneyTableOperation> operations)
        {
            var unifiedBudgetaryPayments = operations
                .Where(x => UnifiedBudgetaryPayments.Contains(x.OperationType))
                .Distinct()
                .ToList();
            if (unifiedBudgetaryPayments.Count == 0)
            {
                return;
            }

            var unifiedTaxPayments = await unifiedBudgetarySubPaymentClient.GetByParentIdsAsync(
                    userContext.FirmId, userContext.UserId,
                    unifiedBudgetaryPayments.Select(x => x.DocumentBaseId).ToArray())
                .ConfigureAwait(false);

            var unifiedTaxPaymentIdsDict = unifiedTaxPayments.GroupBy(x => x.ParentDocumentId)
                .ToDictionary(x => x.Key,
                    x => x.Select(v => v.DocumentBaseId).ToList());

            var taxSumsDict = await taxPostingsReader.GetSumsBySubBaseIdsAsync(userContext, unifiedTaxPaymentIdsDict).ConfigureAwait(false);

            foreach (var operation in operations)
            {
                if (taxSumsDict.TryGetValue(operation.DocumentBaseId, out var taxSums))
                {
                    operation.Taxes = taxSums;
                }
            }
        }

        private async Task ApplyBalanceSumAsync(IUserContext userContext, MainMoneyTableRequest request, BalanceMasterStatus balanceMaster, IReadOnlyCollection<MainMoneyMultiCurrencyTableSummary> summaries)
        {
            var balancesSum = balanceMaster.IsCompleted
                ? await balancesReader.GetBalancesSumAsync(userContext, request.SourceType, request.SourceId).ConfigureAwait(false)
                : 0m;

            var rubSummaries = summaries
                .Where(currencyType => currencyType.Currency == Currency.RUB ||
                                       currencyType.Currency == Currency.RUR)
                .ToArray();

            foreach (var summary in rubSummaries)
            {
                summary.StartBalance += balancesSum;
                summary.EndBalance += balancesSum;
            }
        }

        private async Task ApplyCurrencyBalanceSumAsync(IUserContext userContext, MainMoneyTableRequest request, BalanceMasterStatus balanceMaster, IReadOnlyCollection<MainMoneyMultiCurrencyTableSummary> summaries)
        {
            var balances = balanceMaster.IsCompleted
                ? await balancesReader.GetCurrencyBalancesSumAsync(userContext, request.SourceType, request.SourceId).ConfigureAwait(false)
                : new Dictionary<Currency, decimal>();

            var currencySummaries = summaries
                .Where(currencyType => currencyType.Currency != Currency.RUB && currencyType.Currency != Currency.RUR).ToArray();

            foreach (var summary in currencySummaries)
            {
                var currencyBalanceSum = balances.ContainsKey(summary.Currency) ? balances[summary.Currency] : 0;
                summary.StartBalance += currencyBalanceSum;
                summary.EndBalance += currencyBalanceSum;
            }
        }

        private async Task<MainMoneyTableBankBalance> LoadBankBalanceAsync(IUserContext userContext, MainMoneyTableRequest request, DateTime initialDate)
        {
            if (request.SourceType != MoneySourceType.SettlementAccount)
            {
                return null;
            }

            if (request.SourceId == null)
            {
                return null;
            }

            var balance = await moneyBankBalanceApiClient.GetAsync(userContext.FirmId, userContext.UserId,
                    new BankBalanceRequestDto
                    {
                        SettlementAccountId = (int)request.SourceId.Value,
                        StartDate = request.StartDate ?? initialDate,
                        EndDate = request.EndDate ?? DateTime.Today
                    }).ConfigureAwait(false);

            if (balance == null)
            {
                return null;
            }

            return new MainMoneyTableBankBalance
            {
                SourceId = request.SourceId.Value,
                StartBalance = balance.StartBalance,
                EndBalance = balance.EndBalance,
                IncomingBalance = balance.IncomingBalance,
                OutgoingBalance = balance.OutgoingBalance
            };
        }

        private async Task<DateTime> GetInitialDateAsync(IUserContext userContext, CancellationToken cancellationToken)
        {
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            var balanceMaster = await balanceMasterService.GetStatusAsync(userContext, cancellationToken).ConfigureAwait(false);
            var initialDate = GetInitialDate(contextExtraData.FirmRegistrationDate, balanceMaster);
            return initialDate;
        }

        /// <summary>
        /// Дата с которой начинают учитываться операции в таблице денег
        /// </summary>
        private static DateTime GetInitialDate(DateTime? firmRegistrationDate, BalanceMasterStatus balanceMaster)
        {
            return firmRegistrationDate.HasValue &&
                firmRegistrationDate > balanceMaster.Date
                ? firmRegistrationDate.Value
                : balanceMaster.Date.GetStartDateForYear();
        }

        /// <summary>
        /// Дата с которой начинают учитываться операции в итогах под таблицей деньги
        /// </summary>
        private static DateTime GetInitialSummaryDate(DateTime? firmRegistrationDate, BalanceMasterStatus balanceMaster)
        {
            return firmRegistrationDate.HasValue &&
                   firmRegistrationDate > balanceMaster.Date
                ? firmRegistrationDate.Value
                : balanceMaster.Date;
        }

        private void AllCurrencyinRUB(IReadOnlyCollection<MoneyTableOperation> operations)
        {
            foreach (var operation in operations)
            {
                operation.Currency = Currency.RUB;
            }
        }

        private async Task LoadImportRulesAsync(IUserContext userContext, IReadOnlyCollection<MoneyTableOperation> operations)
        {
            var rules = await paymentImportRulesClient.GetAppliedRulesAsync(
                userContext.FirmId,
                userContext.UserId,
                operations.Select(x => x.DocumentBaseId).ToArray()
            ).ConfigureAwait(false);

            foreach (var operation in operations)
            {
                operation.ImportRules = rules
                    .Where(x => x.DocumentBaseId == operation.DocumentBaseId)
                    .Select(x => new MoneyTableOperationImportRule { Id = x.Id, Name = x.Name })
                    .ToArray();
            }
        }

        private async Task LoadOutsourceImportRulesAsync(IUserContext userContext, IReadOnlyCollection<MoneyTableOperation> operations)
        {
            var baseIds = operations.Select(o => o.DocumentBaseId).ToArray();
            var rules = await outsourceImportRulesClient
                .GetAppliedRulesAsync(userContext.FirmId, userContext.UserId, new AppliedRulesRequestDto
                {
                    DocumentBaseIds = baseIds,
                    ExcludeOutsourceProcessing = true,
                }).ConfigureAwait(false);
            foreach (var operation in operations)
            {
                operation.OutsourceImportRules = rules
                    .Where(x => x.DocumentBaseId == operation.DocumentBaseId)
                    .Select(x => new MoneyTableOperationImportRule { Id = x.RuleId, Name = x.RuleName })
                    .ToArray();
            }
        }

        private async Task<Dictionary<long, PaymentToNaturalPersonResponseDto>> LoadPaymentsToNaturalPersonsAsync(
            IUserContext userContext,
            IReadOnlyCollection<MoneyTableOperation> operations, CancellationToken cancellationToken)
        {
            const int batchSize = 50;
            var result = new Dictionary<long, PaymentToNaturalPersonResponseDto>();
            var chunks = operations
                .Where(operation => operation.OperationType == OperationType.PaymentOrderOutgoingForTransferSalary)
                .Select(operation => operation.DocumentBaseId)
                .Chunk(batchSize);
            var firmId = userContext.FirmId;
            var userId = userContext.UserId;

            foreach (var baseIds in chunks)
            {
                var payments = await paymentToNaturalPersonsClient
                    .GetByBaseIdsIdAsync(firmId, userId, baseIds.ToArray(), cancellationToken)
                    .ConfigureAwait(false);

                foreach (var operation in payments)
                {
                    result[operation.DocumentBaseId] = operation;
                }
            }

            return result;
        }

        private static void ApplyCanDownload(
            IReadOnlyCollection<MoneyTableOperation> operations,
            Dictionary<long, PaymentToNaturalPersonResponseDto> paymentsToNaturalPersons)
        {
            foreach (var operation in operations)
            {
                var operationType = operation.OperationType;
                if (operationType.IsMemorialWarrant()
                    || operationType.IsPurseOperation()
                    || operationType.IsCurrencyOperation())
                {
                    continue;
                }

                // Зарплатный проект
                if (operationType == OperationType.PaymentOrderOutgoingForTransferSalary)
                {
                    if (!operation.KontragentId.HasValue)
                    {
                        continue;
                    }

                    // Старая костыльная "Выплата физ. лицам" с 2 и более сотрудниками
                    if (IsOldFakeSalaryProjectOperation(operation, paymentsToNaturalPersons))
                    {
                        continue;
                    }
                }

                operation.CanDownload = true;
            }
        }

        private async Task ApplyCanSendToBankAsync(
            IUserContext userContext,
            IReadOnlyCollection<MoneyTableOperation> operations,
            Dictionary<long, PaymentToNaturalPersonResponseDto> paymentsToNaturalPersons)
        {
            await canSendToBankReader.ApplyCanSendToBank(userContext, operations).ConfigureAwait(false);

            foreach (var operation in operations)
            {
                if (IsOldFakeSalaryProjectOperation(operation, paymentsToNaturalPersons) || operation.OperationType == OperationType.PaymentOrderOutgoingRentPayment || operation.KontragentId == IpAsWorkerId)
                {
                    operation.CanSendToBank = false;
                }
            }
        }

        /// <summary>
        /// Старая костыльная "Выплата физ. лицам" с 2 и более сотрудниками
        /// </summary>
        private static bool IsOldFakeSalaryProjectOperation(
            MoneyTableOperation operation,
            Dictionary<long, PaymentToNaturalPersonResponseDto> paymentsToNaturalPersons)
        {

            return paymentsToNaturalPersons.TryGetValue(operation.DocumentBaseId, out PaymentToNaturalPersonResponseDto moneyOperation)
                && moneyOperation.EmployeePayments.Count > 1
                && moneyOperation.PaymentType == PaymentToNaturalPersonsType.WorkContract;
        }
    }
}