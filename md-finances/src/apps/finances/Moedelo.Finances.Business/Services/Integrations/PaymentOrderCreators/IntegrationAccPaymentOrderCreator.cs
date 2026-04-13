using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Moedelo.Accounting.Domain.Shared.PaymentOrders.Outgoing.BudgetaryPayments;
using Moedelo.Address.Client.Address;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.Models.PaymentOrder;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.Common.Enums.Enums.Payroll.Deductions;
using Moedelo.Common.Enums.Extensions.Money;
using Moedelo.Common.Enums.Extensions.PostingEngine;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Business.Mappers.Integrations;
using Moedelo.Finances.Domain.Enums.Money.Operations.PaymentOrders;
using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.KontragentsV2.Client.SettlementAccounts;
using Moedelo.KontragentsV2.Dto;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.PayrollV2.Client.Employees;
using Moedelo.PayrollV2.Client.Payments;
using Moedelo.PayrollV2.Dto.Employees;
using Moedelo.RequisitesV2.Client.FirmRequisites;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.FirmRequisites;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using BankDocType = Moedelo.Common.Enums.Enums.Accounting.BankDocType;
using BudgetaryPaymentBase = Moedelo.BankIntegrations.Enums.BudgetaryPaymentBase;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using OperationType = Moedelo.Common.Enums.Enums.PostingEngine.OperationType;

namespace Moedelo.Finances.Business.Services.Integrations.PaymentOrderCreators
{
    [InjectPerWebRequest]
    public class IntegrationAccPaymentOrderCreator : IIntegrationAccPaymentOrderCreator
    {
        private const string TAG = nameof(IntegrationAccPaymentOrderCreator);

        private const int OktmoStartYear = 2014;
        private const string TinkoffSalaryProjectTransitSettlementNumber = "47422810900000081042";

        private readonly AsyncLazy<FirmDto> requisitesCache;
        private readonly ConcurrentDictionary<int, SettlementAccountDto> settlementAccountsCache = new ConcurrentDictionary<int, SettlementAccountDto>();
        private readonly ConcurrentDictionary<int, BankDto> banksCache = new ConcurrentDictionary<int, BankDto>();
        private readonly ConcurrentDictionary<int, KontragentDto> kontragentsCache = new ConcurrentDictionary<int, KontragentDto>();
        private readonly ConcurrentDictionary<int, WorkerDto> workersCache = new ConcurrentDictionary<int, WorkerDto>();
        private readonly ConcurrentDictionary<int, WorkerCardAccountDto> workerCardAccountsCache = new ConcurrentDictionary<int, WorkerCardAccountDto>();

        private static readonly Dictionary<PaymentOrderNdsType, NdsType> NdsTypesDictionary =
            Enum.GetValues(typeof(PaymentOrderNdsType))
                .Cast<PaymentOrderNdsType>()
                .ToDictionary(
                    key => key,
                    PaymentOrderMapper.MapNdsType
                );
        
        private readonly IUserContext userContext;
        private readonly IAddressApiClient addressApiClient;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IKontragentsClient kontragentsClient;
        private readonly IKontragentSettlementAccountsClient kontragentSettlementAccountsClient;
        private readonly IEmployeesApiClient employeesApiClient;
        private readonly IBanksApiClient banksApiClient;
        private readonly ILogger logger;
        private readonly IChargePaymentsApiClient chargePaymentsApiClient;

