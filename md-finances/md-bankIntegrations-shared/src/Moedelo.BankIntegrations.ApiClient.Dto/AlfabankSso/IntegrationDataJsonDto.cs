using System;

namespace Moedelo.BankIntegrations.Dto.AlfabankSso
{
    public class IntegrationDataJsonDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? SessionLastDate { get; set; }

    }
}
