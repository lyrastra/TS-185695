using Moedelo.Spam.ApiClient.Abastractions.Dto.PushToken;
using System.Threading.Tasks;
using System.Threading;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.Push;

public interface IPushTokenNetApiClient
{
    Task SaveAsync(
        PushTokenSaveRequestDto dto,
        CancellationToken cancellationToken);
}