using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.RetailReport;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.RetailReport
{
    [InjectAsSingleton]
    public class RetailReportApiClient : BaseApiClient, IRetailReportApiClient
    {
        private readonly SettingValue apiEndPoint;

        public RetailReportApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<SavedRetailReportDto> CreateAsync(int firmId, int userId, RetailReportDto dto)
        {
            return PostAsync<RetailReportDto, SavedRetailReportDto>($"/RetailReportApi/CreateAsync?firmId={firmId}&userId={userId}", dto, null,
                new HttpQuerySetting
                {
                    Timeout = TimeSpan.FromMinutes(10)
                });
        }

        public Task<List<RetailReportDto>> GetListAsync(
            int firmId, 
            int userId, 
            RetailReportPaginationRequest request)
        {
            return PostAsync<RetailReportPaginationRequest, List<RetailReportDto>>($"/RetailReportApi/GetListAsync?firmId={firmId}&userId={userId}", request);
        }

        public Task<List<RetailReportDto>> GetListV2Async(
            int firmId,
            int userId,
            RetailReportRequest request)
        {
            return PostAsync<RetailReportRequest, List<RetailReportDto>>($"/RetailReportApi/GetListAsync?firmId={firmId}&userId={userId}", request);
        }

        public Task<RetailReportDto> GetByBaseIdAsync(int firmId, int userId, long id)
        {
            return GetAsync<RetailReportDto>($"/RetailReportApi/GetByBaseIdAsync?firmId={firmId}&userId={userId}&baseId={id}");
        }

        public Task<List<RetailReportDto>> GetForPeriodAsync(int firmId, int userId, DateTime? afterDate = null,  DateTime? beforeDate = null)
        {
            var request = new
            {
                afterDate,
                beforeDate
            };
            return PostAsync<object, List<RetailReportDto>>($"/RetailReportApi/GetForPeriod?firmId={firmId}&userId={userId}", request);
        }

        public Task<List<RetailReportDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<RetailReportDto>());
            }
            
            return PostAsync<IReadOnlyCollection<long>, List<RetailReportDto>>($"/RetailReportApi/GetByBaseIds?firmId={firmId}&userId={userId}", baseIds, null,
                new HttpQuerySetting
                {
                    Timeout = TimeSpan.FromMinutes(10)
                });
        }

        public Task<SavedRetailReportDto> UpdateAsync(int firmId, int userId, RetailReportDto dto)
        {
            return PostAsync<RetailReportDto, SavedRetailReportDto>($"/RetailReportApi/Update?firmId={firmId}&userId={userId}", dto);
        }

        public Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return PostAsync($"/RetailReportApi/DeleteByBaseId?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}");
        }
    }
}