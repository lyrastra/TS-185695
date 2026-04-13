using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccPostings.Client
{
    public interface ISyntheticAccountClient : IDI
    {
        Task<List<SyntheticAccountDto>> GetActualAsync();
        
        Task<List<SyntheticAccountTypeDto>> GetByIdsAsync(IReadOnlyCollection<long> accountTypeIds);

        Task<List<SyntheticAccountTypeDto>> GetByCodesAsync(IReadOnlyCollection<SyntheticAccountCode> codes);

        Task<List<SyntheticAccountSubcontoRuleDto>> GetRulesByAccountIdsAsync(IReadOnlyCollection<SyntheticAccountCode> codes);
    }
}