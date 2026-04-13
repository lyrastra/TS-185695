using System.Threading;
using System.Threading.Tasks;
using Moedelo.BillingV2.Dto.FirmBillingState;

namespace Moedelo.BillingV2.Client.FirmBillingState
{
    public interface IFirmBillingStateApiClient
    {
        Task<FirmBillingStateDto> GetActualAsync(int firmId, CancellationToken cancellationToken = default);
        Task<FirmBillingDatesDto> GetBillingDatesAsync(int firmId, CancellationToken cancellationToken);
    }
}