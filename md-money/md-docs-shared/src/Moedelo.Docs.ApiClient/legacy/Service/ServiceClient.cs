using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Service;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Service.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Docs.ApiClient.legacy.Service
{
    [InjectAsSingleton(typeof(IServiceClient))]
    public class ServiceClient : IServiceClient
    {
        private readonly IHttpRequestExecuter httpRequestExecuter;
        private readonly SettingValue docsApiEndpoint;

        public ServiceClient(
            IHttpRequestExecuter httpRequestExecuter,
            ISettingRepository settingRepository)
        {
            this.httpRequestExecuter = httpRequestExecuter;
            docsApiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }
        
        public async Task<List<ServiceDto>> GetByNamesAsync(FirmId firmId, UserId userId, IReadOnlyCollection<string> names)
        {
            if (names?.Any() != true)
            {
                return new List<ServiceDto>();
            }
            
            var uri = $"{docsApiEndpoint.Value}/Services/ByNames?firmId={firmId}&userId={userId}";
            var response = await httpRequestExecuter.PostAsync(uri, names.ToUtf8JsonContent()).ConfigureAwait(false);
            return response.FromJsonString<List<ServiceDto>>();
        }
    }
}