namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptCaptchaResponseDto
    {
        public byte[] Captcha { get; set; }
        
        public string CaptchaToken { get; set; }
    }
}