        public IntegrationAccPaymentOrderCreator(
            IUserContext userContext,
            IFirmRequisitesClient firmRequisitesClient,
            IAddressApiClient addressApiClient,
            ISettlementAccountClient settlementAccountClient,
            IKontragentsClient kontragentsClient,
            IKontragentSettlementAccountsClient kontragentSettlementAccountsClient,
            IEmployeesApiClient employeesApiClient,
            IBanksApiClient banksApiClient,
            ILogger logger,
            IChargePaymentsApiClient chargePaymentsApiClient)
        {
            this.userContext = userContext;
            this.addressApiClient = addressApiClient;
            this.settlementAccountClient = settlementAccountClient;
            this.kontragentsClient = kontragentsClient;
            this.kontragentSettlementAccountsClient = kontragentSettlementAccountsClient;
            this.employeesApiClient = employeesApiClient;
            this.banksApiClient = banksApiClient;
            this.logger = logger;
            this.chargePaymentsApiClient = chargePaymentsApiClient;
            requisitesCache = new AsyncLazy<FirmDto>(
                () => firmRequisitesClient.GetFirmByIdAsync(userContext.FirmId));
        }

        public async Task<PaymentOrderDto> CreateAsync(Guid guid, object order, IntegrationPartners? integrationPartner = null)
        {
            var operation = order as PaymentOrderOperation;
            var snapshot = DeserializeSnapshot(operation);
            var purposeCode = await GetPurposeCodeAsync(operation, snapshot).ConfigureAwait(false);
            var paymentOrder = new PaymentOrderDto
            {
                Guid = guid,
                PaymentNumber = operation.Number,
                OrderDate = operation.Date,
                Sum = operation.Sum,
                Direction = (PaymentDirection)operation.Direction,
                Purpose = operation.Description,
                PurposeCode = purposeCode,
                PaymentPriority = (PaymentPriority)operation.PaymentPriority,
                OrderType = (OrderType)snapshot.OrderType
            };
            if (operation.IncludeNds)
            {
                if (NdsTypesDictionary.TryGetValue(operation.NdsType, out var value))
                {
                    paymentOrder.NdsType = value;
                    paymentOrder.NdsSum = operation.NdsSum.GetValueOrDefault(0);
                }
                else
                {
                    logger.Error(TAG, $"Nds type {operation.NdsType} not exists in dictionary for operation with baseId = {operation.DocumentBaseId}", null, userContext.GetAuditContext());
                }
            }
            await MapPayerAndRecipientAsync(operation, snapshot, paymentOrder, integrationPartner).ConfigureAwait(false);
            await MapBudgetaryFields(snapshot, paymentOrder, operation).ConfigureAwait(false);
            MapDeductionsPaymentFields(snapshot, paymentOrder, operation.OperationType);
            return paymentOrder;
        }

        private static PaymentSnapshot DeserializeSnapshot(PaymentOrderOperation operation)
        {
            PaymentSnapshot snapshot;
            var serializer = new XmlSerializer(typeof(PaymentSnapshot));
            using (var reader = new StringReader(operation.PaymentSnapshot))
            {
                snapshot = serializer.Deserialize(reader) as PaymentSnapshot;
            }
            return snapshot;
        }

        private async Task MapPayerAndRecipientAsync(PaymentOrderOperation operation, PaymentSnapshot snapshot, PaymentOrderDto paymentOrder, IntegrationPartners? integrationPartner)
        {
            paymentOrder.Payer = await GetFirmDetailsAsync(operation.SettlementAccountId, operation.OrderType, integrationPartner).ConfigureAwait(false);
            paymentOrder.Recipient = await GetKontragentDetailsAsync(operation, snapshot, integrationPartner).ConfigureAwait(false);
        }

