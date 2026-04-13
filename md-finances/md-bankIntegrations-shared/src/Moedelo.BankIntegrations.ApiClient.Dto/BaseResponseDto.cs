using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.Dto
{
    public class BaseResponseDto
    {
        public IntegrationResponseStatusCode StatusCode { get; set; }
        public string RequestMessage { get; set; }
        public string ResponseMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorMessageForUser { get; set; }
        public bool IsNeedDisableIntegration { get; set; }
    }
}
