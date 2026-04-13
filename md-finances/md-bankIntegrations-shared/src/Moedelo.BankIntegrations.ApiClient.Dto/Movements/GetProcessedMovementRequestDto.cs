using System;
using Moedelo.BankIntegrations.ApiClient.Dto.Movements;

namespace Moedelo.BankIntegrations.Dto.Movements
{
    public class GetProcessedMovementRequestDto: BaseRequestDto
    {
        public string RequestId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public FirmDetailsDto FirmDetails { get; set; }
    }
}
