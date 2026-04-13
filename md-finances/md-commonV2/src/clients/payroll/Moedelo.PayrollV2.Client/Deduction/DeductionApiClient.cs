using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Deduction;

namespace Moedelo.PayrollV2.Client.Deduction
{
    [InjectAsSingleton]
    public class DeductionApiClient: BaseApiClient, IDeductionApiClient
    {
        private readonly SettingValue apiEndPoint;

        public DeductionApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
            uriCreator, 
            responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Deductions";
        }

        public Task<List<WorkerDeductionDto>> CalculateByWorkerId(int firmId, int userId, int workerId)
        {
            return GetAsync<List<WorkerDeductionDto>>($"/WorkerDeductions/{workerId}", new { firmId, userId });
        }

        public Task<List<WorkerDeductionDto>> CalculateForFirmByPeriod(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<WorkerDeductionDto>>($"/FirmWorkersDeductions", new { firmId, userId, startDate, endDate });
        }
    }
}