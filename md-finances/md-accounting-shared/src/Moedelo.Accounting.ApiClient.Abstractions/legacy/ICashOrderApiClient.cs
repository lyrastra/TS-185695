using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface ICashOrderApiClient
    {
        Task DeleteAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task<List<FirmCashOrderDto>> GetByBaseIds(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task ProvideAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds);
    }
}