        private async Task MapBudgetaryFields(PaymentSnapshot snapshot, PaymentOrderDto paymentOrder, PaymentOrderOperation operation)
        {
            if (snapshot.OrderType != PaymentOrderType.BudgetaryPayment)
            {
                return;
            }

            paymentOrder.BudgetaryPayerStatus = (BudgetaryPayerStatus)snapshot.BudgetaryPayerStatus;
            paymentOrder.Kbk = snapshot.Kbk;
            paymentOrder.BudgetaryOkato = snapshot.BudgetaryOkato;
            paymentOrder.BudgetaryPaymentBase = (BudgetaryPaymentBase)snapshot.BudgetaryPaymentBase;
            paymentOrder.BudgetaryPeriod = GetBudgetaryPeriod(snapshot, operation.BudgetaryTaxesAndFees);
            paymentOrder.BudgetaryDocNumber = snapshot.BudgetaryDocNumber;
            paymentOrder.BudgetaryDocDate = snapshot.BudgetaryDocDate;
            paymentOrder.BudgetaryPaymentType = (BudgetaryPaymentType)snapshot.BudgetaryPaymentType;
            paymentOrder.KindOfPay = snapshot.KindOfPay;
            paymentOrder.NumberTop = snapshot.NumberTop;
            paymentOrder.BankDocType = (BankIntegrations.Enums.BankDocType)snapshot.BankDocType;
            paymentOrder.CodeUin = snapshot.CodeUin;
            var orderDetails = snapshot.Direction == Common.Enums.Enums.Accounting.PaymentDirection.Incoming
                ? snapshot.Payer
                : snapshot.Recipient;
            if (snapshot.OrderDate.Year < OktmoStartYear)
            {
                orderDetails.Okato = snapshot.BudgetaryOkato;
            }
            else
            {
                orderDetails.Oktmo = snapshot.BudgetaryOkato;
            }

            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(paymentOrder.Payer?.Kpp) && !contextExtraData.IsOoo )
            {
                paymentOrder.Payer.Kpp = "0";
            }

            if (operation.Date >= BudgetaryPaymentDates.FormatDate16052025 &&
                operation.OperationType == OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment)
            {
                paymentOrder.Payer.Kpp = "0";
            }

            if (IsDeductionFromSalary(paymentOrder.BudgetaryPayerStatus))
            {
                paymentOrder.Payer.Inn = snapshot.Payer.Inn;
            }
        }

        private static BudgetaryPeriod GetBudgetaryPeriod(PaymentSnapshot snapshot, int? accountCodes)
        {
            return accountCodes == (int?)BudgetaryAccountCodes.UnifiedBudgetaryPayment
                ? new BudgetaryPeriod(0, BudgetaryPeriodType.None, 0)
                : new BudgetaryPeriod(snapshot.BudgetaryPeriod.Number,
                    (BudgetaryPeriodType)snapshot.BudgetaryPeriod.Type, 
                    snapshot.BudgetaryPeriod.Year, 
                    snapshot.BudgetaryPeriod.Date);
        }

        private void MapDeductionsPaymentFields(PaymentSnapshot snapshot, PaymentOrderDto paymentOrder,
            OperationType operationType)
        {
            if (operationType != OperationType.PaymentOrderOutgoingDeduction)
            {
                return;
            }

            paymentOrder.Payer.Kpp = snapshot.Payer.Kpp;
            paymentOrder.Payer.Inn = snapshot.Payer.Inn;
            if (!IsDeductionFromSalary((BudgetaryPayerStatus)snapshot.BudgetaryPayerStatus))
            {
                return;
            }

            paymentOrder.BudgetaryPayerStatus = (BudgetaryPayerStatus)snapshot.BudgetaryPayerStatus;
            paymentOrder.CodeUin = snapshot.CodeUin;
            paymentOrder.BudgetaryDocNumber = snapshot.BudgetaryDocNumber;
        }

        private async Task<OrderDetailsDto> GetFirmDetailsAsync(int? settlementAccountId, BankDocType orderType, IntegrationPartners? integrationPartner)
        {
            var requisites = await requisitesCache.Value.ConfigureAwait(false);
            var settlementAccount = settlementAccountId.HasValue
                ? await GetSettlementAccountAsync(settlementAccountId.Value).ConfigureAwait(false)
                : null;
            var bank = settlementAccount != null
                ? await GetBankAsync(settlementAccount.BankId).ConfigureAwait(false)
                : null;
            var address = await addressApiClient.GetFirmAddressString(userContext.FirmId).ConfigureAwait(false);
            var organizationName = await GetFirmNameAsync(orderType).ConfigureAwait(false);
            return new OrderDetailsDto
            {
                Name = organizationName,
                Inn = requisites.Inn,
                Kpp = requisites.Kpp,
                SettlementNumber = settlementAccount.Number,
                BankName = GetBankName(bank, integrationPartner),
                BankBik = bank.Bik,
                BankCorrespondentAccount = bank.CorrespondentAccount,
                BankCity = bank.City,
                IsOoo = requisites.IsOoo,
                Address = address,
                Okato = requisites.Okato,
                Oktmo = requisites.Oktmo
            };
        }

