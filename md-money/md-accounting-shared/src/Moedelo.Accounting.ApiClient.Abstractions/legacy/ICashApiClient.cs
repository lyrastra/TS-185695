using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface ICashApiClient
    {
        Task<CashDto[]> GetAsync(UserId userId, FirmId firmId);

        Task<CashDto> GetByIdAsync(UserId userId, FirmId firmId, long id);

        Task SetOtherKontragent(FirmId firmId, UserId userId, IReadOnlyCollection<long> ids);
    }
}
