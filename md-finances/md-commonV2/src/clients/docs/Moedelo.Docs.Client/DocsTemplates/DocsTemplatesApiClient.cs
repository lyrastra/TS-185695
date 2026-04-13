using System.Threading.Tasks;
using Moedelo.Docs.Dto.Common;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.Docs.Client.DocsTemplates
{
    [InjectAsSingleton]
    public class DocsTemplatesApiClient : BaseCoreApiClient, IDocsTemplatesApiClient
    {
        private const string prefix = "/api/v1";
        
        private readonly ISettingRepository settingRepository;

        public DocsTemplatesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("DocsTemplatesApiEndpoint").Value;
        }
        
        public async Task<bool> SimpleBillExistsAsync(int firmId, int userId)
        {
            var uri = $"{prefix}/Bill/exists";
            return await GetResponseApiData<bool>(firmId, userId, uri).ConfigureAwait(false);
        }

        public async Task<bool> BillContractExistsAsync(int firmId, int userId)
        {
            var uri = $"{prefix}/BillContract/exists";
            return await GetResponseApiData<bool>(firmId, userId, uri).ConfigureAwait(false);
        }

        public async Task<bool> StatementExistsAsync(int firmId, int userId)
        {
            var uri = $"{prefix}/Statement/exists";
            return await GetResponseApiData<bool>(firmId, userId, uri).ConfigureAwait(false);
        }

        public async Task<string> SimpleBillGetFileNameAsync(int firmId, int userId)
        {
            var uri = $"{prefix}/Bill/fileName";
            return await GetResponseApiData<string>(firmId, userId, uri).ConfigureAwait(false);
        }

        public async Task<string> BillContractGetFileNameAsync(int firmId, int userId)
        {
            var uri = $"{prefix}/BillContract/fileName";
            return await GetResponseApiData<string>(firmId, userId, uri).ConfigureAwait(false);
        }

        public async Task<string> StatementGetFileNameAsync(int firmId, int userId)
        {
            var uri = $"{prefix}/Statement/fileName";
            return await GetResponseApiData<string>(firmId, userId, uri).ConfigureAwait(false);
        }

        private async Task<T> GetResponseApiData<T>(int firmId, int userId, string uri)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<T>>(
                uri,
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }
    }
}