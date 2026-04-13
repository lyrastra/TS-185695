using Moedelo.HomeV2.Dto.ClientSource;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.ClientSource
{
    [InjectAsSingleton]
    public class ClientSourceApiClient : BaseApiClient, IClientSourceApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ClientSourceApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager
            )
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint") ;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/ClientSource/";
        }

        public async Task<List<ClientSourceDto>> GetAllSourceForPromoCodeAsync()
        {
            var response = await GetAsync<ListWrapper<ClientSourceDto>>("GetAllSourceForPromoCodeAsync").ConfigureAwait(false);
            return response.Items;
        }

        public async Task<int> SaveClientSourceAsync(ClientSourceDto requestDto)
        {
            var response = await PostAsync<ClientSourceDto, DataRequestWrapper<int>>("SaveClientSourceAsync", requestDto).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<bool> CheckExistsClientSourceByNameAsync(string name)
        {
            var response = await GetAsync<DataRequestWrapper<bool>>("CheckExistsClientSourceByNameAsync").ConfigureAwait(false);
            return response.Data;
        }

        public Task DeleteClientSourceAsync(int id)
        {
            return PostAsync("DeleteClientSourceAsync");
        }
    }
}