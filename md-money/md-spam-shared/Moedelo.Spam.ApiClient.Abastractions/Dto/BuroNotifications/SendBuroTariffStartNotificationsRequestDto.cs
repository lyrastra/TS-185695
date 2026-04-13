using System;
using System.Collections.Generic;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.BuroNotifications
{
    public class SendBuroTariffStartNotificationsRequestDto
    {
        public string Email { get; set; }
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public IReadOnlyCollection<DateTime> SendDates { get; set; }
    }
}