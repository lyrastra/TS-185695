using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.ExecutionContext.Client.Implementations
{
    /// <inheritdoc />
    [InjectAsSingleton]
    public class TokenApiClient : ITokenApiClient
    {
        private readonly IHttpRequestExecutor httpRequestExecutor;
        private readonly SettingValue apiEndpoint;

        public TokenApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            ISettingRepository settingRepository)
        {
            this.httpRequestExecutor = httpRequestExecutor;
            apiEndpoint = settingRepository.Get("AuthContextApiEndpoint");
        }

        private string GetAuthEndpoint()
        {
            return apiEndpoint.Value;
        }

        /// <inheritdoc />
        public Task<string> GetFromUserContextAsync(int firmId, int userId)
        {
            var uri = $"{GetAuthEndpoint()}/private/api/Token/FromUserContext";
            var requestDto = new
            {
                UserId = userId,
                FirmId = firmId
            };

            return httpRequestExecutor.PostAsync(uri, requestDto);
        }

        /// <inheritdoc />
        public Task<string> GetUnidentified()
        {
            var uri = $"{GetAuthEndpoint()}/private/api/Token/Unidentified";
            return httpRequestExecutor.PostAsync(uri);
        }
    }
}