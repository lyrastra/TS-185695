using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client.Reconciliation
{
    [InjectAsSingleton]
    public class ReconciliationResultClient : BaseCoreApiClient, IReconciliationResultClient
    {
        private readonly SettingValue endpoint;

        public ReconciliationResultClient(
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
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            endpoint = settingRepository.Get("PaymentImportHandlerApiEndpoint");
        }

        public Task InsertAsync(ReconciliationResultDto dto)
        {
            return PostAsync("", dto);
        }

        public Task UpdateAsync(ReconciliationResultDto dto)
        {
            return PutAsync("", dto);
        }

        public Task<ReconciliationResultDto> GetAsync(Guid sessionId, int firmId)
        {
            return GetAsync<ReconciliationResultDto>("", new { sessionId, firmId });
        }
        
        protected override string GetApiEndpoint()
        {
            return endpoint.Value + "/private/api/v1/ReconciliationResult";
        }
    }
}