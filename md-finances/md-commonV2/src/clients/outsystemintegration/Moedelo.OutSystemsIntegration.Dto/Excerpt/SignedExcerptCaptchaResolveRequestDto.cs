namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptCaptchaResolveRequestDto : IHaveSessionId
    {
        public string Captcha { get; set; }
        
        public string CaptchaToken { get; set; }
        
        public string SessionID { get; set; }
    }
}