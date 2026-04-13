namespace Moedelo.OutSystemsIntegrationV2.Dto.Excerpt
{
    public class SignedExcerptQueryRequestDto : IHaveSessionId, IHaveCaptchaResolve
    {
        public string Query { get; set; }
        
        public string SessionID { get; set; }

        public string Resolve { get; set; }
    }
}