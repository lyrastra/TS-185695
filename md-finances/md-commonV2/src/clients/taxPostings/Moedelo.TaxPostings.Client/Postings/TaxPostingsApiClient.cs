using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.TaxPostings.Client.Postings.Money.Extensions;
using Moedelo.TaxPostings.Dto.Postings.Dto;
using System.Threading.Tasks;

namespace Moedelo.TaxPostings.Client.Postings
{
    [InjectAsSingleton(typeof(ITaxPostingsApiClient))]
    internal sealed class TaxPostingsApiClient : BaseCoreApiClient, ITaxPostingsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public TaxPostingsApiClient(
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
            this.apiEndpoint = settingRepository.Get("TaxPostingsRestApiEndpoint")
                .ThrowExceptionIfNull(true);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<ITaxPostingsResponseDto<ITaxPostingDto>> GetByDocumentIdAsync(int firmId, int userId, long baseId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<dynamic>(
                $"/api/v1/Postings/{baseId}", queryHeaders: tokenHeaders).ConfigureAwait(false);

            return TaxPostingsResponseMapper.MapToDto(response);
        }
    }
}