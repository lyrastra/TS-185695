using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.ProductMerge;
using Moedelo.Docs.Dto.PurchasesCurrencyInvoices;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.PurchasesCurrencyInvoices
{
    [InjectAsSingleton(typeof(IPurchasesCurrencyInvoicesApiClient))]
    public class PurchasesCurrencyInvoicesApiClient : BaseCoreApiClient, IPurchasesCurrencyInvoicesApiClient
    {
        private readonly ISettingRepository settingRepository;
        private const string prefix = "/api/v1/Purchases";

        public PurchasesCurrencyInvoicesApiClient(
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

        public async Task<PurchasesCurrencyInvoiceDto[]> GetByPeriodAsync(int firmId, int userId,
            PeriodRequestDto request, CancellationToken cancellationToken)
        {
            var uri = $"/private{prefix}/GetByPeriod";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, cancellationToken).ConfigureAwait(false);
            var response = await PostAsync<PeriodRequestDto, ApiDataResult<PurchasesCurrencyInvoiceDto[]>>(
                uri, request,
                queryHeaders: tokenHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

            return response.data;
        }

        public async Task ReprovideAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.Count == 0)
            {
                return;
            }

            var uri = $"/private{prefix}/Provide";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync(uri, documentBaseIds, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task MergeItemsAsync(int firmId, int userId, ProductMergeRequestDto mergeRequest)
        {
            var uri = $"/private{prefix}/MergeProducts";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync(uri, mergeRequest, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}