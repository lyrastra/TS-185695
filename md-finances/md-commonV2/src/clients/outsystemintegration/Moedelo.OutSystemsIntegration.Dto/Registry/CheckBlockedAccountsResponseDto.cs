using System.Collections.Generic;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Registry
{
    public class CheckBlockedAccountsResponseDto
    {
        public string SessionId { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsWrongCaptcha { get; set; }

        public string CaptchaImg { get; set; }

        public string CaptchaToken { get; set; }

        public List<BlockedAccountDto> BlockedAccounts { get; set; }

        public string Error { get; set; }

        public bool IsSuccess { get; set; }
    }
}