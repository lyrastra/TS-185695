using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Rsv;
using Moedelo.PayrollV2.Dto.Szv;

namespace Moedelo.PayrollV2.Client.Reports.SzvExperience
{
    [InjectAsSingleton]
    public class SzvExperienceReportApiClient : BaseApiClient, ISzvExperienceReportApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public SzvExperienceReportApiClient(
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
            return $"{apiEndPoint.Value}/SzvExperienceReport";
        }
        
        public Task<List<SzvWorkerModelDto>> GetWorkersAsync(int firmId, int userId, int year)
        {
            return GetAsync<List<SzvWorkerModelDto>>("/GetWorkers", new { firmId, userId, year });
        }
        
        public Task<List<SzvValidationResultDto>> GetPeriodErrorsListAsync(int firmId, int userId, int year)
        {
            return GetAsync<List<SzvValidationResultDto>>("/GetPeriodErrors", new { firmId, userId, year });
        }

        public Task<List<WorkerInfoCheckDto>> GetWorkersForCheckAsync(int firmId, int userId, int year)
        {
            return GetAsync<List<WorkerInfoCheckDto>>("/GetWorkersForCheck", new { firmId, userId, year });
        }
    }
}