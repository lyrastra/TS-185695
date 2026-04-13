using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class IntegrationTurnResponseDto
    {
        public IntegrationResponseStatusCode StatusCode { get; set; }

        public string ErrorText { get; set; }

        /// <summary> Заштрихованная часть телефонного номера для OTP </summary>
        public string PhoneMask { get; set; }
    }
}
