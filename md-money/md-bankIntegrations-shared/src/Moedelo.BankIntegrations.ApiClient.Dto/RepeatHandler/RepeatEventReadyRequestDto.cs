using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.RepeatHandler
{
    public class RepeatEventReadyRequestDto
    {
        public int RetryCount { get; set; }

        public DateTime RetryDate { get; set; }
    }
}
