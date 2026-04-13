using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Balances;
using Moedelo.Accounting.Enums;
using Moedelo.Accounts.Abstractions.Dto;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.ApiClient.Abstractions.Money;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;
using Moedelo.Money.Reports.Business.Abstractions.BankAndServiceBalanceReport;
using Moedelo.Money.Reports.Business.BankAndServiceBalanceReport.Models;
using Moedelo.Money.Reports.Business.Extensions;
using Moedelo.Money.Reports.DataAccess.Abstractions.Balances;
using Moedelo.Money.Reports.DataAccess.Abstractions.Balances.Models;
using Moedelo.Money.Reports.DataAccess.Abstractions.Reconciliation;
using Moedelo.Money.Reports.Domain.BankAndServiceBalanceReport;
using Moedelo.Money.Reports.Domain.Banks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.Reports.Business.BankAndServiceBalanceReport
{
    [InjectAsSingleton(typeof(IBankAndServiceBalanceReportReader))]
    internal class BankAndServiceBalanceReportReader : IBankAndServiceBalanceReportReader
    {
        private static readonly SyntheticAccountCode[] AccountCodes =
        {
            SyntheticAccountCode._50_01,
            SyntheticAccountCode._50_02,
            SyntheticAccountCode._51_01,
            SyntheticAccountCode._76_07,
            SyntheticAccountCode._52_01_01,
            SyntheticAccountCode._52_01_02
        };

        private readonly LastFirmPaymentGetter lastFirmsPaymentGetter;
        private readonly IFirmRequisitesApiClient firmRequisitesApiClient;
        private readonly IUserClient userClient;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly BankNamesReader bankNamesReader;
        private readonly IBankBalanceHistoryApiClient bankBalanceHistoryApiClient;
        private readonly IBalancesApiClient serviceRemainsBalancesApiClient;
        private readonly IBalancesDao serviceOperationsBalancesDao;
        private readonly IReconciliationDao reconciliationDao;

        public BankAndServiceBalanceReportReader(
            LastFirmPaymentGetter lastFirmsPaymentGetter,
            IFirmRequisitesApiClient firmRequisitesApiClient,
            IUserClient userClient,
            ISettlementAccountApiClient settlementAccountApiClient,
            BankNamesReader bankNamesReader,
            IBankBalanceHistoryApiClient bankBalanceHistoryApiClient,
            IBalancesApiClient serviceRemainsBalancesApiClient,
            IBalancesDao serviceOperationsBalancesDao,
            IReconciliationDao reconciliationDao)
        {
            this.lastFirmsPaymentGetter = lastFirmsPaymentGetter;
            this.firmRequisitesApiClient = firmRequisitesApiClient;
            this.userClient = userClient;
            this.serviceOperationsBalancesDao = serviceOperationsBalancesDao;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.bankNamesReader = bankNamesReader;
            this.bankBalanceHistoryApiClient = bankBalanceHistoryApiClient;
            this.serviceRemainsBalancesApiClient = serviceRemainsBalancesApiClient;
            this.reconciliationDao = reconciliationDao;
        }

        public async Task<IReadOnlyCollection<BankAndServiceBalanceReportRow>> ReadAsync(DateTime onDate)
        {
            var lastFirmPayments = await lastFirmsPaymentGetter.GetAsync(onDate);
            var firmIds = lastFirmPayments.Select(x => x.FirmId).ToArray();
            // получаем FirmInfo только по фирмам is_internal = 0
            var firmInfoById = await GetFirmInfosAsync(firmIds);

            firmIds = firmInfoById.Values
                .Select(x => x.Id)
                .ToArray();
            // оставляем только платежи по фирмам is_internal = 0
            lastFirmPayments = lastFirmPayments.Where(x => firmInfoById.ContainsKey(x.FirmId)).ToArray();

            var bankBalancesByFirmId = await GetBankBalancesAsync(firmIds, onDate);
            var userInfoByFirmId = await GetUserInfosAsync(firmIds);
            var settlementAccountByFirmId = await settlementAccountApiClient.GetByFirmIdsAsync(firmIds);
            var serviceRemainsBalancesByFirmId = await GetServiceRemainsBalancesAsync(firmIds);
            var serviceOperationBalancesByFirmId = await GetServiceOperationsBalancesAsync(firmIds, serviceRemainsBalancesByFirmId, onDate);
            var reconciliationBySettlementAccountId = await GetReconciliationDataAsync(firmIds);

            var bankIds = settlementAccountByFirmId.Values
                .SelectMany(x => x)
                .Select(x => x.BankId)
                .Distinct()
                .ToArray();
            var bankNameById = await bankNamesReader.GetByIdsAsync(bankIds);

            return lastFirmPayments
                .Select(lastFirmPayment => 
                    MapRowsByPayment(
                        lastFirmPayment,
                        bankBalancesByFirmId,
                        firmInfoById,
                        userInfoByFirmId,
                        settlementAccountByFirmId,
                        bankNameById,
                        serviceRemainsBalancesByFirmId,
                        serviceOperationBalancesByFirmId,
                        reconciliationBySettlementAccountId))
                .SelectMany(x => x)
                .ToArray();
        }

        private async Task<IReadOnlyDictionary<int, LastBankBalanceResponseDto[]>> GetBankBalancesAsync(IReadOnlyCollection<int> firmIds, DateTime onDate)
        {
            // по задаче, при получении балансов берём период 11 дней
            var minDate = onDate.AddDays(-11);

            var result = new Dictionary<int, LastBankBalanceResponseDto[]>();

            var firmIdsGroups = DividerIntoGroups.Divide(firmIds, 1000);
            foreach (var firmIdsGroup in firmIdsGroups)
            {
                var bankBalancesByFirmId = await bankBalanceHistoryApiClient.OnDateByFirms(new BankBalancesOnDateByFirmsRequestDto
                {
                    FirmIds = firmIdsGroup,
                    OnDate = onDate,
                    MinDate = minDate
                });
                
                foreach (var (firmId, bankBalances) in bankBalancesByFirmId)
                {
                    result.Add(firmId, bankBalances);
                }
            }

            return result;
        }

        private async Task<IReadOnlyDictionary<int, FirmShortInfoDto>> GetFirmInfosAsync(IReadOnlyCollection<int> firmIds)
        {
            var firmInfos = await firmRequisitesApiClient.GetFirmShortInfosAsync(firmIds);

            return firmInfos
                .Where(x => !x.IsInternal)
                .ToDictionary(x => x.Id);
        }

        private async Task<IReadOnlyDictionary<int, BaseUserInfoDto>> GetUserInfosAsync(IReadOnlyCollection<int> firmIds)
        {
            var userInfos = await userClient.GetUserInfoListByFirmIdAsync(firmIds);

            return userInfos.ToDictionary(x => x.FirmId);
        }

        private async Task<IReadOnlyDictionary<int, AccountBalanceDto[]>> GetServiceRemainsBalancesAsync(IReadOnlyCollection<int> firmIds)
        {
            var remainsBalancesByFirmId = await serviceRemainsBalancesApiClient.GetBalancesByFirmIdsAndAccountCodesAsync(new GetBalanceByFirmIdsRequestDto
            {
                FirmIds = firmIds,
                SyntheticAccountCodes = AccountCodes
            });

            return remainsBalancesByFirmId;
        }

        private async Task<IReadOnlyDictionary<int, SettlementAccountBalanceResponse[]>> GetServiceOperationsBalancesAsync(
            IReadOnlyCollection<int> firmIds,
            IReadOnlyDictionary<int, AccountBalanceDto[]> serviceRemainsBalancesByFirmId, DateTime onDate)
        {
            var result = new List<SettlementAccountBalanceResponse>();

            var firmIdsGroups = DividerIntoGroups.Divide(firmIds, 1_000);
            foreach (var firmIdsGroup in firmIdsGroups)
            {
                var firmInitDates = firmIdsGroup.Select(firmId => new FirmInitDate
                {
                    FirmId = firmId,
                    InitDate = serviceRemainsBalancesByFirmId.GetValueOrDefault(firmId)?.First().Date ?? DateTime.MinValue
                }).ToArray();

                var request = new SettlementAccountBalancesRequest
                {
                    FirmInitDates = firmInitDates,
                    OnDate = onDate
                };

                var serviceBalances = await serviceOperationsBalancesDao.GetAsync(request);

                result.AddRange(serviceBalances);
            }

            // группируем по FirmId а не по SettlementAccountId, т.к. в базе есть битые данные,
            // когда операции одному и тому счёту есть в разных фирмах
            return result
                .GroupBy(x => x.FirmId)
                .Select(x => x.ToArray())
                .ToDictionary(x => x.First().FirmId);
        }

        private async Task<IReadOnlyDictionary<int, SettlementAccountReconciliation>> GetReconciliationDataAsync(IReadOnlyCollection<int> firmIds)
        {
            var settlementAccountReconciliations = await reconciliationDao.GetSettlementAccountReconciliationAsync(firmIds);

            return settlementAccountReconciliations
                .ToDictionary(x => x.SettlementAccountId);
        }

        private IReadOnlyCollection<BankAndServiceBalanceReportRow> MapRowsByPayment(
            LastFirmPayment lastFirmPayment,
            IReadOnlyDictionary<int, LastBankBalanceResponseDto[]> bankBalancesByFirmId,
            IReadOnlyDictionary<int, FirmShortInfoDto> firmInfoById,
            IReadOnlyDictionary<int, BaseUserInfoDto> userInfoByFirmId,
            IReadOnlyDictionary<int, SettlementAccountDto[]> settlementAccountByFirmId,
            IReadOnlyDictionary<int, BankName> bankNameById,
            IReadOnlyDictionary<int, AccountBalanceDto[]> serviceRemainsBalancesByFirmId,
            IReadOnlyDictionary<int, SettlementAccountBalanceResponse[]> serviceOperationBalancesByFirmId,
            IReadOnlyDictionary<int, SettlementAccountReconciliation> reconciliationBySettlementAccountId)
        {
            var firmId = lastFirmPayment.FirmId;
            var bankBalances = bankBalancesByFirmId.GetValueOrDefault(firmId);
            if (bankBalances == null || !bankBalances.Any())
            {
                return Array.Empty<BankAndServiceBalanceReportRow>();
            }

            return bankBalances
                .Select(bankBalance =>
                    MapRowData(
                        lastFirmPayment,
                        bankBalance,
                        firmInfoById,
                        userInfoByFirmId,
                        settlementAccountByFirmId,
                        bankNameById,
                        serviceRemainsBalancesByFirmId,
                        serviceOperationBalancesByFirmId,
                        reconciliationBySettlementAccountId))
                .Where(x => x != null)
                .ToArray();
        }

        private BankAndServiceBalanceReportRow MapRowData(
            LastFirmPayment lastPayment,
            LastBankBalanceResponseDto bankBalance,
            IReadOnlyDictionary<int, FirmShortInfoDto> firmInfoById,
            IReadOnlyDictionary<int, BaseUserInfoDto> userInfoByFirmId,
            IReadOnlyDictionary<int, SettlementAccountDto[]> settlementAccountByFirmId,
            IReadOnlyDictionary<int, BankName> bankNameById,
            IReadOnlyDictionary<int, AccountBalanceDto[]> serviceRemainsBalancesByFirmId,
            IReadOnlyDictionary<int, SettlementAccountBalanceResponse[]> serviceOperationBalancesByFirmId,
            IReadOnlyDictionary<int, SettlementAccountReconciliation> reconciliationBySettlementAccountId)
        {
            int firmId = lastPayment.FirmId;
            var settlementAccountId = bankBalance.SettlementAccountId;

            var firmInfo = firmInfoById[firmId];
            var userInfo = userInfoByFirmId.GetValueOrDefault(firmId);
            if (userInfo == null)
            {
                return null;
            }
            var firmSettlementAccountList = settlementAccountByFirmId.GetValueOrDefault(firmId);
            var settlementAccount = firmSettlementAccountList?.FirstOrDefault(x => x.Id == settlementAccountId);
            if (settlementAccount == null)
            {
                return null;
            }

            var bankName = bankNameById[settlementAccount.BankId];
            var serviceRemainsBalancesForFirm = GetServiceRemainsBalancesForFirm(serviceRemainsBalancesByFirmId, firmId, firmSettlementAccountList);
            var serviceRemainsBalancesForSettlementAccount = GetServiceRemainsBalancesForSettlementAccount(settlementAccount, serviceRemainsBalancesForFirm);
            var serviceRemainsBalanceSum = GetServiceRemainsBalanceSum(serviceRemainsBalancesForSettlementAccount);
            var serviceOperationBalance = GetServiceBalanceBySettlementAccount(firmId, settlementAccountId, serviceOperationBalancesByFirmId);
            var reconciliation = reconciliationBySettlementAccountId.GetValueOrDefault(settlementAccountId);

            return new BankAndServiceBalanceReportRow
            {
                Login = userInfo.Login,
                Tariff = lastPayment.Tariff,
                Product = lastPayment.Product,
                IsOoo = firmInfo.IsOoo,
                SettlementAccount = settlementAccount.Number,
                BankName = bankName.Name,
                BankBalanceDate = bankBalance.BalanceDate,
                BankBalance = bankBalance.Balance,
                ServiceBalance = serviceRemainsBalanceSum + (serviceOperationBalance?.Balance ?? 0),
                RemainsFilled = serviceRemainsBalancesForSettlementAccount?.Any() ?? false,
                RemainsFillDate = serviceRemainsBalancesForSettlementAccount?.Any() ?? false
                    ? serviceRemainsBalancesForSettlementAccount.Max(x => x.Date)
                    : null,
                CountSettlementAccountWithRemainsFilled = serviceRemainsBalancesForFirm?.GroupBy(x => x.SubcontoId).Count() ?? 0,
                ReconciliationState = reconciliation?.Status.ToString() ?? string.Empty,
                LastReconciliationStartDate = reconciliation?.CreateDate,
                UnrecognizedIncomingCount = serviceOperationBalance?.UnrecognizedIncomingCount ?? 0,
                UnrecognizedIncomingSum = serviceOperationBalance?.UnrecognizedIncomingSum ?? 0,
                UnrecognizedOutgoingCount = serviceOperationBalance?.UnrecognizedOutgoingCount ?? 0,
                UnrecognizedOutgoingSum = serviceOperationBalance?.UnrecognizedOutgoingSum ?? 0
            };
        }

        private IReadOnlyCollection<AccountBalanceDto> GetServiceRemainsBalancesForFirm(
            IReadOnlyDictionary<int, AccountBalanceDto[]> serviceRemainsBalancesByFirmId,
            int firmId,
            IReadOnlyCollection<SettlementAccountDto> settlementAccounts)
        {
            var settlementAccountSubcontoList = settlementAccounts
                .Select(x => x.SubcontoId)
                .ToArray();

            var serviceRemainsBalancesForFirm = serviceRemainsBalancesByFirmId.GetValueOrDefault(firmId);
            if (serviceRemainsBalancesForFirm == null)
            {
                return null;
            }

            // среди всех остатков отфильтровываем только остатки по р/сч
            return serviceRemainsBalancesForFirm
                .Where(x => settlementAccountSubcontoList.Contains(x.SubcontoId))
                .ToArray();
        }

        private IReadOnlyCollection<AccountBalanceDto> GetServiceRemainsBalancesForSettlementAccount(
            SettlementAccountDto settlementAccount,
            IReadOnlyCollection<AccountBalanceDto> serviceRemainsBalances)
        {
            if (serviceRemainsBalances == null)
            {
                return null;
            }

            return serviceRemainsBalances
                .Where(x => x.SubcontoId == settlementAccount.SubcontoId)
                .ToArray();
        }

        private decimal GetServiceRemainsBalanceSum(
            IReadOnlyCollection<AccountBalanceDto> serviceRemainsBalances)
        {
            if (serviceRemainsBalances == null)
            {
                return 0;
            }

            return serviceRemainsBalances
                .Sum(x => x.IsCredit ? -x.CurrencySum ?? -x.Sum : x.CurrencySum ?? x.Sum);
        }

        private SettlementAccountBalanceResponse GetServiceBalanceBySettlementAccount(int firmId,
            int settlementAccountId,
            IReadOnlyDictionary<int, SettlementAccountBalanceResponse[]> serviceOperationBalancesByFirmId)
        {
            var firmBalances = serviceOperationBalancesByFirmId.GetValueOrDefault(firmId);
            return firmBalances?.FirstOrDefault(x => x.SettlementAccountId == settlementAccountId);
        }
    }
}
