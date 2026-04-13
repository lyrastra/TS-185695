using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface IChargeGenerationApiClient
{
    Task<ChargeGenerationInitialDataDto> GetInitialDataAsync(FirmId firmId, UserId userId,
        CancellationToken token = default);
}