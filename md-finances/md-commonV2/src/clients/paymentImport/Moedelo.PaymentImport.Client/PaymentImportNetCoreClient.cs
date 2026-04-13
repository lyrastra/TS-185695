using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client
{
    [InjectAsSingleton]
    public class PaymentImportNetCoreClient : BaseCoreApiClient, IPaymentImportNetCoreClient
    {
        private readonly SettingValue endpoint;

        public PaymentImportNetCoreClient(
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

        public async Task<ImportStatusDto> CheckImportFileAsync(int firmId, int userId, ImportFromUserDto data)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await PostAsync<ImportFromUserDto, ImportStatusDto>(
                "/private/api/v1/PaymentImport/CheckImportFile",
                data,
                setting: new HttpQuerySetting(TimeSpan.FromMinutes(10)),
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return result;
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }
    }
}
