using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Balances;
using Moedelo.Money.BankBalanceHistory.DataAccess.Abstractions;
using Moedelo.Money.BankBalanceHistory.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.Balances
{
    [InjectAsSingleton(typeof(IBalancesReader))]
    internal class BalancesReader : IBalancesReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IBalancesDao dao;

        public BalancesReader(
            IExecutionInfoContextAccessor contextAccessor,
            IBalancesDao dao)
        {
            this.contextAccessor = contextAccessor;
            this.dao = dao;
        }

        public async Task<BankBalanceResponse> GetAsync(BankBalanceRequest request)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var isContaisData = await dao.IsContainsDataForPeriodAsync(
                (int)context.FirmId,
                request.SettlementAccountId,
                request.StartDate,
                request.EndDate,
                request.IncludeUserMovement);

            if (isContaisData == false)
            {
                return null;
            }

            return await dao.GetAsync((int)context.FirmId, request);
        }

        public  async Task<LastBankBalance[]> GetOnDateByFirmIdAsync(DateTime onDate, bool includeUserMovement, DateTime? minDate = null)
        {
            var context = contextAccessor.ExecutionInfoContext;
            var result = await dao.GetOnDateByFirmIdAsync((int)context.FirmId,
                onDate,
                minDate ?? new DateTime(0001, 1, 1),
                includeUserMovement);
            return result;
        }

        public Task<IReadOnlyDictionary<int, LastBankBalance[]>> GetOnDateByFirmIdsAsync(IReadOnlyCollection<int> firmIds,
            DateTime onDate, DateTime minDate, bool includeUserMovement)
        {
            if (firmIds == null || firmIds.Count == 0)
            {
                return Task.FromResult<IReadOnlyDictionary<int, LastBankBalance[]>>(new Dictionary<int, LastBankBalance[]>());
            }
            return dao.GetOnDateByFirmIdsAsync(firmIds, onDate, minDate, includeUserMovement);
        }
    }
}
