using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Client.Balances;
using Moedelo.AccountingV2.Client.Cash;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.Finances.Domain.Interfaces.Business.BalanceMaster;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Interfaces.Business.SettlementAccounts;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Balances;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.KontragentsV2.Client.Kontragents;
using Moedelo.RequisitesV2.Client.SettlementAccounts;

namespace Moedelo.Finances.Business.Services.Money.Balances
{
    [InjectAsSingleton(typeof(IAccMoneyBalancesReader))]
    public class AccMoneyBalancesReader : IAccMoneyBalancesReader
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

        private readonly IBalancesApiClient balancesApiClient;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly ICashApiClient cashApiClient;
        private readonly IKontragentsClient kontragentsClient;
        private readonly IBalanceMasterService balanceMasterService;
        private readonly IAccMoneyBalancesDao balancesDao;
        private readonly ISettlementAccountsReader settlementAccountsReader;

        public AccMoneyBalancesReader(
            IBalancesApiClient balancesApiClient,
            ISettlementAccountClient settlementAccountClient,
            ICashApiClient cashApiClient,
            IKontragentsClient kontragentsClient,
            IBalanceMasterService balanceMasterService,
            IAccMoneyBalancesDao balancesDao,
            ISettlementAccountsReader settlementAccountsReader)
        {
            this.balancesApiClient = balancesApiClient;
            this.settlementAccountClient = settlementAccountClient;
            this.cashApiClient = cashApiClient;
            this.kontragentsClient = kontragentsClient;
            this.balanceMasterService = balanceMasterService;
            this.balancesDao = balancesDao;
            this.settlementAccountsReader = settlementAccountsReader;
        }

        public async Task<List<MoneySourceBalance>> GetAsync(int firmId, int userId,
            IReadOnlyCollection<MoneySourceBase> moneySources, DateTime? date,
            CancellationToken cancellationToken)
        {
            if (moneySources.Count == 0)
            {
                return new List<MoneySourceBalance>();
            }

            var onDate = date ?? DateTime.Today;
            onDate = onDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            var balanceMaster = await balanceMasterService
                .GetAccStatusAsync(firmId, userId, cancellationToken)
                .ConfigureAwait(false);

            var balances = await balancesDao.GetBySourceIdsAsync(firmId, new BalancesRequest
            {
                BalancesMasterDate = balanceMaster.Date,
                MoneySources = moneySources,
                OnDate = onDate
            }, cancellationToken).ConfigureAwait(false);

            balances.AddRange(moneySources.Where(s => balances.All(b => !IsEqual(s, b)))
                .Select(s => new MoneySourceBalance
                {
                    Type = s.Type,
                    Id = s.Id,
                    Balance = 0
                })
            );

            if (balanceMaster.IsCompleted)
            {
                await ApplyBalancesSumAsync(firmId, userId, moneySources, balances).ConfigureAwait(false);
            }
            return balances;
        }

        public Task<decimal> GetBalancesSumAsync(int firmId, int userId, MoneySourceType sourceType, long? sourceId)
        {
            switch (sourceType)
            {
                case MoneySourceType.All:
                    return GetAllBalancesSumAsync(firmId, userId);
                case MoneySourceType.SettlementAccount:
                    return GetSettlementAccountBalancesSumAsync(firmId, userId, (int?)sourceId);
                case MoneySourceType.Cash:
                    return GetCashBalancesSumAsync(firmId, userId, sourceId);
                case MoneySourceType.Purse:
                    return GetPurseBalancesSumAsync(firmId, userId, sourceId);
                default:
                    return Task.FromResult(0m);
            }
        }

