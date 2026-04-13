using System;
using System.Threading;
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
    public class PaymentImportClient : BaseApiClient, IPaymentImportClient
    {
        private readonly SettingValue apiEndpoint;

        public PaymentImportClient(
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
            apiEndpoint = settingRepository.Get("PaymentImportApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<ImportStatusDto> ImportFromUserAsync(int firmId, int userId, ImportFromUserDto data)
        {
            var httpSettings = new HttpQuerySetting { Timeout = new TimeSpan(0, 10, 0) };
            return PostAsync<ImportFromUserDto, ImportStatusDto>($"/ImportFromUser?firmId={firmId}&userId={userId}", data, null, httpSettings);
        }

        public Task<ImportStatusDto> ImportFromUserAsync(int firmId, int userId, string fileId, bool processSettlementAccount, bool checkDocuments)
        {
            var httpSettings = new HttpQuerySetting { Timeout = new TimeSpan(0, 10, 0) };
            return PostAsync<ImportStatusDto>($"/ImportFromUser?firmId={firmId}&userId={userId}&fileId={fileId}&processSettlementAccount={processSettlementAccount}&checkDocuments={checkDocuments}", null, httpSettings);
        }

        public Task ImportFromIntegrationAsync(int firmId, int userId, string fileId, bool isManual)
        {
            var httpSettings = new HttpQuerySetting { Timeout = new TimeSpan(0, 10, 0) };
            return PostAsync($"/ImportFromIntegration?firmId={firmId}&userId={userId}&fileId={fileId}&isManual={isManual}", null, httpSettings);
        }

        public Task ImportFromRobokassaAsync(int firmId, int userId, int fileId)
        {
            return PostAsync($"/ImportFromRobokassa?firmId={firmId}&userId={userId}&fileId={fileId}");
        }

        public Task<string> GetImportMessagesAsync(int firmId, int userId, CancellationToken ctx)
        {
            return GetAsync<string>("/GetImportMessages", new { firmId, userId }, cancellationToken: ctx);
        }

        public Task<ImportStatusDto> ImportFromReconcilationAsync(int firmId, int userId, ReconcilationImportDto dto)
        {
            var httpSettings = new HttpQuerySetting { Timeout = new TimeSpan(0, 10, 0) };
            return PostAsync<ReconcilationImportDto, ImportStatusDto>($"/ImportFromReconcilation?firmId={firmId}&userId={userId}", dto, null, httpSettings);
        }
    }
}