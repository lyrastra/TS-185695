using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptOrderResponceDto
    {
        public string Cookie { get; set; }

        public string CaptchaToken { get; set; }

        public string CaptchaImage { get; set; }

        public SignedExcerptStatus Status { get; set; }
    }
}