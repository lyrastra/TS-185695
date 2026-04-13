using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.CommonApi.ApiClient.Abstractions.legacy.FirmFlags.Dtos;

namespace Moedelo.CommonApi.ApiClient.Abstractions.legacy.FirmFlags
{
    public interface IFirmFlagsApiClient
    {
        Task<FirmFlagDto[]> GetAsync(FirmId firmId, CancellationToken cancellationToken = default);
        
        Task<bool> IsEnableAsync(FirmId firmId, UserId userId, string name, CancellationToken cancellationToken = default);

        Task EnableAsync(FirmId firmId, UserId userId, string name);

        Task DisableAsync(FirmId firmId, UserId userId, string name);

        Task RemoveAsync(FirmId firmId, UserId userId, string name);
    }
}
