using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.RepeatHandler;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationRequestQueueApiClient
{
    public interface IIntegrationRequestQueueApiClient
    {
        Task CreateEventAsync(RepeatEventCreateRequestDto request, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<RepeatEventDto>> GetReadyEventsAsync(REventType type, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<RepeatEventDto>> TakeEventsInWorkAsync(REventType type, CancellationToken cancellationToken = default);

        Task TakeEventReadyAsync(int eventId, RepeatEventReadyRequestDto request, CancellationToken cancellationToken = default);

        Task UpdateEventRetryDateAsync(int eventId, RepeatEventRetryDateRequestDto request, CancellationToken cancellationToken = default);

        Task DeleteEventAsync(int eventId, CancellationToken cancellationToken = default);

        Task DeleteEventsAsync(RepeatEventDeleteRequestDto request, CancellationToken cancellationToken = default);
    }
}
