using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.CashOrders.Business.AccPostings
{
    [InjectAsSingleton(typeof(SyntheticAccountReader))]
    internal class SyntheticAccountReader
    {
        private readonly ConcurrentDictionary<SyntheticAccountCode, long?> cache = new();

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISyntheticAccountClient syntheticAccountClient;

        public SyntheticAccountReader(
            IExecutionInfoContextAccessor contextAccessor,
            ISyntheticAccountClient syntheticAccountClient)
        {
            this.contextAccessor = contextAccessor;
            this.syntheticAccountClient = syntheticAccountClient;
        }

        public async Task<long?> GetIdByCodeAsync(SyntheticAccountCode code)
        {
            if (cache.TryGetValue(code, out var syntheticAccountId))
            {
                return syntheticAccountId;
            }

            var context = contextAccessor.ExecutionInfoContext;
            var response = await syntheticAccountClient.GetByCodes(new[] { code });
            syntheticAccountId = response.SingleOrDefault().Id;

            cache.TryAdd(code, syntheticAccountId);

            return syntheticAccountId;
        }
    }
}
