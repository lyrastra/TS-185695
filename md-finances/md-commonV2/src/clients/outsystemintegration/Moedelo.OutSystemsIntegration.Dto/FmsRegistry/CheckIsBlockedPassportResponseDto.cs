using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.FmsRegistry
{
    public class CheckIsBlockedPassportResponseDto
    {
        public bool IsBlocked { get; set; }

        public DateTime DBDate { get; set; }
    }
}