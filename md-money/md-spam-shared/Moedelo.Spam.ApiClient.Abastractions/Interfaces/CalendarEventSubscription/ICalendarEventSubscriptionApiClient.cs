using System.Threading;
using System.Threading.Tasks;
using Moedelo.Spam.ApiClient.Abastractions.Dto.CalendarEventSubscription;
using Moedelo.Spam.ApiClient.Abastractions.Dto.Common;

namespace Moedelo.Spam.ApiClient.Abastractions.Interfaces.CalendarEventSubscription
{
    public interface ICalendarEventSubscriptionApiClient
    {
        Task<ApiPageResponseDto<CalendarEventSubscriptionResponseDto>> GetPagedListAsync(
            CalendarEventSubscriptionRequestDto dto,
            CancellationToken cancellationToken);

        Task UpdateLastEmailAndSmsProcessingDateByFirmIdsAsync(
            UpdateLastEmailAndSmsProcessingDateRequestDto requestDto,
            CancellationToken cancellationToken);
    }
}