        private async Task<string> GetFirmNameAsync(BankDocType orderType)
        {
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            if (!contextExtraData.IsOoo && orderType == BankDocType.BudgetaryPayment)
            {
                var requisites = await requisitesCache.Value.ConfigureAwait(false);
                var fio = $"{requisites.IpSurname} {requisites.IpName} {requisites.IpPatronymic}".Trim();
                return $"{fio} (ИП)";
            }
            return contextExtraData.OrganizationName;
        }

        private async Task<OrderDetailsDto> GetKontragentDetailsAsync(PaymentOrderOperation operation, PaymentSnapshot snapshot, IntegrationPartners? integrationPartner)
        {
            if (operation.OperationType.IsWorkerOperation() && operation.WorkerId.HasValue)
            {
                return await GetWorkerDetailsAsync(operation, integrationPartner).ConfigureAwait(false);
            }

            if (operation.OperationType.IsOperationBetweenSettlementAccounts() && operation.TransferSettlementAccountId.HasValue)
            {
                return await GetFirmDetailsAsync(operation.TransferSettlementAccountId.Value, operation.OrderType, integrationPartner).ConfigureAwait(false);
            }

            if (snapshot == null)
            {
                return new OrderDetailsDto();
            }

            /*var orderDetails = snapshot.Direction == Common.Enums.Enums.Accounting.PaymentDirection.Incoming
                ? snapshot.Payer
                : snapshot.Recipient;*/
            var orderDetails = snapshot.Recipient;

            var bank = await GetBankAsync(orderDetails.BankBik).ConfigureAwait(false);

            var result = new OrderDetailsDto
            {
                Name = orderDetails.Name,
                Inn = orderDetails.Inn,
                Kpp = orderDetails.Kpp,
                SettlementNumber = orderDetails.SettlementNumber,
                BankBik = orderDetails.BankBik,
                BankName = bank != null ? GetBankName(bank, integrationPartner) : orderDetails.BankName,
                BankCity = bank != null ? bank.City : orderDetails.BankCity,
                BankCorrespondentAccount = orderDetails.BankCorrespondentAccount,
                IsOoo = orderDetails.IsOoo,
                Okato = orderDetails.Okato,
                Oktmo = orderDetails.Oktmo,
            };

            if (snapshot.OrderDate.Year < OktmoStartYear)
            {
                result.Okato = orderDetails.Okato;
            }
            else
            {
                result.Oktmo = orderDetails.Oktmo;
            }

            if (string.IsNullOrEmpty(result.SettlementNumber) && operation.KontragentId.HasValue)
            {
                await GetKontragentSettlementAccount(operation.KontragentId.Value, result, integrationPartner).ConfigureAwait(false);
                return result;
            }

            if (string.IsNullOrEmpty(result.BankBik))
            {
                return result;
            }

            return result;
        }

        private async Task GetKontragentSettlementAccount(int kontragentId, OrderDetailsDto kontragent, IntegrationPartners? integrationPartner)
        {
            var settlementAccounts = await kontragentSettlementAccountsClient.GetByKontragentAsync(userContext.FirmId, userContext.UserId, kontragentId).ConfigureAwait(false);
            var settlementAccount = settlementAccounts.FirstOrDefault();
            if (settlementAccount == null)
            {
                return;
            }
            kontragent.SettlementNumber = settlementAccount.Number;
            if (settlementAccount.BankId == null)
            {
                return;
            }
            var bank = await GetBankAsync(settlementAccount.BankId.Value).ConfigureAwait(false);
            if (bank != null)
            {
                kontragent.BankName = GetBankName(bank, integrationPartner);
                kontragent.BankBik = bank.Bik;
                kontragent.BankCity = bank.City;
                kontragent.BankCorrespondentAccount = bank.CorrespondentAccount;
            }
        }

