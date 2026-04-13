using System;
using System.Collections.Generic;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.CalendarEventSubscription
{
    public class UpdateLastEmailAndSmsProcessingDateRequestDto
    {
        public IReadOnlyCollection<int> FirmIds { get; set; }

        public DateTime? LastEmailProcessDate { get; set; }

        public DateTime? LastSmsProcessDate { get; set; }
    }
}