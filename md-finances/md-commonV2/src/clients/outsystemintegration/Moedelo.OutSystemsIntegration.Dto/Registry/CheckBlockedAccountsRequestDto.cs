namespace Moedelo.OutSystemsIntegrationV2.Dto.Registry
{
    public class CheckBlockedAccountsRequestDto
    {
        public string SessionId { get; set; }

        public string Inn { get; set; }

        public string Captcha { get; set; }

        public bool UsingRuCaptcha { get; set; }

        public string CaptchaToken { get; set; }
    }
}
