using System.Threading;
using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Clients
{
    public interface INdsRatePeriodsPrivateApiClient
    {
        Task<NdsRatePeriodByFirmIdDto[]> GetByFirmIdsFilterAsync(NdsRatePeriodFilterByFirmIdsDto filterDto,
            CancellationToken ct = default);
    }
}