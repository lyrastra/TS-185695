using System.Threading.Tasks;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Common.AccessRules.External.Authorization
{
    [InjectAsSingleton(typeof(IAuthorizationContextApiClient))]
    public class AuthorizationContextApiClient : IAuthorizationContextApiClient
    {
        private readonly IHttpRequestExecuter httpRequestExecutor;
        private readonly SettingValue apiEndpoint;
        
        public AuthorizationContextApiClient(
            IHttpRequestExecuter httpRequestExecutor,
            ISettingRepository settingRepository)
        {
            this.httpRequestExecutor = httpRequestExecutor;
            apiEndpoint = settingRepository.Get("AuthorizationPrivateApiEndpoint");
        }
        
        public async Task<AuthorizationContextDto> GetAsync(int firmId, int userId)
        {
            var url = $"{apiEndpoint.Value}/v1/AuthorizationContext?firmId={firmId}&userId={userId}";
            
            var response = await httpRequestExecutor.GetAsync(url);
            
            return response.FromJsonString<AuthorizationContextDto>();
        }
    }
}