        private async Task<OrderDetailsDto> GetWorkerDetailsAsync(PaymentOrderOperation operation, IntegrationPartners? integrationPartner)
        {
            if (operation.WorkerId == null)
            {
                return new OrderDetailsDto
                {
                    Name = operation.KontragentName
                };
            }
            var worker = await GetWorkerAsync(operation.WorkerId.Value).ConfigureAwait(false);
            if (worker == null)
            {
                return new OrderDetailsDto
                {
                    Name = operation.KontragentName
                };
            }
            var workerCardAccount = await GetWorkerCardAccountAsync(operation.WorkerId.Value).ConfigureAwait(false);
            if (workerCardAccount == null || string.IsNullOrEmpty(workerCardAccount.Number))
            {
                return new OrderDetailsDto
                {
                    Name = worker.FullName,
                    Inn = worker.Inn
                };
            }
            var name = string.IsNullOrWhiteSpace(workerCardAccount.Recipient)
                ? worker.FullName
                : workerCardAccount.Recipient;
            var inn = string.IsNullOrEmpty(workerCardAccount.InnRecipient)
                ? worker.Inn
                : workerCardAccount.InnRecipient;
            var bank = workerCardAccount.BankId.HasValue
                ? await GetBankAsync(workerCardAccount.BankId.Value).ConfigureAwait(false)
                : null;
            var address = worker.ActualAddress != null
                ? await addressApiClient.GetAddressString(worker.ActualAddress.Value).ConfigureAwait(false)
                : null;
            return new OrderDetailsDto
            {
                Name = name,
                Inn = inn?.Trim(),
                SettlementNumber = workerCardAccount.Number?.Trim(),
                BankName = operation.Direction == Domain.Enums.Money.MoneyDirection.Outgoing
                    ? GetBankName(bank, integrationPartner)
                    : bank.FullName,
                BankBik = bank.Bik,
                BankCorrespondentAccount = bank.CorrespondentAccount,
                BankCity = bank.City,
                Address = address,
            };
        }

        private async Task<SettlementAccountDto> GetSettlementAccountAsync(int id)
        {
            if (settlementAccountsCache.TryGetValue(id, out var settlementAccount))
            {
                return settlementAccount;
            }
            settlementAccount = await settlementAccountClient.GetByIdAsync(userContext.FirmId, userContext.UserId, id).ConfigureAwait(false);
            if (settlementAccount != null)
            {
                settlementAccountsCache.TryAdd(id, settlementAccount);
            }
            return settlementAccount;
        }

        private async Task<BankDto> GetBankAsync(int id)
        {
            if (banksCache.TryGetValue(id, out var bank))
            {
                return bank;
            }
            bank = (await banksApiClient.GetByIdsAsync(new[] { id }).ConfigureAwait(false)).FirstOrDefault();
            if (bank != null)
            {
                banksCache.TryAdd(id, bank);
            }
            return bank;
        }

        private async Task<BankDto> GetBankAsync(string bik)
        {
            var bank = banksCache.Values.FirstOrDefault(x => x.Bik == bik);
            if (bank != null)
            {
                return bank;
            }
            bank = (await banksApiClient.GetByBiksAsync(new[] { bik }).ConfigureAwait(false)).FirstOrDefault();
            if (bank != null)
            {
                banksCache.TryAdd(bank.Id, bank);
            }
            return bank;
        }