        public async Task<Dictionary<Currency, decimal>> GetCurrencyBalancesSumAsync(int firmId, int userId, MoneySourceType sourceType, long? sourceId)
        {
            var result = new Dictionary<Currency, decimal>();

            var balances = await balancesApiClient.GetByAccountCodesAsync(firmId, userId,
                new []
                {
                    SyntheticAccountCode._52_01_01,
                    SyntheticAccountCode._52_01_02
                }).ConfigureAwait(false);

            var subcontoIds = balances.Where(b => b.SubcontoId > 0).Select(b => b.SubcontoId.Value).Distinct().ToList();
            var settlementAccounts = await settlementAccountsReader.GetSettlementAccounts(firmId, userId, subcontoIds, sourceId).ConfigureAwait(false);
            var currencies = settlementAccounts.Select(sa => sa.Currency).Distinct().ToList();

            foreach (var currency in currencies)
                {
                    var settlementAccountsSubcontosByCurrency = settlementAccounts
                        .Where(sa => sa.Currency == currency && sa.SubcontoId > 0)
                        .Select(sa => sa.SubcontoId.Value)
                        .ToList();

                    if (settlementAccountsSubcontosByCurrency.Count == 0)
                        continue;

                    var balanceForCurrency = balances.Where(b =>
                            b.SubcontoId > 0 && settlementAccountsSubcontosByCurrency.Contains(b.SubcontoId.Value))
                        .Select(x => x.IsCredit ? -x.CurrencySum ?? 0 : x.CurrencySum ?? 0)
                        .Sum();
                    result.Add(currency, balanceForCurrency);
                }

                return result;
        }

        private async Task ApplyBalancesSumAsync(int firmId, int userId, IReadOnlyCollection<MoneySourceBase> moneySources, IReadOnlyCollection<MoneySourceBalance> moneyBalances)
        {
            if (moneyBalances.Count == 0)
            {
                return;
            }

            var masterBalances = await balancesApiClient.GetByAccountCodesAsync(firmId, userId, AccountCodes).ConfigureAwait(false);
            if (masterBalances.Count == 0)
            {
                return;
            }

            var mainCash = moneySources.FirstOrDefault(s => s.Type == MoneySourceType.Cash && s.IsPrimary);
            if (mainCash != null)
            {
                var mainCashBalance = masterBalances.FirstOrDefault(b => b.Code == SyntheticAccountCode._50_01);
                if (mainCashBalance != null)
                {
                    mainCashBalance.SubcontoId = mainCash.SubcontoId;
                }
            }

            var subcontoIdBalanceMatches = masterBalances.Where(x => x.SubcontoId.HasValue)
                .GroupBy(x => x.SubcontoId.Value)
                .ToDictionary(x => x.Key, x => x.ToList());

            var settlementAccountIdSubcontoIdMatches = new Dictionary<long, long>();
            var settlementAccounts = moneySources.Where(x => x.Type == MoneySourceType.SettlementAccount);
            foreach (var settlementAccount in settlementAccounts.Where(x => x.SubcontoId.HasValue))
            {
                settlementAccountIdSubcontoIdMatches.Add(settlementAccount.Id, settlementAccount.SubcontoId.Value);
            }

            var cashIdSubcontoIdMatches = new Dictionary<long, long>();
            var cashList = moneySources.Where(x => x.Type == MoneySourceType.Cash);
            foreach (var cash in cashList.Where(x => x.SubcontoId.HasValue))
            {
                cashIdSubcontoIdMatches.Add(cash.Id, cash.SubcontoId.Value);
            }

            var purseIdSubcontoIdMatches = new Dictionary<long, long>();
            var purseList = moneySources.Where(x => x.Type == MoneySourceType.Purse);
            foreach (var purse in purseList.Where(x => x.SubcontoId.HasValue))
            {
                purseIdSubcontoIdMatches.Add(purse.Id, purse.SubcontoId.Value);
            }

            foreach (var moneyBalance in moneyBalances)
            {
                Dictionary<long, long> sourceIdSubcontoIdMatches;
                switch (moneyBalance.Type)
                {
                    case MoneySourceType.SettlementAccount:
                        sourceIdSubcontoIdMatches = settlementAccountIdSubcontoIdMatches;
                        break;
                    case MoneySourceType.Cash:
                        sourceIdSubcontoIdMatches = cashIdSubcontoIdMatches;
                        break;
                    case MoneySourceType.Purse:
                        sourceIdSubcontoIdMatches  = purseIdSubcontoIdMatches;
                        break;
                    default:
                        sourceIdSubcontoIdMatches = new Dictionary<long, long>();
                        break;
                }

                if (sourceIdSubcontoIdMatches.TryGetValue(moneyBalance.Id, out var subcontoId))
                {
                    if (subcontoIdBalanceMatches.TryGetValue(subcontoId, out var masterBalancesBySource))
                    {
                        moneyBalance.Balance += masterBalancesBySource.Select(x => x.IsCredit ? -x.CurrencySum ?? -x.Sum : x.CurrencySum ?? x.Sum ).Sum();
                    }
                }
            }
        }

