using System.Collections.Generic;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.CalendarEventSubscription
{
    public class CalendarEventSubscriptionResponseDto
    {
        public int FirmId { get; set; }

        public IReadOnlyCollection<CalendarEventSubscriptionUserData> UsersData { get; set; }
    }
}