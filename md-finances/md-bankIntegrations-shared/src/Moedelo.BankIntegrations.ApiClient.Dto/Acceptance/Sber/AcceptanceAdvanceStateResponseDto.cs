using Moedelo.BankIntegrations.Dto;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Acceptance.Sber
{
    public class AcceptanceAdvanceStateResponseDto : BaseResponseDto
    {
        public string BankComment { get; set; }
        public string BankStatus { get; set; }
    }
}
