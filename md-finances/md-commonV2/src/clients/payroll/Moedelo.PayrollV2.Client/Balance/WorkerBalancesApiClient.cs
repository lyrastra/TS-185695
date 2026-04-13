using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PayrollV2.Dto.Balance;

namespace Moedelo.PayrollV2.Client.Balance
{
    [InjectAsSingleton]
    public class WorkerBalancesApiClient : BaseApiClient, IWorkerBalancesApiClient
    {
        private readonly SettingValue apiEndPoint;

        public WorkerBalancesApiClient(
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
            auditTracer, 
            auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/WorkerBalances";
        }

        public Task<IEnumerable<WorkerBalancesDto>> GetAsync(int userId, int firmId, DateTime date)
        {
            return GetAsync<IEnumerable<WorkerBalancesDto>>("", new { firmId, userId, date });
        }        
    }
}
