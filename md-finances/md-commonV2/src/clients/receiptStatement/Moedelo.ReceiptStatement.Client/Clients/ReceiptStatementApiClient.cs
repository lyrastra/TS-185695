using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.ReceiptStatement.Dto;

namespace Moedelo.ReceiptStatement.Client.Clients
{
    [InjectAsSingleton]
    public class ReceiptStatementApiClient : BaseCoreApiClient, IReceiptStatementApiClient
    {
        private readonly ISettingRepository settingRepository;

        public ReceiptStatementApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, settingRepository, auditTracer, auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        public async Task<ReceiptStatementDto> GetAsync(int firmId, int userId, long documentBaseId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<ReceiptStatementDto>>($"/api/v1/{documentBaseId}", queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await DeleteAsync($"/api/v1/{documentBaseId}", queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("ReceiptStatementApiEndpoint").Value;
        }

        public async Task<List<ReceiptStatementBySubcontoDto>> GetBaseIdsBySubcontoIdsAsync(int firmId, int userId, IReadOnlyCollection<long> subcontoIds)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var url = "/api/v1/GetBaseIdsBySubcontoIds";
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<List<ReceiptStatementBySubcontoDto>>>(url, subcontoIds, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
