using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.KontragentsV2.Dto.KontragentSummary;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    [InjectAsSingleton]
    public class KontragentSummaryClient : BaseApiClient, IKontragentSummaryClient
    {
        private readonly SettingValue apiEndpoint;

        public KontragentSummaryClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("KontragentsPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<FirmKontragentSumSummaryDto>> GetTopSellersAsync(IReadOnlyCollection<int> firmIds, int count, DateTime startDate, DateTime endDate)
        {
            var dto = new KontragentSummaryRequestDto
            {
                FirmIds = firmIds,
                StartDate = startDate,
                EndDate = endDate,
                Count = count
            };
            return PostAsync<KontragentSummaryRequestDto, List<FirmKontragentSumSummaryDto>>("/V2/GetTopSellers", dto);
        }

        public Task<List<FirmKontragentSumSummaryDto>> GetTopCustomersAsync(IReadOnlyCollection<int> firmIds, int count, DateTime startDate, DateTime endDate)
        {
            var dto = new KontragentSummaryRequestDto
            {
                FirmIds = firmIds,
                StartDate = startDate,
                EndDate = endDate,
                Count = count
            };
            return PostAsync<KontragentSummaryRequestDto, List<FirmKontragentSumSummaryDto>>("/V2/GetTopCustomers", dto);
        }
    }
}