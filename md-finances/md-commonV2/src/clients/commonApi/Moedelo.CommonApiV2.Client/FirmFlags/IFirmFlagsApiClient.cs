using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonApiV2.Dto.FirmFlag;

namespace Moedelo.CommonApiV2.Client.FirmFlags;

public interface IFirmFlagsApiClient
{
    Task<int> GetCountFirmWithFlag(string name);

    Task<List<int>> GetEnabledByFirms(GetEnabledByFirmsDto dto);

    Task<List<FirmFlagDto>> GetAsync(int firmId);

    Task<bool> IsEnableAsync(int firmId, int userId, string name, CancellationToken cancellationToken = default);

    Task<bool> HasAnyEnabledAsync(int firmId, string[] names, CancellationToken ct);

    Task EnableAsync(int firmId, int userId, string name);

    Task DisableAsync(int firmId, int userId, string name);

    Task RemoveAsync(int firmId, int userId, string name);
}