using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Access;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.SettlementAccounts;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Balances;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.Money.Balances
{
    [InjectAsSingleton]
    public class MoneyBalancesReader : IMoneyBalancesReader
    {
        private readonly IAccMoneyBalancesReader accMoneyBalancesReader;

        public MoneyBalancesReader(
            IAccMoneyBalancesReader accMoneyBalancesReader)
        {
            this.accMoneyBalancesReader = accMoneyBalancesReader;
        }

        public async Task<List<MoneySourceBalance>> GetAsync(IUserContext userContext,
            IReadOnlyCollection<MoneySourceBase> moneySources,
            DateTime? date, CancellationToken cancellationToken)
        {
            await EnsureNotBizAsync(userContext).ConfigureAwait(false);

            if (moneySources.Count == 0)
            {
                return new List<MoneySourceBalance>();
            }

            return await accMoneyBalancesReader
                .GetAsync(userContext.FirmId, userContext.UserId, moneySources, date, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<decimal> GetBalancesSumAsync(IUserContext userContext, MoneySourceType sourceType, long? sourceId)
        {
            await EnsureNotBizAsync(userContext).ConfigureAwait(false);

            return await accMoneyBalancesReader
                .GetBalancesSumAsync(userContext.FirmId, userContext.UserId, sourceType, sourceId)
                .ConfigureAwait(false);
        }

        public async Task<Dictionary<Currency, decimal>> GetCurrencyBalancesSumAsync(IUserContext userContext, MoneySourceType sourceType, long? sourceId)
        {
            await EnsureNotBizAsync(userContext).ConfigureAwait(false);

            return await accMoneyBalancesReader
                .GetCurrencyBalancesSumAsync(userContext.FirmId, userContext.UserId, sourceType, sourceId)
                .ConfigureAwait(false);
        }

        private static async Task EnsureNotBizAsync(IUserContext userContext)
        {
            var isBiz = await userContext.HasAllRuleAsync(AccessRule.BizPlatform).ConfigureAwait(false);

            if (isBiz)
            {
                throw new InvalidOperationException("Не поддерживается для платформы БИЗ");
            }
        }
    }
}