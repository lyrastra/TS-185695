using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.KudirOsno.Client.DtoWrappers;
using Moedelo.KudirOsno.Client.Postings.Dto;
using System.Threading.Tasks;

namespace Moedelo.KudirOsno.Client.TaxRemains
{
    [InjectAsSingleton]
    public class IpOsnoTaxRemainsClient : BaseCoreApiClient, IIpOsnoTaxRemainsClient
    {
        private readonly ISettingRepository settingRepository;

        public IpOsnoTaxRemainsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
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
            return settingRepository.Get("KudirOsnoApiEndpoint").Value;
        }

        public async Task<TaxRemainsDto> GetAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var url = $"/api/v1/TaxRemains";
            var response = await GetAsync<ApiDataDto<TaxRemainsDto>>(url, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task SaveAsync(int firmId, int userId, TaxRemainsDto dto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var url = $"/api/v1/TaxRemains";
            await PutAsync(url, dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var url = $"/api/v1/TaxRemains";
            await DeleteAsync(url, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}
