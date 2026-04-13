using System;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.RepeatHandler
{
    public class RepeatEventCreateRequestDto
    {
        public string Data { get; set; }

        public REventStatus Status { get; set; }

        public REventType Type { get; set; }

        public DateTime RetryDate { get; set; }

        public int RetryCount { get; set; }
    }
}
