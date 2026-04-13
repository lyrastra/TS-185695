using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.SzvmReport;

namespace Moedelo.PayrollV2.Client.Reports.Szvm
{
    [InjectAsSingleton]
    public class SzvmReportApiClient : BaseApiClient, ISzvmReportApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SzvmReportApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }
        
        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}/SzvmReport";
        }
        
        public Task<List<WorkerForSzvmDto>> GetWorkersAsync(
            int firmId, int userId, DateTime startDate, DateTime endDate, IReadOnlyCollection<int> ids)
        {
            return PostAsync<WorkerForSzvmRequestDto, List<WorkerForSzvmDto>>(
                $"/GetWorkers?firmId={firmId}&userId={userId}",
                new WorkerForSzvmRequestDto {StartDate = startDate, EndDate = endDate, WorkerIds = ids});
        }
    }
}
