using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.RosstatReport;

namespace Moedelo.PayrollV2.Client.Reports.Rosstat
{
    [InjectAsSingleton]
    public class RosstatReportApiClient : BaseApiClient, IRosstatReportApiClient
    {
        private readonly SettingValue apiEndPoint;

        public RosstatReportApiClient(
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
            return $"{apiEndPoint.Value}/RosstatReport";
        }

        public Task<WorkersCountForRosstatDto> GetWorkersCountAsync(int firmId, int userId, int year)
        {
            return GetAsync<WorkersCountForRosstatDto>($"/GetWorkersCount", new {firmId, userId, year});
        }

        public Task<WorkersSalaryForRosstatDto> GetWorkersSalaryAsync(int firmId, int userId, int year)
        {
            return GetAsync<WorkersSalaryForRosstatDto>($"/GetWorkersSalary", new {firmId, userId, year});
        }
    }
}
