using System;

namespace Moedelo.BankIntegrations.Dto.Movements
{
    public class RequestMovementRequestDto: BaseRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
