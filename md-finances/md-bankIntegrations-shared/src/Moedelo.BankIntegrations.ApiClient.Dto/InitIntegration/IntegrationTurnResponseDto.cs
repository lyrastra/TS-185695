using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.ApiClient.Dto.InitIntegration
{
    public class IntegrationTurnResponseDto
    {
        public IntegrationResponseStatusCode StatusCode { get; set; }

        public string ErrorText { get; set; }

        /// <summary> Заштрихованная часть телефонного номера для OTP </summary>
        public string PhoneMask { get; set; }
    }
}
