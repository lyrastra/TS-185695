#nullable enable
namespace Moedelo.AccountV2.Dto.ExternalApi
{
    public class ApiKeyAuthorizationResultDto
    {
        public ApiKeyAuthorizationStatus Status { get; set; }
        public AuthorizedApiKeyDto? ApiKey { get; set; }
    }
}
