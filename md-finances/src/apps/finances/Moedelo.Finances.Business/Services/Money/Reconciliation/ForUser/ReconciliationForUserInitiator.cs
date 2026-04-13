using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using Moedelo.BankIntegrationsV2.Client.BankOperation;
using Moedelo.BankIntegrationsV2.Client.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.BillingV2.Client.BillingApi;
using Moedelo.CatalogV2.Client.Banks;
using Moedelo.CatalogV2.Dto.Banks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.BalanceMaster;
using Moedelo.Finances.Domain.Interfaces.Business.ClosedPeriods;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Reconcilation;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationForUserInitiator : IReconciliationForUserInitiator
    {
        private const string TAG = nameof(ReconciliationForUserInitiator);

        private readonly ILogger logger;
        private readonly IBanksApiClient banksApiClient;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IBankOperationClient integrationBankOperationClient;
        private readonly IBankIntegrationsDataInformationClient integrationsDataInformationClient;
        private readonly IMoneyBalancesReader moneyBalancesReader;
        private readonly IBalanceReconcilationDao balanceReconcilationDao;
        private readonly IBalanceMasterService balanceMasterService;
        private readonly IClosedPeriodsService closedPeriodsService;
        private readonly IReconciliationNotificationSender notificationSender;
        private readonly IBillingApiClient billingApiClient;
        private readonly IReconciliationLimitator limitator;


        public ReconciliationForUserInitiator(
            ILogger logger,
            IBanksApiClient banksApiClient,
            ISettlementAccountClient settlementAccountClient,
            IBankOperationClient integrationBankOperationClient,
            IBankIntegrationsDataInformationClient integrationsDataInformationClient,
            IMoneyBalancesReader moneyBalancesReader,
            IBalanceReconcilationDao balanceReconcilationDao,
            IBalanceMasterService balanceMasterService,
            IClosedPeriodsService closedPeriodsService,
            IReconciliationNotificationSender notificationSender,
            IBillingApiClient billingApiClient,
            IReconciliationLimitator limitator)
        {
            this.logger = logger;
            this.banksApiClient = banksApiClient;
            this.settlementAccountClient = settlementAccountClient;
            this.integrationBankOperationClient = integrationBankOperationClient;
            this.integrationsDataInformationClient = integrationsDataInformationClient;
            this.moneyBalancesReader = moneyBalancesReader;
            this.balanceReconcilationDao = balanceReconcilationDao;
            this.balanceMasterService = balanceMasterService;
            this.closedPeriodsService = closedPeriodsService;
            this.notificationSender = notificationSender;
            this.billingApiClient = billingApiClient;
            this.limitator = limitator;
        }

        public async Task<bool> InitiateAsync(IUserContext userContext, int settlementAccountId, DateTime onDate)
        {
            var rules = await userContext.GetUserRulesAsync().ConfigureAwait(false);
            var currentPayment = await billingApiClient.GetCurrentPaymentWithTrialAsync(userContext.FirmId).ConfigureAwait(false);
            if (currentPayment == null)
            {
                logger.Info(TAG, $"Could not start reconcilation for account without payment or trial", context: userContext.GetAuditContext());
                return false;
            }

            var balance = new MoneySourceBalance
            {
                Id = settlementAccountId,
                Type = MoneySourceType.SettlementAccount,
                Balance = 0
            };
            return await ReconcileAsync(userContext, balance, onDate, true).ConfigureAwait(false);
        }

        public async Task<bool> InitiateAsync(IUserContext userContext, IReadOnlyCollection<MoneySourceBalance> balances, DateTime onDate)
        {
            var currentPayment = await billingApiClient.GetCurrentPaymentWithTrialAsync(userContext.FirmId).ConfigureAwait(false);
            if (currentPayment == null || currentPayment.IsTrial)
            {
                logger.Info(TAG, $"Could not start reconcilation for account without payment or trial", context: userContext.GetAuditContext());
                return false;
            }

            var settlementAccountBalances = balances.Where(x => x.Type == MoneySourceType.SettlementAccount);
            var tasks = settlementAccountBalances.Select(x => ReconcileAsync(userContext, x, onDate, false));
            var result = await Task.WhenAll(tasks).ConfigureAwait(false);
            return result[0];
        }

        private async Task<bool> ReconcileAsync(IUserContext userContext, MoneySourceBalance balance, DateTime onDate, bool isManual)
        {
            var settlementAccount = await settlementAccountClient.GetByIdAsync(userContext.FirmId, userContext.UserId, (int)balance.Id).ConfigureAwait(false);
            if (settlementAccount == null)
            {
                logger.Error(TAG, $"Settlement account with id = {settlementAccount.Id} is not found", context: userContext.GetAuditContext());
                return false;
            }

            var settlementAccountStatus = await GetSettlementAccountStatus(userContext, settlementAccount).ConfigureAwait(false);
            if (settlementAccountStatus == null)
            {
                logger.Error(TAG, $"Could not get settlement account status", extraData: new { settlementAccount.Number }, context: userContext.GetAuditContext());
                return false;
            }
            if (settlementAccountStatus.IntegrationPartner == IntegrationPartners.Vtb24Bank)
            {
                logger.Info(TAG, $"Could not start reconcilation for settlement account with id = {settlementAccount.Id} because it's Vtb24", extraData: settlementAccountStatus, context: userContext.GetAuditContext());
                return false;
            }

            var isReconcilationInProgressExists = await balanceReconcilationDao.IsAnyInProgressAsync(userContext.FirmId, settlementAccount.Id).ConfigureAwait(false);
            if (isReconcilationInProgressExists && isManual == false)
            {
                logger.Info(TAG, $"Already exists reconciliation in progress for settlement account with id = {settlementAccount.Id}", userContext.GetAuditContext());
                return false;
            }

            var serviceBalance = await GetServiceBalanceAsync(userContext, settlementAccount, onDate).ConfigureAwait(false);
            if (serviceBalance == null)
            {
                logger.Info(TAG, $"Could not get balance by settlement account with id = {settlementAccount.Id}", userContext.GetAuditContext());
                return false;
            }

            if (serviceBalance == balance.Balance && isManual == false)
            {
                logger.Info(TAG, $"Balance by settlement account with id = {settlementAccount.Id} is actual", userContext.GetAuditContext());
                return false;
            }

            var maxCreateDate = await balanceReconcilationDao.GetMaxCreateDateAsync(userContext.FirmId, settlementAccount.Id).ConfigureAwait(false) ?? DateTime.MinValue;
            logger.Info(TAG, $"Check last reconcilation date {maxCreateDate:dd.MM.yyyy}", userContext.GetAuditContext());
            if (maxCreateDate >= DateTime.Now.AddDays(-7) && isManual == false)
            {
                logger.Info(TAG, $"Less than 7 days passed since the last reconcilation with settlement account id = {settlementAccount.Id}. Last reconcilation date {maxCreateDate:dd.MM.yyyy}", userContext.GetAuditContext());
                return false;
            }

            var startDate = await GetStartDateAsync(userContext, onDate, settlementAccountStatus, CancellationToken.None)
                .ConfigureAwait(false);

            if (startDate >= onDate && isManual == false)
            {
                logger.Info(TAG, $"Reconciliation by settlement account with id = {settlementAccount.Id} is not available onDate {onDate:dd.MM.yyyy}", userContext.GetAuditContext());
                return false;
            }

            var sessionId = Guid.NewGuid();
            var reconciliation = new BalanceReconcilation
            {
                ServiceBalance = serviceBalance.Value,
                BankBalance = balance.Balance,
                ReconcilationDate = onDate,
                CreateDate = DateTime.Now,
                SessionId = sessionId,
                Status = ReconciliationStatus.InProgress,
                SettlementAccountId = settlementAccount.Id
            };

            if (startDate >= onDate && isManual)
            {
                reconciliation.Status = ReconciliationStatus.Error;
                await balanceReconcilationDao.InsertAsync(userContext.FirmId, reconciliation).ConfigureAwait(false);
                var message = $"—верка на дату {onDate:dd.MM.yyyy} попадает в закрытый период.";
                await notificationSender.SendErrorNotificationAsync(userContext.FirmId, userContext.UserId, message).ConfigureAwait(false);
                return false;
            }

            await balanceReconcilationDao.InsertAsync(userContext.FirmId, reconciliation).ConfigureAwait(false);
            await balanceReconcilationDao.SetReadyToCompleteAsync(userContext.FirmId, settlementAccount.Id).ConfigureAwait(false);
            return await RequestMovementListAsync(userContext, settlementAccountStatus, startDate, onDate, sessionId, isManual).ConfigureAwait(false);
        }

        private async Task<DateTime> GetStartDateAsync(IUserContext userContext, DateTime onDate,
            SettlementAccountStatusDto settlementAccountStatus, CancellationToken ctx)
        {
            var startDate = onDate.AddDays(-30);

            var minDate = new DateTime(2017, 1, 1);
            if (settlementAccountStatus.IntegrationPartner == IntegrationPartners.PointBank)
            {
                //https://youtrack.moedelo.org/youtrack/issue/AD-2026/Avtosverka-dlya-Tochki-Banka
                // Ѕанк нахимичил с биками что сломало сверку раньше феврал¤ 2024
                minDate = new DateTime(2024, 2, 1);
            }
            if (startDate < minDate)
            {
                startDate = minDate;
            }

            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            if (contextExtraData.FirmRegistrationDate.HasValue && startDate < contextExtraData.FirmRegistrationDate.Value)
            {
                startDate = contextExtraData.FirmRegistrationDate.Value;
            }

            var balanceMaster = await balanceMasterService.GetStatusAsync(userContext, ctx).ConfigureAwait(false);
            if (balanceMaster.IsCompleted && startDate < balanceMaster.Date)
            {
                startDate = balanceMaster.Date;
            }

            var lastClosedPeriodDate = await closedPeriodsService
                .GetLastClosedDateAsync(userContext, ctx)
                .ConfigureAwait(false);

            if (startDate < lastClosedPeriodDate)
            {
                startDate = lastClosedPeriodDate;
            }

            return startDate;
        }

        private async Task<decimal?> GetServiceBalanceAsync(IUserContext userContext, SettlementAccountDto settlementAccount, DateTime onDate)
        {
            var moneySources = new[]
            {
                new MoneySourceBase
                {
                    Id = settlementAccount.Id,
                    SubcontoId = settlementAccount.SubcontoId,
                    IsPrimary = settlementAccount.IsPrimary,
                    Type = MoneySourceType.SettlementAccount
                }
            };
            var balances = await moneyBalancesReader.GetAsync(userContext, moneySources, onDate).ConfigureAwait(false);
            return balances.FirstOrDefault()?.Balance;
        }

        private async Task<bool> RequestMovementListAsync(IUserContext userContext, SettlementAccountStatusDto settlementAccountStatus, DateTime startDate, DateTime endDate, Guid? guid = null, bool isManual = false)
        {
            var isAccountingTask = userContext.HasAllRuleAsync(AccessRule.UsnAccountantTariff);
            var contextExtraDataTask = userContext.GetContextExtraDataAsync();
            await Task.WhenAll(isAccountingTask, contextExtraDataTask).ConfigureAwait(false);

            var identityDto = new IntegrationIdentityDto
            {
                FirmId = userContext.FirmId,
                Inn = contextExtraDataTask.Result.Inn,
                SettlementNumber = settlementAccountStatus.SettlementNumber,
                Bik = settlementAccountStatus.Bik,
                IntegrationPartner = settlementAccountStatus.IntegrationPartner
            };
            var request = new RequestMovementListRequestDto
            {
                Guid = guid ?? Guid.NewGuid(),
                CallType = IntegrationCallType.ReviseForUser,
                BeginDate = startDate,
                EndDate = endDate,
                IsManual = isManual,
                IsAccounting = isAccountingTask.Result,
                IdentityDto = identityDto
            };
            var newRequest = new ReconciliationForUserRequest 
            { 
                SessionId = request.Guid, 
                StartDate = request.BeginDate, 
                EndDate = request.EndDate 
            };

            (var checkLimits, var limits) = await limitator.CheckLimitsAsync(newRequest).ConfigureAwait(false);
            if (checkLimits == null)
            {
                logger.Error(TAG, $"Period already reconciled for Guid : {request.Guid} Limits: CurrentCycleNumber: {limits.CurrentCycleNumber}, ReconciledDate: {limits.ReconciledDate.ToString("d")} IntegrationPartner: {settlementAccountStatus.IntegrationPartner}", extraData: request);
                return false;
            }
            if (checkLimits == false)
            {
                logger.Error(TAG, $"Number of reconciliation cycle exceeded for Guid : {request.Guid} Limits: MaxCycleNumber: {limitator.GetMaxCycleNumber()}, CurrentCycleNumber: {limits.CurrentCycleNumber}, ReconciledDate: {limits.ReconciledDate.ToString("d")} IntegrationPartner: {settlementAccountStatus.IntegrationPartner}", extraData: request);
                return false;
            }


            var response = await integrationBankOperationClient.RequestMovementListAsync(request).ConfigureAwait(false);
            if (!response.IsSuccess)
            {
                logger.Error(TAG, $"Failed to send request for movement list IntegrationPartner: {settlementAccountStatus.IntegrationPartner}", extraData: request);
            }
            else
            {
                var newLimits = await limitator.SetLimitsAsync(newRequest).ConfigureAwait(false);
                logger.Info(TAG, $"First cycle of reconciliation started. Limits: CurrentCycleNumber: {newLimits.CurrentCycleNumber}, ReconciledDate: {newLimits.ReconciledDate.ToString("d")}, IntegrationPartner: {settlementAccountStatus.IntegrationPartner}", extraData: request);

            }
            return response.IsSuccess;
        }

        private async Task<BankDto> GetBankAsync(int bankId)
        {
            var banks = await banksApiClient.GetByIdsAsync(new[] { bankId }).ConfigureAwait(false);
            return banks.FirstOrDefault();
        }

        private async Task<SettlementAccountStatusDto> GetSettlementAccountStatus(IUserContext userContext, SettlementAccountDto settlementAccount)
        {
            var bank = await GetBankAsync(settlementAccount.BankId).ConfigureAwait(false);
            if (bank == null)
            {
                logger.Error(TAG, $"Bank with id = {settlementAccount.BankId} is not found");
                return null;
            }

            var response = await integrationsDataInformationClient.GetIntSummaryBySettlementsAsync(new IntSummaryBySettlementsRequestDto
            {
                FirmId = userContext.FirmId,
                UserId = userContext.UserId,
                Settlements = new List<SettlementAccountV2Dto>
                {
                    new SettlementAccountV2Dto
                    {
                        BankFullName = bank.Name,
                        Bik = bank.Bik,
                        SettlementNumber = settlementAccount.Number
                    }
                }
            }).ConfigureAwait(false);
            return response.IsSuccess
                ? response.Result.FirstOrDefault()
                : null;
        }
    }
}
