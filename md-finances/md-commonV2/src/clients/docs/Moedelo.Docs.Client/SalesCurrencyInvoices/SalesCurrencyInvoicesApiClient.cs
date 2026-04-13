using System.Threading;
using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.ProductMerge;
using Moedelo.Docs.Dto.SalesCurrencyInvoices;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.SalesCurrencyInvoices
{
    [InjectAsSingleton]
    public class SalesCurrencyInvoicesApiClient : BaseCoreApiClient, ISalesCurrencyInvoicesApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1/Sales";

        public SalesCurrencyInvoicesApiClient(
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
            return settingRepository.Get("CurrencyInvoicesApiEndpoint").Value;
        }

        public async Task<ReportFileDto> GetFileAsync(int firmId, int userId, long id, bool useStampAndSign)
        {
            var path = $"Download/{id}/pdf?useStampAndSign={useStampAndSign}&asFileInfo=true";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<ReportFileDto>>(
                $"{prefix}/{path}",
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }

        public async Task<SalesCurrencyInvoiceDto[]> GetByPeriodAsync(int firmId, int userId, PeriodRequestDto request,
            CancellationToken cancellationToken)
        {
            var uri = $"/private{prefix}/GetByPeriod";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, cancellationToken).ConfigureAwait(false);
            var response = await PostAsync<PeriodRequestDto, ApiDataResult<SalesCurrencyInvoiceDto[]>>(
                uri, request,
                queryHeaders: tokenHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

            return response.data;
        }

        public async Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest)
        {
            var uri = $"/private{prefix}/MergeProducts";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync(uri, mergeRequest, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}