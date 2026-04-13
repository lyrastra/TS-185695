using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.PayrollV2.Dto;
using Moedelo.PayrollV2.Dto.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.PayrollV2.Client.Employees
{
    [InjectAsSingleton]
    public class EmployeesForImportApiClient : BaseApiClient, IEmployeesForImportApiClient
    {
        private readonly SettingValue apiEndPoint;

        public EmployeesForImportApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollApi");
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndPoint.Value}/EmployeesRestApi";
        }

        public async Task<List<NotFiredWorkerDto>> GetNotFiredWorkersAsync(int firmId, int userId, DateTime date)

        {
            var results = await PostAsync<object, DataResponse<List<NotFiredWorkerDto>>>($"/GetNotFiredWorkersForImport?firmId={firmId}&userId={userId}", new { date })
                .ConfigureAwait(false);
            return results.Data;
        }

        public async Task<WorkerForPaymentImportModelDto[]> GetAsync(int firmId, int userId)
        {
            var results = await PostAsync<DataResponse<WorkerForPaymentImportModelDto[]>>($"/GetForPaymentImport?firmId={firmId}&userId={userId}")
                .ConfigureAwait(false);
            return results.Data;
        }
    }
}