using System.Collections.Concurrent;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.SyntheticAccounts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.SyntheticAccounts
{
    [InjectAsSingleton(typeof(ISyntheticAccountReader))]
    internal class SyntheticAccountReader : ISyntheticAccountReader
    {
        private readonly ISyntheticAccountClient syntheticAccountClient;
        private readonly IExecutionInfoContextAccessor contextAccessor;

        private ConcurrentDictionary<SyntheticAccountCode, SyntheticAccountDto> accountsCache =
            new ConcurrentDictionary<SyntheticAccountCode, SyntheticAccountDto>();

        private ConcurrentDictionary<SyntheticAccountCode, IReadOnlyCollection<SyntheticAccountSubcontoRule>>
            subcontoRulesCache = new ConcurrentDictionary<SyntheticAccountCode, IReadOnlyCollection<SyntheticAccountSubcontoRule>>();

        public SyntheticAccountReader(
            ISyntheticAccountClient syntheticAccountClient,
            IExecutionInfoContextAccessor contextAccessor)
        {
            this.syntheticAccountClient = syntheticAccountClient;
            this.contextAccessor = contextAccessor;
        }

        public async Task<long?> GetIdByCodeAsync(SyntheticAccountCode syntheticAccountCode)
        {
            var syntheticAccount = accountsCache.GetValueOrDefault(syntheticAccountCode);
            if (syntheticAccount != null)
            {
                return syntheticAccount.Id;
            }

            var context = contextAccessor.ExecutionInfoContext;
            var syntheticAccounts = await syntheticAccountClient
                .GetByCodes(new[] { syntheticAccountCode });

            syntheticAccount = syntheticAccounts.FirstOrDefault();

            return syntheticAccount != null 
                ? accountsCache.GetOrAdd(syntheticAccountCode, _ => syntheticAccount).Id 
                : null;
        }
        
        public async Task<IReadOnlyCollection<SyntheticAccountSubcontoRule>> GetSubcontoRulesAsync(
            SyntheticAccountCode syntheticAccountCode)
        {
            var rules = subcontoRulesCache.GetValueOrDefault(syntheticAccountCode);
            if (rules != null)
            {
                return rules;
            }

            var response = await syntheticAccountClient
                .GetRulesByCodesAsync(new[] { syntheticAccountCode });

            rules = response.Select(x => new SyntheticAccountSubcontoRule
            {
                SubcontoType = x.SubcontoType,
                Level = x.Level
            }).ToArray();

            return subcontoRulesCache.GetOrAdd(syntheticAccountCode, _ => rules);
        }
    }
}
