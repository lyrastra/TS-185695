using Moedelo.BankIntegrations.ApiClient.Dto.AccountRequest;
using Moedelo.BankIntegrations.ApiClient.Dto.Sberbank;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperations;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.Sberbank
{
    [InjectAsSingleton(typeof(IIntegrationAccountsApiClient))]
    public class IntegrationAccountsApiClient : BaseCoreApiClient, IIntegrationAccountsApiClient
    {
        private const string SberUri = "/private/api/v1/schedule";
        private readonly SettingValue endpoint;

        public IntegrationAccountsApiClient(
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
            endpoint = settingRepository.Get("IntegrationApiNetCore");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task ScheduleAccountsAndMovementsFetchAsync(AccountRequestDto dto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(dto.FirmId, dto.UserId).ConfigureAwait(false);
            await PostAsync<AccountRequestDto>($"{SberUri}/request-accounts", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        public async Task ScheduleFillingRequisitesAsync(FillingByInnRequestDto dto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(dto.FirmId, dto.UserId).ConfigureAwait(false);
            await PostAsync<FillingByInnRequestDto>($"{SberUri}/filling-requisites", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
        }
    }
}