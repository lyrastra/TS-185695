using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Address.Client.Address;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.Enums;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.Common.Enums.Enums.Kontragents;
using Moedelo.Common.Enums.Enums.Payroll.Deductions;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Enums.Money.Operations.MoneyTransfers;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.KontragentsV2.Client.Kpps;
using Moedelo.KontragentsV2.Client.SettlementAccounts;
using Moedelo.KontragentsV2.Dto;
using Moedelo.PayrollV2.Client.Employees;
using Moedelo.PayrollV2.Client.Payments;
using Moedelo.PayrollV2.Dto.Employees;
using Moedelo.RequisitesV2.Client.FirmRequisites;
using Moedelo.RequisitesV2.Client.Purses;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.FirmRequisites;
using Moedelo.RequisitesV2.Dto.Purses;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.Finances.Business.Services.Integrations.PaymentOrderCreators
{
    [InjectPerWebRequest]
    public class IntegrationBizPaymentOrderCreator : IIntegrationBizPaymentOrderCreator
    {
        private const int PersonalCardMethod = 2;
        private const string MovementFromSettlementToSettlementDefaultDescription = "Перевод собственных средств для пополнения счета. НДС не облагается.";

        private readonly AsyncLazy<FirmDto> requisitesCache;
        private readonly ConcurrentDictionary<int, SettlementAccountDto> settlementAccountsCache = new ConcurrentDictionary<int, SettlementAccountDto>();
        private readonly ConcurrentDictionary<int, BankDto> banksCache = new ConcurrentDictionary<int, BankDto>();
        private readonly ConcurrentDictionary<int, PurseDto> pursesCache = new ConcurrentDictionary<int, PurseDto>();
        private readonly ConcurrentDictionary<int, KontragentDto> kontragentsCache = new ConcurrentDictionary<int, KontragentDto>();
        private readonly ConcurrentDictionary<int, WorkerDto> workersCache = new ConcurrentDictionary<int, WorkerDto>();
        private readonly ConcurrentDictionary<int, WorkerCardAccountDto> workerCardAccountsCache = new ConcurrentDictionary<int, WorkerCardAccountDto>();

        private readonly IUserContext userContext;
        private readonly IAddressApiClient addressApiClient;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IKontragentsClient kontragentsClient;
        private readonly IKontragentKppsClient kontragentKppsClient;
        private readonly IKontragentSettlementAccountsClient kontragentSettlementAccountsClient;
        private readonly IEmployeesApiClient employeesApiClient;
        private readonly IBanksApiClient banksApiClient;
        private readonly IPurseClient purseClient;
        private readonly IChargePaymentsApiClient chargePaymentsApiClient;

        public IntegrationBizPaymentOrderCreator(
            IUserContext userContext,
            IFirmRequisitesClient firmRequisitesClient,
            IAddressApiClient addressApiClient,
            ISettlementAccountClient settlementAccountClient,
            IKontragentsClient kontragentsClient,
            IKontragentKppsClient kontragentKppsClient,
            IKontragentSettlementAccountsClient kontragentSettlementAccountsClient,
            IEmployeesApiClient employeesApiClient,
            IBanksApiClient banksApiClient,
            IPurseClient purseClient,
            IChargePaymentsApiClient chargePaymentsApiClient)
        {
            this.userContext = userContext;
            this.addressApiClient = addressApiClient;
            this.settlementAccountClient = settlementAccountClient;
            this.kontragentsClient = kontragentsClient;
            this.kontragentKppsClient = kontragentKppsClient;
            this.kontragentSettlementAccountsClient = kontragentSettlementAccountsClient;
            this.employeesApiClient = employeesApiClient;
            this.banksApiClient = banksApiClient;
            this.purseClient = purseClient;
            this.chargePaymentsApiClient = chargePaymentsApiClient;
            requisitesCache = new AsyncLazy<FirmDto>(
                () => firmRequisitesClient.GetFirmByIdAsync(userContext.FirmId));
        }

        public async Task<PaymentOrderDto> CreateAsync(Guid guid, object order, IntegrationPartners? integrationPartner = null)
        {
            var operation = order as MoneyTransferOperation;

            var paymentPriority = Common.Enums.Enums.Accounting.PaymentPriority.Fifth;
            if (operation.OperationType == MoneyTransferOperationType.PayDays)
            {
                if (operation.PaymentType == /* PayDaysPaymentTypes.Salary*/ 1 ||
                    operation.MoneyBayType == MoneyBayType.SalaryProject)
                {
                    paymentPriority = Common.Enums.Enums.Accounting.PaymentPriority.Third;
                }
            }
            var purposeCode = await GetPurposeCodeAsync(operation).ConfigureAwait(false);
            var paymentOrder = new PaymentOrderDto
            {
                Guid = guid,
                PaymentNumber = operation.Number,
                OrderDate = operation.Date,
                Sum = operation.Sum,
                Direction = (PaymentDirection)operation.Direction,
                Purpose = operation.Description,
                PurposeCode = purposeCode,
                PaymentPriority = (PaymentPriority)paymentPriority,
            };

            if (operation.OperationType == MoneyTransferOperationType.MovementFromSettlementToSettlement && string.IsNullOrEmpty(paymentOrder.Purpose))
            {
                paymentOrder.Purpose = MovementFromSettlementToSettlementDefaultDescription;
            }

            await MapPayerAndRecipientAsync(operation, paymentOrder, integrationPartner).ConfigureAwait(false);
            await MapBudgetaryFields(operation, paymentOrder).ConfigureAwait(false);
            return paymentOrder;
        }

        private async Task MapPayerAndRecipientAsync(MoneyTransferOperation operation, PaymentOrderDto paymentOrder, IntegrationPartners? integrationPartner)
        {
            paymentOrder.Payer = await GetFirmDetailsAsync(operation.SettlementAccountId, integrationPartner).ConfigureAwait(false);

            paymentOrder.Recipient = new OrderDetailsDto
            {
                Name = operation.OperationType == MoneyTransferOperationType.LoansThirdPartiesOutgoing
                    ? operation.Recepient
                    : operation.KontragentName
            };

            if (operation.OperationType == MoneyTransferOperationType.LoansThirdPartiesOutgoing)
            {
                paymentOrder.Recipient.Name = operation.Recepient;
            }

            if (operation.KontragentId.HasValue)
            {
                paymentOrder.Recipient = await GetKontragentDetailsAsync(operation, integrationPartner).ConfigureAwait(false);
            }
            else if (operation.WorkerId.HasValue)
            {
                paymentOrder.Recipient = await GetWorkerDetailsAsync(operation, integrationPartner).ConfigureAwait(false);
            }

            if (operation.OperationType == MoneyTransferOperationType.PayDays && operation.MoneyBayType == MoneyBayType.SalaryProject)
            {
                var bank = await GetBankAsync(paymentOrder.Payer?.BankBik).ConfigureAwait(false);
                paymentOrder.Recipient = new OrderDetailsDto
                {
                    Name = bank?.FullName ?? string.Empty,
                    BankName = bank.FullName ?? string.Empty,
                    BankBik = bank?.Bik ?? string.Empty,
                    Inn = bank?.Inn ?? string.Empty,
                    Kpp = bank?.Kpp ?? string.Empty,
                    BankCorrespondentAccount = bank?.CorrespondentAccount ?? string.Empty,
                    SettlementNumber = operation.BankSettlementAccount
                };
            }
        }

        private Task MapBudgetaryFields(MoneyTransferOperation operation, PaymentOrderDto paymentOrder)
        {
            if (operation.OperationType != MoneyTransferOperationType.BudgetaryPayment)
            {
                return Task.CompletedTask;
            }

            switch (operation.BudgetaryPaymentType)
            {
                case Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType.Patent:
                    //var patent = LoadPatent(facade);
                    //paymentOrder = BudgetaryPaymentOrder.GetPatentPaymentOrder(facade, operation, patent);
                    break;
                case Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType.Other:
                case Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType.PatentNew:
                case Common.Enums.Enums.FinancialOperations.BudgetaryPaymentType.TradingTax:
                    //paymentOrder = BudgetaryPaymentOtherOrder.GetPaymentOrder(facade, operation);
                    break;
                default:
                    //paymentOrder = BudgetaryPaymentOrder.GetPaymentOrder(facade, operation);
                    break;
            }

            //paymentOrder.BudgetaryPayerStatus = snapshot.BudgetaryPayerStatus;
            paymentOrder.Kbk = operation.Kbk;
            /*paymentOrder.BudgetaryOkato = snapshot.BudgetaryOkato;
            paymentOrder.BudgetaryPaymentBase = snapshot.BudgetaryPaymentBase;
            paymentOrder.BudgetaryPeriod = snapshot.BudgetaryPeriod;
            paymentOrder.BudgetaryDocNumber = snapshot.BudgetaryDocNumber;
            paymentOrder.BudgetaryDocDate = snapshot.BudgetaryDocDate;
            paymentOrder.BudgetaryPaymentType = snapshot.BudgetaryPaymentType;
            paymentOrder.KindOfPay = snapshot.KindOfPay;
            paymentOrder.NumberTop = snapshot.NumberTop;
            paymentOrder.BankDocType = snapshot.BankDocType;*/
            paymentOrder.CodeUin = operation.CodeUin;
            /*var orderDetails = snapshot.Direction == Common.Enums.Enums.Accounting.PaymentDirection.Incoming
                ? snapshot.Payer
                : snapshot.Recipient;
            orderDetails.Okato = operation.Okato;
            orderDetails.Oktmo = operation.Okato;
            if (string.IsNullOrEmpty(paymentOrder.Payer?.Kpp) && !userContext.IsOoo)
            {
                paymentOrder.Payer.Kpp = "0";
            }*/
            return Task.CompletedTask;
        }

        private async Task<OrderDetailsDto> GetFirmDetailsAsync(int? settlementAccountId, IntegrationPartners? integrationPartner)
        {
            var requisites = await requisitesCache.Value.ConfigureAwait(false);
            var settlementAccount = settlementAccountId.HasValue
                ? await GetSettlementAccountAsync(settlementAccountId.Value).ConfigureAwait(false)
                : null;
            var bank = settlementAccount != null
                ? await GetBankAsync(settlementAccount.BankId).ConfigureAwait(false)
                : null;
            var address = await addressApiClient.GetFirmAddressString(userContext.FirmId).ConfigureAwait(false);
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            return new OrderDetailsDto
            {
                Name = contextExtraData.OrganizationName,
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

        private async Task<OrderDetailsDto> GetKontragentDetailsAsync(MoneyTransferOperation operation, IntegrationPartners? integrationPartner)
        {
            var kontragent = await GetKontragentAsync(operation.KontragentId.Value).ConfigureAwait(false);
            if (kontragent == null)
            {
                return new OrderDetailsDto();
            }

            var result = new OrderDetailsDto
            {
                Name = kontragent.GetName(),
                Inn = kontragent.Inn,
                IsOoo = kontragent.Form == KontragentForm.UL
            };

            var kpp = await kontragentKppsClient.GetByKontragentAsync(userContext.FirmId, userContext.UserId, kontragent.Id, operation.Date).ConfigureAwait(false);
            result.Kpp = kpp?.Number ?? string.Empty;

            var settlementAccount = kontragent.SettlementAccounts.FirstOrDefault(x => x.Id == operation.KontragentSettlementAccountId);
            result.SettlementNumber = settlementAccount.Number ?? string.Empty;

            if (settlementAccount != null && settlementAccount.BankId.HasValue)
            {
                var bank = await GetBankAsync(settlementAccount.BankId.Value).ConfigureAwait(false);
                result.BankBik = bank?.Bik ?? string.Empty;
                result.BankName = bank != null ? GetBankName(bank, integrationPartner) : string.Empty;
                result.BankCity = bank?.City ?? string.Empty;
                result.BankCorrespondentAccount = bank?.CorrespondentAccount ?? string.Empty;
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

        private async Task<OrderDetailsDto> GetWorkerDetailsAsync(MoneyTransferOperation operation, IntegrationPartners? integrationPartner)
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
            if (workerCardAccount == null)
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
            var inn = operation.OperationType == MoneyTransferOperationType.PayDays && worker.PaymentMethod == PersonalCardMethod && !string.IsNullOrEmpty(workerCardAccount.InnRecipient)
                ? workerCardAccount.InnRecipient
                : worker.Inn;
            var bank = workerCardAccount.BankId.HasValue
                ? await GetBankAsync(workerCardAccount.BankId.Value).ConfigureAwait(false)
                : null;
            var address = worker.ActualAddress != null
                ? await addressApiClient.GetAddressString(worker.ActualAddress.Value).ConfigureAwait(false)
                : null;
            return new OrderDetailsDto
            {
                Name = name,
                Inn = inn,
                SettlementNumber = workerCardAccount.Number,
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

        private async Task<PurseDto> GetPurseAsync(int id)
        {
            if (pursesCache.TryGetValue(id, out var purse))
            {
                return purse;
            }
            purse = (await purseClient.GetByIdsAsync(userContext.FirmId, userContext.UserId, new[] { id }).ConfigureAwait(false)).FirstOrDefault();
            if (purse != null)
            {
                pursesCache.TryAdd(id, purse);
            }
            return purse;
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

        private async Task<string> GetPurposeCodeAsync(MoneyTransferOperation operation)
        {
            if (operation.OperationType != MoneyTransferOperationType.PayDays)
            {
                return string.Empty;
            }

            var applyDeductionStatus = await chargePaymentsApiClient.GetApplyDeductionStatusAsync(userContext.FirmId, userContext.UserId, operation.DocumentBaseId, operation.Date).ConfigureAwait(false);
            var purposeCode = applyDeductionStatus == ApplyDeductionStatus.None ? string.Empty : ((int)applyDeductionStatus).ToString();
            return purposeCode;
        }
    }

    [InjectAsSingleton]
    public class IntegrationBizPaymentOrderCreatorStateless : IIntegrationBizPaymentOrderCreator
    {
        private readonly IDIResolver diResolver;

        private readonly AsyncLocal<IIntegrationBizPaymentOrderCreator> paymentOrderCreator = new AsyncLocal<IIntegrationBizPaymentOrderCreator>();
        private IIntegrationBizPaymentOrderCreator PaymentOrderCreator => paymentOrderCreator.Value ?? (paymentOrderCreator.Value = diResolver.GetInstance<IIntegrationBizPaymentOrderCreator>());

        public IntegrationBizPaymentOrderCreatorStateless(IDIResolver diResolver)
        {
            this.diResolver = diResolver;
        }

        public Task<PaymentOrderDto> CreateAsync(Guid guid, object order, IntegrationPartners? integrationPartner = null)
        {
            return PaymentOrderCreator.CreateAsync(guid, order, integrationPartner);
        }
    }
}
