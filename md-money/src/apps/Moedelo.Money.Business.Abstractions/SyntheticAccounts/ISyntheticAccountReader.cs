using System.Collections.Generic;
using Moedelo.AccPostings.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.SyntheticAccounts
{
    public interface ISyntheticAccountReader
    {
        Task<long?> GetIdByCodeAsync(SyntheticAccountCode syntheticAccountCode);

        Task<IReadOnlyCollection<SyntheticAccountSubcontoRule>> GetSubcontoRulesAsync(SyntheticAccountCode syntheticAccountCode);
    }
}
