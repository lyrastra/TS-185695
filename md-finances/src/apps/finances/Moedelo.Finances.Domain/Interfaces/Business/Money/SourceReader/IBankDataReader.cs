using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Domain.Models.Money.SourceReader;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader
{
    public interface IBankDataReader
    {
        Task<Dictionary<int, SourceBankData>> GetBanksBySourcesAsync(IUserContext userContext,
            IReadOnlyCollection<MoneySource> sources, CancellationToken cancellationToken = default);
    }
}
