using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.OutSystemsIntegrationV2.Dto.Excerpt;
using System.Threading.Tasks;

namespace Moedelo.OutSystemsIntegrationV2.Client.NalogRu
{
    public interface INalogRuApiClient : IDI
    {
        Task<string> GetSession();

        Task<SignedExcerptResponseDto> Query(SignedExcerptQueryRequestDto request);

        Task<SignedExcerptSearchResponseDto> Search(SignedExcerptRequestDto request);

        Task<SignedExcerptResponseDto> Request(SignedExcerptRequestDto request);

        Task<bool> IsReady(SignedExcerptRequestDto request);
        
        Task<byte[]> Download(SignedExcerptRequestDto request);

        Task<SignedExcerptCaptchaResponseDto> Captcha(string sessionId);

        Task<string> CaptchaResolve(SignedExcerptCaptchaResolveRequestDto request);
    }
}