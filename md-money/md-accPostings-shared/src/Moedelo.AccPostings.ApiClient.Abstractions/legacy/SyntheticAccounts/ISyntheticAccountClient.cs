using Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts.Dto;
using Moedelo.AccPostings.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts
{
    /// <summary>
    /// Счета бухгалтерского учёта
    /// </summary>
    public interface ISyntheticAccountClient
    {
        Task<SyntheticAccountDto[]> GetByIds(IReadOnlyCollection<long> ids);

        Task<SyntheticAccountDto[]> GetByCodes(IReadOnlyCollection<SyntheticAccountCode> codes);

        /// <summary>
        /// ДЕЙСТВУЮЩИЕ счета
        /// </summary>
        Task<SyntheticAccountDto[]> GetActualAsync();

        /// <summary>
        /// Правила субконто для счетов
        /// </summary>
        Task<SyntheticAccountSubcontoRuleDto[]> GetRulesByCodesAsync(IReadOnlyCollection<SyntheticAccountCode> codes);
    }
}
