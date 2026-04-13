using System.Threading;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationError;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationError;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.integrationError
{
    [InjectAsSingleton(typeof(IIntegrationErrorApiClient))]
    public class IntegrationErrorApiClient : BaseCoreApiClient, IIntegrationErrorApiClient
    {
        private readonly SettingValue endpoint;
        private const string ControllerName = "/private/api/v1/IntegrationError";
        
        public IntegrationErrorApiClient(
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
            this.endpoint = settingRepository.Get("IntegrationApiNetCore");
        }
        
        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }
        
        /// <summary>
        /// Обновить существующую или создать новую запись об ошибке по тексту ошибки
        /// </summary>
        /// <param name="dto"></param>
        public async Task MergeFromErrorStringAsync(MergeFromErrorIntegrationErrorRequestDto dto)
        {
            await PostAsync($"{ControllerName}/MergeFromErrorString", dto).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Обновить существующую или создать новую запись об ошибке по расчетному счету
        /// </summary>
        public async Task MergeFromGetStatementResultAsync(MergeFromGetStatementErrorRequestDto dto)
        {
            await PostAsync($"{ControllerName}/MergeFromGetStatementResult", dto).ConfigureAwait(false);
        }
        
        public Task ReadUnreadByIdsAsync(ReadUnreadIntegrationErrorRequestDto dto)
        {
            return PutAsync($"{ControllerName}/readUnreadByIdsAsync", dto);
        }
        
        public Task<IntegrationErrorListDto> GetListAsync(GetListIntegrationErrorRequestDto dto, CancellationToken ctx)
        {
            return GetAsync<IntegrationErrorListDto>($"{ControllerName}/GetList", dto, cancellationToken: ctx);
        }
    }
}