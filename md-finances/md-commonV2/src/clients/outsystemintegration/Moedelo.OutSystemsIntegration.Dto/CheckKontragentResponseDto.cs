using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.OutSystemsIntegrationV2.Dto
{
    public class CheckKontragentResponseDto
    {
        public string Inn { get; set; }

        public string Kpp { get; set; }

        public CheckKontragentStatus Status { get; set; }
    }
}