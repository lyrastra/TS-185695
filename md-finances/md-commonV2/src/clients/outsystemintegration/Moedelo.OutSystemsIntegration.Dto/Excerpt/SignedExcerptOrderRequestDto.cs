namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptOrderRequestDto
    {
        public string Cookie { get; set; }

        public string CaptchaToken { get; set; }

        public string Captcha { get; set; }

        public string InnOrOgrn { get; set; }

        public int UserId { get; set; }

        public int FirmId { get; set; }
    }
}