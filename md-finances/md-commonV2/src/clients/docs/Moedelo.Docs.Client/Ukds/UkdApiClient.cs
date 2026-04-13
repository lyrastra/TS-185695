using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto;
using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.Ukd;
using Moedelo.Docs.Dto.Upd;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.Ukds
{
    [InjectAsSingleton]
    public class UkdApiClient : BaseCoreApiClient, IUkdApiClient
    {
        private const string prefix = "/api/v1";
        private const string DateOnlyFormat = "yyyy-MM-dd";

        private readonly SettingValue apiEndpoint;

        public UkdApiClient(
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
            apiEndpoint = settingRepository.Get("UkdApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
        
        public async Task<List<UkdDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return new List<UkdDto>();
            }

            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await PostAsync<IEnumerable<long>, ApiDataResult<List<UkdDto>>>(
                $"{prefix}/Ukd/GetByBaseIds?firmId={firmId}&userId={userId}", baseIds, queryHeaders: tokenHeaders);
            return result.data;
        }
        
        public async Task<List<UkdDto>> GetWithoutRefundPaymentsAsync(int firmId, int userId, DateTime startDate,
            DateTime endDate)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await GetAsync<ApiDataResult<List<UkdDto>>>(
                    $"{prefix}/Ukd/GetWithoutRefundPayments",
                    new
                    {
                        firmId,
                        userId,
                        startDate = startDate.ToString(DateOnlyFormat),
                        endDate = endDate.ToString(DateOnlyFormat)
                    },
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            return result.data;
        }
        
        public async Task<IList<UkdCriteriaTableItemDto>> GetByCriteriaAsync(int firmId, int userId,
            UkdCriteriaRequestDto request, CancellationToken cancellationToken)
        {
            var uri = $"{prefix}/Ukd/GetByCriteria";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, cancellationToken).ConfigureAwait(false);
            var response = await PostAsync<UkdCriteriaRequestDto, ApiDataResult<IList<UkdCriteriaTableItemDto>>>(
                uri, request,
                queryHeaders: tokenHeaders, cancellationToken: cancellationToken).ConfigureAwait(false);

            return response.data;
        }
        
        public async Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, long id, bool useStampAndSign)
        {
            var path = $"UkdDownload/{id}/pdf?useStampAndSign={useStampAndSign}";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            
            return await GetFileAsync(
                $"{prefix}/{path}",
                queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return;
            }

            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync<IEnumerable<long>>(
                $"{prefix}/Ukd/DeleteByIds?firmId={firmId}&userId={userId}", baseIds, queryHeaders: tokenHeaders);
        }

        public async Task<List<long>> GetProductsIdsInUKDsAsync(int firmId, int userId, ProductsIdsInUKDRequest request)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return await PostAsync<ProductsIdsInUKDRequest, List<long>>(
                $"{prefix}/Ukd/GetProductsIdsInUKDs?firmId={firmId}&userId={userId}", request, queryHeaders: tokenHeaders);
        }
    }
}