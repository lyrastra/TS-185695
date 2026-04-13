using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.ExternalPartner.Robokassa;
using Moedelo.BankIntegrationsV2.Dto.ExternalPartner.RobokassaAcquirer;
using Moedelo.BankIntegrationsV2.Dto.IntegratedFile;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.ExternalPartner
{
    [InjectAsSingleton(typeof(IExternalPartnerClient))]
    public class ExternalPartnerClient : BaseApiClient, IExternalPartnerClient
    {
        private const string ControllerName = "/ExternalPartner/";
        private readonly SettingValue apiEndPoint;

        public ExternalPartnerClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public Task<IntegrationResponseDto<RobokassaTransferFilesResponseDto>> RobokassaTransferFiles(RobokassaTransferFilesRequestDto requestDto)
        {
            return PostAsync<RobokassaTransferFilesRequestDto, IntegrationResponseDto<RobokassaTransferFilesResponseDto>>("RobokassaTransferFiles", requestDto);
        }

        public async Task<Dictionary<string, string>> GetRequisitesForFormByFirmId(int firmId)
        {
            var result = await GetAsync<IntegrationResponseDto<Dictionary<string, string>>>("GetRequisitesForFormByFirmId", new { firmId }).ConfigureAwait(false);
            return result.Data;
        }

        public Task<IntegrationResponseDto<string>> RobokassaGetLinkToPayAsync(RobokassaGetLinkToPayRequestDto requestDto)
        {
            return PostAsync<RobokassaGetLinkToPayRequestDto, IntegrationResponseDto<string>>("RobokassaGetLinkToPay", requestDto);
        }
    }
}
