using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.Docs.PurchasesCommissionAgentReports;
using Moedelo.Docs.Dto.ProductMerge;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.PurchasesCommissionAgentReports
{
    [InjectAsSingleton]
    public class PurchasesCommissionAgentReportsApiClient : BaseCoreApiClient, IPurchasesCommissionAgentReportsApiClient
    {
        private readonly SettingValue apiEndpoint;
        private const string dateFormat = "yyyy-MM-dd";

        public PurchasesCommissionAgentReportsApiClient(
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
            apiEndpoint = settingRepository.Get("CommissionAgentReportsApiEndpoint");
        }

        public async Task<IReadOnlyCollection<CommissionAgentReportShortDto>> GetByCriteriaAsync(int firmId,
            int userId,
            CommissionAgentReportRequestDto requestDto, CancellationToken cancellationToken)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId, cancellationToken).ConfigureAwait(false);

            var result = await PostAsync<CommissionAgentReportRequestDto, TableApiPageResult<CommissionAgentReportShortDto>>(
                "/private/api/v1/Purchases/GetByCriteria",
                requestDto,
                queryHeaders: tokenHeaders, cancellationToken: cancellationToken);

            return result.data;
        }

        public async Task<IReadOnlyCollection<CommissionAgentReportWithItemsDto>> GetWithItemsByBaseIdsAsync(int firmId,
            int userId, CommissionAgentReportByBaseIdsRequestDto requestDto)
        {
            if (requestDto?.BaseIds?.Any() != true)
            {
                return Array.Empty<CommissionAgentReportWithItemsDto>();
            }
            
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<CommissionAgentReportByBaseIdsRequestDto, ApiDataResult<List<CommissionAgentReportWithItemsDto>>>(
                $"/private/api/v1/Purchases/WithItems/GetByBaseIds",
                requestDto,
                queryHeaders: tokenHeaders
            );

            return result.data;
        }

        public async Task<IReadOnlyCollection<long>> MergeProductsAsync(int firmId, int userId, ProductMergeRequestDto requestDto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<ProductMergeRequestDto, ApiDataResult<long[]>>(
                $"/private/api/v1/Purchases/MergeProducts",
                requestDto,
                queryHeaders: tokenHeaders
            );

            return result.data;
        }

        public async Task<IReadOnlyCollection<CommissionAgentReportAsReasonDocumentDto>> GetAsReasonDocumentsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || !documentBaseIds.Any())
            {
                return Array.Empty<CommissionAgentReportAsReasonDocumentDto>();
            }

            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<IReadOnlyCollection<long>, ApiDataResult<IReadOnlyCollection<CommissionAgentReportAsReasonDocumentDto>>>(
                $"/private/api/v1/Purchases/GetAsReasonDocuments",
                documentBaseIds,
                queryHeaders: tokenHeaders
            ).ConfigureAwait(false);

            return result.data;
        }

        public async Task<IReadOnlyCollection<ReasonDocumentsAutocompleteResponseDto>> GetReasonDocumentsAutocompleteAsync(int firmId, int userId, ReasonDocumentsAutocompleteRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await GetAsync<ApiDataResult<IReadOnlyCollection<ReasonDocumentsAutocompleteResponseDto>>>(
                $"/api/v1/Purchases/ReasonDocumentsAutocomplete",
                new
                {
                    request.Query,
                    request.Count,
                    request.KontragentId,
                    request.AvailableSum,
                    DocumentDate = request.DocumentDate?.ToString(dateFormat),
                },
                queryHeaders: tokenHeaders
            ).ConfigureAwait(false);

            return result.data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}