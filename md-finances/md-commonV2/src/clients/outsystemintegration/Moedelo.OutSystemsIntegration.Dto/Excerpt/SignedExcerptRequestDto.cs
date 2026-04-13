namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptRequestDto : IHaveSessionId, IHaveToken, IHaveCaptchaResolve
    {
        public string SessionID { get; set; }

        public string Token { get; set; }
        
        public string Resolve { get; set; }
    }
}