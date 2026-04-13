using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Worker;

namespace Moedelo.PayrollV2.Client.Validation
{
    [InjectAsSingleton]
    public class ValidationClient : BaseApiClient, IValidationClient
    {
        private readonly SettingValue apiEndPoint;

        public ValidationClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Validation";
        }
        
        public Task<List<WorkerRequisitesCheckDto>> ValidateWorkersForNotFilledRequisitesReportAsync(DateTime startDate, DateTime endDate, int firmId, int userId)
        {
            return GetAsync<List<WorkerRequisitesCheckDto>>(
                "/ValidateWorkersForNotFilledRequisitesReport",
                new { firmId, userId, startDate, endDate });
        }
    }
}