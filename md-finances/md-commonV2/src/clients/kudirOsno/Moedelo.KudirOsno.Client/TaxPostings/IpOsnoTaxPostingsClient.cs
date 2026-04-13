using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.KudirOsno.Client.DtoWrappers;
using Moedelo.KudirOsno.Client.Postings;
using Moedelo.KudirOsno.Client.TaxPostings.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.KudirOsno.Client.TaxPostings
{
    [InjectAsSingleton]
    public class IpOsnoTaxPostingsClient : BaseCoreApiClient, IIpOsnoTaxPostingsClient
    {
        private readonly ISettingRepository settingRepository;

        public IpOsnoTaxPostingsClient(
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

        public async Task<PaymentTaxPostingsResponseDto> GetByPaymentBaseIdAsync(int firmId, int userId, long paymentBaseId)
        {
            var url = $"/api/v1/TaxPostings/Payments/{paymentBaseId}";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataDto<PaymentTaxPostingsResponseDto>>(url, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task<DocumentTaxPostingsResponseDto> GetByDocumentBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            var url = $"/api/v1/TaxPostings/Documents/{documentBaseId}";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataDto<DocumentTaxPostingsResponseDto>>(url, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
        
        public async Task<TaxSumsDto[]> GetPaymentsTaxSumsAsync(int firmId, int userId, IReadOnlyCollection<long> paymentBaseIds)
        {
            var url = $"/api/v1/TaxPostings/Payments/TaxSums";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<IReadOnlyCollection<long>, ApiDataDto<TaxSumsDto[]>>(url, paymentBaseIds, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }
    }
}
