using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Dto;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports;
using Moedelo.Docs.ApiClient.Abstractions.PurchasesCommissionAgentReports.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Docs.ApiClient.PurchasesCommissionAgentReports
{
    [InjectAsSingleton(typeof(IPurchasesCommissionAgentReportsApiClient))]
    internal class PurchasesCommissionAgentReportsApiClient : BaseApiClient, IPurchasesCommissionAgentReportsApiClient
    {
        public PurchasesCommissionAgentReportsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurchasesCommissionAgentReportsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("CommissionAgentReportsApiEndpoint"),
                logger)
        {
        }

        public async Task<CommissionAgentReportResponseDto> GetByDocumentBaseIdAsync(long baseId)
        {
            var url = $"/api/v1/Purchases/{baseId}";
            var response = await GetAsync<ApiDataDto<CommissionAgentReportResponseDto>>(url);
            return response.data;
        }

        public async Task<long> Save(CommissionAgentReportSaveRequestDto saveRequest, HttpQuerySetting querySetting = null)
        {
            var result = await PostAsync<CommissionAgentReportSaveRequestDto, DataResponse<IdResponseDto>>(
                $"/api/v1/Purchases",
                saveRequest, setting: querySetting);
            return result.Data.Id;
        }

        public Task UpdateAsync(long baseId, CommissionAgentReportSaveRequestDto dto)
        {
            return PutAsync<CommissionAgentReportSaveRequestDto, DataResponse<IdResponseDto>>(
                $"/api/v1/Purchases/{baseId}",
                dto);
        }

        public async Task<IReadOnlyCollection<CommissionAgentReportWithItemsPrivateDto>> GetPrivateByDocumentBaseIdsAsync(CommissionAgentReportByBaseIdsRequestDto requestDto)
        {
            if (requestDto?.BaseIds?.Any() != true)
            {
                return Array.Empty<CommissionAgentReportWithItemsPrivateDto>();
            }

            var result = await PostAsync<CommissionAgentReportByBaseIdsRequestDto, DataResponse<List<CommissionAgentReportWithItemsPrivateDto>>>(
                $"/private/api/v1/Purchases/WithItems/GetByBaseIds",
                requestDto
            );

            return result.Data;
        }

        public async Task<IReadOnlyCollection<CommissionAgentReportPrivateDto>> GetByCriteriaAsync(CommissionAgentReportRequestDto requestDto)
        {
            var result = await PostAsync<CommissionAgentReportRequestDto, DataPageResponse<CommissionAgentReportPrivateDto>>(
                $"/private/api/v1/Purchases/GetByCriteria",
                requestDto
            );

            return result.Data;
        }

        public async Task<IReadOnlyCollection<CommissionAgentReportForRefundsResponseDto>> GetForRefundsAsync(CommissionAgentReportForRefundsRequestDto requestDto)
        {
            var result = await PostAsync<CommissionAgentReportForRefundsRequestDto, DataPageResponse<CommissionAgentReportForRefundsResponseDto>>(
                $"/private/api/v1/Purchases/GetForRefunds",
                requestDto
            );

            return result.Data;
        }
    }
}
