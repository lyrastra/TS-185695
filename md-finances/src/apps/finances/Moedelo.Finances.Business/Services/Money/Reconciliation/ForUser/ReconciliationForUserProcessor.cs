using Moedelo.BankIntegrationsV2.Client.BankOperation;
using Moedelo.BankIntegrationsV2.Client.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.BankIntegrationsV2.Dto.DataInformation;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
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
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.Parsers.Klto1CParser.Business;
using Moedelo.Parsers.Klto1CParser.Models.Klto1CParser;
using Moedelo.PayrollV2.Client.SalaryProjects;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using Moedelo.RequisitesV2.Dto.SettlementAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationForUserProcessor : IReconciliationForUserProcessor
    {
        private const string TAG = nameof(ReconciliationForUserProcessor);

        private readonly ILogger logger;
        private readonly IBanksApiClient banksApiClient;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IBankOperationClient integrationBankOperationClient;
        private readonly IBankIntegrationsDataInformationClient integrationsDataInformationClient;
        private readonly IMoneyBalancesReader moneyBalancesReader;
        private readonly IBalanceReconcilationDao balanceReconcilationDao;
        private readonly IReconciliationMovementListReader fileStorage;
        private readonly IReconciliationComparer comparer;
        private readonly IBalanceMasterService balanceMasterService;
        private readonly IClosedPeriodsService closedPeriodsService;
        private readonly ISalaryProjectApiClient salaryProjectApiClient;
        private readonly IReconciliationNotificationSender notificationSender;
        private readonly IReconciliationLimitator limitator;

        public ReconciliationForUserProcessor(
            ILogger logger,
            IBanksApiClient banksApiClient,
            ISettlementAccountClient settlementAccountClient,
            IBankOperationClient integrationBankOperationClient,
            IBankIntegrationsDataInformationClient integrationsDataInformationClient,
            IMoneyBalancesReader moneyBalancesReader,
            IBalanceReconcilationDao balanceReconcilationDao,
            IReconciliationMovementListReader fileStorage,
            IReconciliationComparer comparer,
            IBalanceMasterService balanceMasterService,
            IClosedPeriodsService closedPeriodsService,
            ISalaryProjectApiClient salaryProjectApiClient,
            IReconciliationNotificationSender notificationSender,
            IReconciliationLimitator limitator)
        {
            this.logger = logger;
            this.banksApiClient = banksApiClient;
            this.settlementAccountClient = settlementAccountClient;
            this.integrationBankOperationClient = integrationBankOperationClient;
            this.integrationsDataInformationClient = integrationsDataInformationClient;
            this.moneyBalancesReader = moneyBalancesReader;
            this.balanceReconcilationDao = balanceReconcilationDao;
            this.fileStorage = fileStorage;
            this.comparer = comparer;
            this.balanceMasterService = balanceMasterService;
            this.closedPeriodsService = closedPeriodsService;
            this.salaryProjectApiClient = salaryProjectApiClient;
            this.notificationSender = notificationSender;
            this.limitator = limitator;
        }

        public async Task ProcessAsync(IUserContext userContext, ReconciliationForUserRequest request)
        {
            try
            {
                await ProcessInternalAsync(userContext, request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (request.IsManual)
                {
                    var message = $"Произошла ошибка при проведении сверки с банком по расчётному счету {request.SettlementNumber}";
                    await notificationSender.SendErrorNotificationAsync(userContext.FirmId, userContext.UserId, message).ConfigureAwait(false);
                }
                await balanceReconcilationDao.SetStatusAsync(userContext.FirmId, request.SessionId, ReconciliationStatus.Error).ConfigureAwait(false);
                logger.Error(TAG, "Reconcilation error", ex, userContext.GetAuditContext(), request);
            }
        }

        private async Task ProcessInternalAsync(IUserContext userContext, ReconciliationForUserRequest request)
        {
            if (request.Status == MovementReviseStatus.Error)
            {
                var limits = await limitator.GetLimitsAsync(request).ConfigureAwait(false);
                await limitator.DeleteLimitsAsync(request).ConfigureAwait(false);
                await balanceReconcilationDao.SetStatusAsync(userContext.FirmId, request.SessionId, ReconciliationStatus.Error).ConfigureAwait(false);
                var message = $"Произошла ошибка при запросе данных из банка по расчётному счету {request.SettlementNumber}";
                await notificationSender.SendErrorNotificationAsync(userContext.FirmId, userContext.UserId, message).ConfigureAwait(false);
                logger.Info(TAG, $"Reconcilation error. Limits: CurrentCycleNumber: {limits.CurrentCycleNumber}, ReconciledDate: {limits.ReconciledDate.ToString("d")}", userContext.GetAuditContext(), request);
                return;
            }

            var isCompleted = await IsCurrentBalanceReconcilationCompletedAsync(userContext, request).ConfigureAwait(false);
            if (isCompleted)
            {
                var limits = await limitator.GetLimitsAsync(request).ConfigureAwait(false);
                await limitator.DeleteLimitsAsync(request).ConfigureAwait(false);
                logger.Info(TAG, $"ALARM Reconcilation is completed. Limits: CurrentCycleNumber: {limits.CurrentCycleNumber}, ReconciledDate: {limits.ReconciledDate.ToString("d")}", userContext.GetAuditContext(), request);
                return;
            }

            var settlementAccount = await GetSettlementAccountAsync(userContext, request).ConfigureAwait(false);
            if (settlementAccount == null)
            {
                return;
            }

            var movementList = await WriteReconcilationInStorageAsync(userContext, request, settlementAccount).ConfigureAwait(false);

            var isBalancesEquality = await CheckBalancesEqualityAsync(userContext, request, settlementAccount, movementList).ConfigureAwait(false);
            if (isBalancesEquality)
            {
                return;
            }
            
            await TryDoNextRequestMovementAsync(userContext, request, settlementAccount, CancellationToken.None)
                .ConfigureAwait(false);
        }

        private async Task TryDoNextRequestMovementAsync(IUserContext userContext,
            ReconciliationForUserRequest request,
            SettlementAccountDto settlementAccount,
            CancellationToken ctx)
        {
            var settlementAccountStatus = await GetSettlementAccountStatus(userContext, settlementAccount).ConfigureAwait(false);
            if (settlementAccountStatus == null)
            {
                logger.Error(TAG, $"Could not get settlement account status", extraData: new { settlementAccount.Number, userContext.FirmId });
                return;
            }

            var startDate = request.StartDate.AddDays(-30);
            var maxReconcilationDate = await GetMaxReconcilationDateAsync(userContext, settlementAccountStatus, ctx)
                .ConfigureAwait(false);
            var finishDate = new DateTime(maxReconcilationDate.Year, maxReconcilationDate.Month, maxReconcilationDate.Day);
            if (startDate > maxReconcilationDate)
            {
                maxReconcilationDate = startDate;
            }
            var endDate = request.StartDate.AddDays(-1);

            if (maxReconcilationDate < endDate)
            {
                var newRequest = new ReconciliationForUserRequest { StartDate = maxReconcilationDate, SessionId = request.SessionId };

                (var checkLimits, var limits) = await limitator.CheckLimitsAsync(newRequest).ConfigureAwait(false);

                if (checkLimits == null)
                {
                    logger.Info(TAG, $"Period already reconciled for Guid : {request.SessionId}. Limits: CurrentCycleNumber: {limits.CurrentCycleNumber}, ReconciledDate: {limits.ReconciledDate.ToString("d")}", userContext.GetAuditContext(), new { Request = request, MaxReconcilationDate = finishDate, StartDate = startDate, RequestMovementStartDate = maxReconcilationDate, RequestMovementEndDate = endDate });
                    return;
                }
                if (checkLimits == false)
                {
                    throw new Exception($"Number of reconciliation сycle exceeded for Guid : {request.SessionId}. Limits: MaxCycleNumber: {limitator.GetMaxCycleNumber()}, CurrentCycleNumber: {limits.CurrentCycleNumber}, ReconciledDate: {limits.ReconciledDate.ToString("d")}");
                }

                await RequestMovementListAsync(userContext, settlementAccountStatus, maxReconcilationDate, endDate, request.SessionId, isManual: request.IsManual).ConfigureAwait(false);
                
                var newlimits = await limitator.SetLimitsAsync(newRequest).ConfigureAwait(false);
                logger.Info(TAG, $"New cycle of reconcilation. Limits: CurrentCycleNumber: {newlimits.CurrentCycleNumber}, ReconciledDate: {newlimits.ReconciledDate.ToString("d")}", userContext.GetAuditContext(), new { Request = request, MaxReconcilationDate = finishDate, StartDate = startDate, RequestMovementStartDate = maxReconcilationDate, RequestMovementEndDate = endDate, settlementAccountStatus.IntegrationPartner });
                return;
            }
            var endlimits = await limitator.GetLimitsAsync(request).ConfigureAwait(false);
            await limitator.DeleteLimitsAsync(request).ConfigureAwait(false);
            logger.Info(TAG, $"End cycle of reconcilation. Limits: CurrentCycleNumber: {endlimits.CurrentCycleNumber}, ReconciledDate: {endlimits.ReconciledDate.ToString("d")}", userContext.GetAuditContext(), new { Request = request, MaxReconcilationDate = finishDate, StartDate = startDate, NoRequestMovementStartDate = maxReconcilationDate, NoRequestMovementEndDate = endDate, settlementAccountStatus.IntegrationPartner });
            await ReconciliationNoDiff(userContext, request).ConfigureAwait(false);
        }

        private async Task<bool> CheckBalancesEqualityAsync(IUserContext userContext, ReconciliationForUserRequest request, SettlementAccountDto settlementAccount, MovementList movementList)
        {
            var balances = await moneyBalancesReader.GetAsync(userContext, new[]
                {
                    new MoneySourceBase
                    {
                        Id = settlementAccount.Id,
                        Type = MoneySourceType.SettlementAccount,
                        SubcontoId = settlementAccount.SubcontoId,
                        IsPrimary = settlementAccount.IsPrimary
                    }
                }, movementList.StartDate.AddDays(-1))
                .ConfigureAwait(false);

            if (request.Status != MovementReviseStatus.Empty && movementList.StartBalance == balances[0].Balance)
            {
                var limits = await limitator.GetLimitsAsync(request).ConfigureAwait(false); 
                logger.Info(TAG, $"Equality balances. End reconcilation.  movementList.StartBalance = {movementList.StartBalance}, balances[0].Balance = {balances[0].Balance} Limits: CurrentCycleNumber: {limits.CurrentCycleNumber}, ReconciledDate: {limits.ReconciledDate}", userContext.GetAuditContext(), request);
                await ReconciliationNoDiff(userContext, request).ConfigureAwait(false);
                return true;
            }

            return false;
        }

        private async Task<MovementList> WriteReconcilationInStorageAsync(IUserContext userContext, ReconciliationForUserRequest request, SettlementAccountDto settlementAccount)
        {
            var salaryProjectSettlementAccountIds = await salaryProjectApiClient.GetSalaryProjectSettlementAccountIds(userContext.FirmId, userContext.UserId).ConfigureAwait(false);
            var isSalaryProject = salaryProjectSettlementAccountIds.Contains(settlementAccount.Id);

            var movementList = new MovementList();
            if (request.Status != MovementReviseStatus.Empty)
            {
                var fileText = await fileStorage.GetAsync(request.FileId).ConfigureAwait(false);
                if (request.Status == MovementReviseStatus.Success)
                {
                    await CompareWithFileAsync(userContext, fileText, request, isSalaryProject).ConfigureAwait(false);
                }
                movementList = Klto1CParser.ParseCommonSection(fileText);
                movementList.Balances = Klto1CParser.ParseBalancesSections(fileText);

                if (movementList.StartDate == DateTime.MinValue)
                {
                    movementList.StartDate = request.StartDate;
                    movementList.EndDate = request.EndDate;
                }
            }
            else
            {
                await CompareWithSettlementNumberAsync(userContext, settlementAccount.Number, request, isSalaryProject).ConfigureAwait(false);
                movementList.StartDate = request.StartDate;
                movementList.EndDate = request.EndDate;
            }
            return movementList;
        }

        private async Task<SettlementAccountDto> GetSettlementAccountAsync(IUserContext userContext, ReconciliationForUserRequest request)
        {
            var settlementAccounts = await settlementAccountClient.GetByNumbersAsync(userContext.FirmId, userContext.UserId, new[] { request.SettlementNumber }).ConfigureAwait(false);
            if (settlementAccounts.Count > 1 || settlementAccounts.Count == 0)
            {
                if (request.IsManual)
                {
                    var message = string.Empty;
                    if (settlementAccounts.Count > 1)
                    {
                        message = $"Найдено несколько расчётных счетов с номером {request.SettlementNumber}";
                    }
                    else if (settlementAccounts.Count == 0)
                    {
                        message = $"Не найден расчётный счет {request.SettlementNumber}";
                    }
                    await notificationSender.SendErrorNotificationAsync(userContext.FirmId, userContext.UserId, message).ConfigureAwait(false);
                }

                await balanceReconcilationDao.SetStatusAsync(userContext.FirmId, request.SessionId, ReconciliationStatus.Error).ConfigureAwait(false);
                logger.Info(TAG, "Reconcilation error. SettlementAccounts more than one or not exist.", userContext.GetAuditContext(), request);
                return null;
            }
            return settlementAccounts[0];
        }

        private async Task ReconciliationNoDiff(IUserContext userContext, ReconciliationForUserRequest request)
        {
            await balanceReconcilationDao.SetStatusAsync(userContext.FirmId, request.SessionId, ReconciliationStatus.Ready).ConfigureAwait(false);
            if (request.IsManual)
            {
                var reconciliation = await balanceReconcilationDao.GetBySessionIdAsync(userContext.FirmId, request.SessionId).ConfigureAwait(false);
                if (reconciliation != null && reconciliation.ExcessOperations.Count == 0 && reconciliation.MissingOperations.Count == 0)
                {
                    await notificationSender.SendNoDiffNotificationAsync(userContext.FirmId, userContext.UserId).ConfigureAwait(false);
                }
                else
                {
                    await notificationSender.SendSuccesNotificationAsync(userContext.FirmId, userContext.UserId, request.SessionId).ConfigureAwait(false);
                }
            }
            var limits = await limitator.GetLimitsAsync(request).ConfigureAwait(false);
            await limitator.DeleteLimitsAsync(request).ConfigureAwait(false);
            logger.Info(TAG, $"Reconcilation ready. Limits: CurrentCycleNumber: {limits.CurrentCycleNumber}, ReconciledDate: {limits.ReconciledDate.ToString("d")}", userContext.GetAuditContext(), request);
        }

        private async Task<DateTime> GetMaxReconcilationDateAsync(IUserContext userContext,
            SettlementAccountStatusDto settlementAccountStatus,
            CancellationToken ctx)
        {
            var today = DateTime.Today;
            var maxDate = new DateTime(today.Year - 1, 1, 1);
            if(settlementAccountStatus.IntegrationPartner == BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners.PointBank)
            {
                //https://youtrack.moedelo.org/youtrack/issue/AD-2026/Avtosverka-dlya-Tochki-Banka
                // Банк нахимичил с биками что сломало сверку раньше февраля 2024
                maxDate = new DateTime(2024, 2, 1);
            }
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);
            if (contextExtraData.FirmRegistrationDate.HasValue && maxDate < contextExtraData.FirmRegistrationDate.Value)
            {
                maxDate = contextExtraData.FirmRegistrationDate.Value;
            }

            var balanceMaster = await balanceMasterService.GetStatusAsync(userContext, ctx).ConfigureAwait(false);
            if (balanceMaster.IsCompleted && maxDate < balanceMaster.Date)
            {
                maxDate = balanceMaster.Date;
            }

            var lastClosedPeriodDate = await closedPeriodsService
                .GetLastClosedDateAsync(userContext, ctx)
                .ConfigureAwait(false);

            if (maxDate < lastClosedPeriodDate)
            {
                maxDate = lastClosedPeriodDate;
            }

            return maxDate;
        }

        private async Task CompareWithFileAsync(IUserContext userContext, string fileText, ReconciliationForUserRequest request, bool isSalaryProject)
        {
            var compareResult = await comparer.CompareWithBankStatementAsync(userContext, fileText, request.StartDate, request.EndDate).ConfigureAwait(false);
            var reconcilationResult = await balanceReconcilationDao.GetBySessionIdAsync(userContext.FirmId, request.SessionId).ConfigureAwait(false);
            MapOperatons(compareResult, reconcilationResult, isSalaryProject);
            await balanceReconcilationDao.UpdateAsync(userContext.FirmId, reconcilationResult).ConfigureAwait(false);
        }

        private async Task CompareWithSettlementNumberAsync(IUserContext userContext, string settlementAccountNumber, ReconciliationForUserRequest request, bool isSalaryProject)
        {
            var compareResult = await comparer.CompareWithEmptyBankStatementAsync(userContext, settlementAccountNumber, request.StartDate, request.EndDate).ConfigureAwait(false);
            var reconcilationResult = await balanceReconcilationDao.GetBySessionIdAsync(userContext.FirmId, request.SessionId).ConfigureAwait(false);
            MapOperatons(compareResult, reconcilationResult, isSalaryProject);
            await balanceReconcilationDao.UpdateAsync(userContext.FirmId, reconcilationResult).ConfigureAwait(false);
        }

        private static void MapOperatons(ReconciliationCompareResult compareResult, ReconciliationResult reconsilationResult, bool isSalaryProject)
        {
            reconsilationResult.ExcessOperations.AddRange(compareResult.ExcessOperations);
            reconsilationResult.MissingOperations.AddRange(compareResult.MissingOperations);
            var salaryOperations = reconsilationResult.ExcessOperations.Concat(reconsilationResult.MissingOperations)
                .Where(x => x.IsSalary && string.IsNullOrEmpty(x.KontragentName));
            foreach (var operation in salaryOperations)
            {
                operation.KontragentName = isSalaryProject
                    ? "Зарплатный проект"
                    : "Выплата физ. лицам";
            }
        }

        private async Task RequestMovementListAsync(IUserContext userContext, SettlementAccountStatusDto settlementAccountStatus, DateTime startDate, DateTime endDate, Guid? guid = null, bool isManual = false)
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
            var response = await integrationBankOperationClient.RequestMovementListAsync(request).ConfigureAwait(false);
            if (!response.IsSuccess)
            {
                logger.Error(TAG, $"Failed to send request for movement list", extraData: request);
            }
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
                        BankFullName = bank.FullName,
                        Bik = bank.Bik,
                        SettlementNumber = settlementAccount.Number
                    }
                }
            }).ConfigureAwait(false);
            return response.IsSuccess
                ? response.Result.FirstOrDefault()
                : null;
        }

        private async Task<bool> IsCurrentBalanceReconcilationCompletedAsync(IUserContext userContext, ReconciliationForUserRequest request)
        {
            var result = true;
            var reconcilation = await balanceReconcilationDao.GetBySessionIdAsync(userContext.FirmId, request.SessionId).ConfigureAwait(false);
            if (reconcilation?.Status == ReconciliationStatus.InProgress)
            {
                result = false;
            }
            return result;
        }
    }
}