        private static string GetBankName(BankDto bank, IntegrationPartners? integrationPartner)
        {
            var partner = integrationPartner ?? bank.IntegratedPartner;
            return partner == IntegrationPartners.OpenBank ||
                   partner == IntegrationPartners.SberBank ||
                   partner == IntegrationPartners.SberBankEasyFinance ||
                   partner == null
                ? bank.FullName
                : bank.FullNameWithCity;
        }

        private async Task<KontragentDto> GetKontragentAsync(int id)
        {
            if (kontragentsCache.TryGetValue(id, out var kontragent))
            {
                return kontragent;
            }
            kontragent = await kontragentsClient.GetByIdAsync(userContext.FirmId, userContext.UserId, id).ConfigureAwait(false);
            if (kontragent != null)
            {
                kontragentsCache.TryAdd(id, kontragent);
            }
            return kontragent;
        }

        private async Task<WorkerDto> GetWorkerAsync(int id)
        {
            if (workersCache.TryGetValue(id, out var worker))
            {
                return worker;
            }
            worker = await employeesApiClient.GetWorkerAsync(userContext.FirmId, userContext.UserId, id).ConfigureAwait(false);
            if (worker != null)
            {
                workersCache.TryAdd(id, worker);
            }
            return worker;
        }

        private async Task<WorkerCardAccountDto> GetWorkerCardAccountAsync(int id)
        {
            if (workerCardAccountsCache.TryGetValue(id, out var workerCardAccount))
            {
                return workerCardAccount;
            }
            workerCardAccount = await employeesApiClient.GetWorkerCardAccount(userContext.FirmId, userContext.UserId, id).ConfigureAwait(false);
            if (workerCardAccount != null)
            {
                workerCardAccountsCache.TryAdd(id, workerCardAccount);
            }
            return workerCardAccount;
        }
        private async Task<string> GetPurposeCodeAsync(PaymentOrderOperation operation, PaymentSnapshot paymentSnapshot)
        {
            if (operation.OperationType != Common.Enums.Enums.PostingEngine.OperationType.PaymentOrderOutgoingForTransferSalary)
            {
                return string.Empty;
            }

            if (operation.UnderContract.IsSalaryProjectDocumentType() &&
                paymentSnapshot?.Recipient?.SettlementNumber == TinkoffSalaryProjectTransitSettlementNumber)
            {
                return string.Empty;
            }

                var applyDeductionStatus = await chargePaymentsApiClient.GetApplyDeductionStatusAsync(userContext.FirmId, userContext.UserId, operation.DocumentBaseId, operation.Date).ConfigureAwait(false);
                var purposeCode = applyDeductionStatus == ApplyDeductionStatus.None ? string.Empty : ((int)applyDeductionStatus).ToString();
                return purposeCode;
        }

        private bool IsDeductionFromSalary(BudgetaryPayerStatus payerStatus)
        {
            return payerStatus == BudgetaryPayerStatus.DeductionFromSalary
                || payerStatus == BudgetaryPayerStatus.BailiffPayment;
        }
    }

    [InjectAsSingleton]
    public class IntegrationAccPaymentOrderCreatorStateless : IIntegrationAccPaymentOrderCreator
    {
        private readonly IDIResolver diResolver;

        private readonly AsyncLocal<IIntegrationAccPaymentOrderCreator> paymentOrderCreator = new AsyncLocal<IIntegrationAccPaymentOrderCreator>();
        private IIntegrationAccPaymentOrderCreator PaymentOrderCreator => paymentOrderCreator.Value ?? (paymentOrderCreator.Value = diResolver.GetInstance<IIntegrationAccPaymentOrderCreator>());

        public IntegrationAccPaymentOrderCreatorStateless(IDIResolver diResolver)
        {
            this.diResolver = diResolver;
        }

        public Task<PaymentOrderDto> CreateAsync(Guid guid, object order, IntegrationPartners? integrationPartner = null)
        {
            return PaymentOrderCreator.CreateAsync(guid, order, integrationPartner);
        }
    }
}