        private static bool IsEqual(MoneySourceBase x, MoneySourceBalance b)
        {
            return b.Id == x.Id && b.Type == x.Type;
        }

        private async Task<decimal> GetAllBalancesSumAsync(int firmId, int userId)
        {
            var balances = await balancesApiClient.GetByAccountCodesAsync(firmId, userId,
                new []
                {
                    SyntheticAccountCode._50_01,
                    SyntheticAccountCode._50_02,
                    SyntheticAccountCode._51_01,
                    SyntheticAccountCode._76_07
                }).ConfigureAwait(false);
            return balances.Select(x => x.IsCredit ? -x.Sum : x.Sum).Sum();
        }

        private async Task<decimal> GetSettlementAccountBalancesSumAsync(int firmId, int userId, int? settlementAccountId)
        {
            if (!settlementAccountId.HasValue)
            {
                throw new NotImplementedException();
            }
            var settlementAccount = await settlementAccountClient.GetByIdAsync(firmId, userId, settlementAccountId.Value)
                .ConfigureAwait(false);
            if (settlementAccount?.SubcontoId == null)
            {
                return 0m;
            }
            var balances = await balancesApiClient.GetBySubcontoIdsAsync(firmId, userId, new[] { settlementAccount.SubcontoId.Value }).ConfigureAwait(false);
            return balances.Select(x => x.IsCredit ? -x.Sum : x.Sum).Sum();
        }

        private async Task<decimal> GetCashBalancesSumAsync(int firmId, int userId, long? cashId)
        {
            if (!cashId.HasValue)
            {
                throw new NotImplementedException();
            }
            var cash = await cashApiClient.GetByIdAsync(firmId, userId, cashId.Value)
                .ConfigureAwait(false);
            if (cash?.SubcontoId == null)
            {
                return 0m;
            }
            var balances = cash.IsMain
                ? await balancesApiClient.GetByAccountCodesAsync(firmId, userId, new[] { SyntheticAccountCode._50_01 }).ConfigureAwait(false)
                : await balancesApiClient.GetBySubcontoIdsAsync(firmId, userId, new[] { cash.SubcontoId.Value }).ConfigureAwait(false);
            return balances.Select(x => x.IsCredit ? -x.Sum : x.Sum).Sum();
        }

        private async Task<decimal> GetPurseBalancesSumAsync(int firmId, int userId, long? purseId)
        {
            if (!purseId.HasValue)
            {
                throw new NotImplementedException();
            }
            var kontragent = await kontragentsClient.GetByIdAsync(firmId, userId, (int)purseId.Value).ConfigureAwait(false);
            if (kontragent?.SubcontoId == null)
            {
                return 0m;
            }
            var balances = (await balancesApiClient.GetByAccountCodesAsync(firmId, userId, new[] { SyntheticAccountCode._76_07 }).ConfigureAwait(false))
                .Where(x => x.SubcontoId == kontragent.SubcontoId.Value);
            return balances.Select(x => x.IsCredit ? -x.Sum : x.Sum).Sum();
        }
    }
}
