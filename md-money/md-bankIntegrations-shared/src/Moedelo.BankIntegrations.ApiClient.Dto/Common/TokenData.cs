using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Common
{
    public class TokenDataDto : OpenIdGetTokenResponseDto
    {
        public DateTime SessionLastDate { get; set; }
    }
}
