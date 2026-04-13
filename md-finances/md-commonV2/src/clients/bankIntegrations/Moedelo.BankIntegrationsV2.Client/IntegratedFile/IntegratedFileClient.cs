using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.IntegratedFile;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrationsV2.Client.IntegratedFile
{
    [InjectAsSingleton]
    public class IntegratedFileClient : BaseApiClient, IIntegratedFileClient
    {
        private const string ControllerName = "/IntegratedFiles/";
        private readonly SettingValue apiEndPoint;

        public IntegratedFileClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public async Task<IList<IntegratedFilesDto>> GetIntegratedFilesForFirmAsync(int firmId)
        {
            var queryParams = new { firmId };
            var responseWrapper = await GetAsync<DataResponseWrapper<IList<IntegratedFilesDto>>>("GetIntegratedFilesForFirm", queryParams).ConfigureAwait(false);
            return responseWrapper.Data;
        }

        public async Task<IntegratedFilesDto> GetIntegratedFileForFirmByIdAsync(int integratedFileId, int firmId)
        {
            var queryParams = new { firmId, integratedFileId };
            var responseWrapper = await GetAsync<DataResponseWrapper<IntegratedFilesDto>>("GetIntegratedFileForFirmById", queryParams).ConfigureAwait(false);
            return responseWrapper.Data;
        }

        public async Task<string> MarkIntegratedFileSkippedAsync(int integratedFileId, int firmId)
        {
            var queryParams = new { firmId, integratedFileId };
            var responseWrapper = await GetAsync<DataResponseWrapper<string>>("MarkIntegratedFileSkipped", queryParams).ConfigureAwait(false);
            return responseWrapper.Data;
        }

        public async Task<string> MarkIntegratedFileAddedAsync(int integratedFileId, int firmId)
        {
            var queryParams = new { firmId, integratedFileId };
            var responseWrapper = await GetAsync<DataResponseWrapper<string>>("MarkIntegratedFileAdded", queryParams).ConfigureAwait(false);
            return responseWrapper.Data;
        }

        public async Task<bool> TransferFilesToMdAsync(MDMovementList movementList, int firmId, IntegrationPartners integrationPartner,
            string requestId)
        {
            var response = await PostAsync<MDMovementList, DataResponseWrapper<bool>>($"TransferFilesToMd?firmId={firmId}&integrationPartner={integrationPartner}&requestId={requestId}", movementList).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<List<IntegratedFileGeneralInfoDto>> GetIntegratedFilesGeneralInfoAsync(int firmId)
        {
            var queryParams = new { firmId };
            var response =
                await GetAsync<DataResponseWrapper<List<IntegratedFileGeneralInfoDto>>>($"GetIntegratedFilesGeneralInfo", queryParams)
                    .ConfigureAwait(false);

            return response.Data;
        }

        public Task<IntegrationResponseDto<IntWithNewFilesResponseDto>> GetActiveIntegrationsWithNewFilesAsync(int firmId)
        {
            return GetAsync<IntegrationResponseDto<IntWithNewFilesResponseDto>>(
                $"GetActiveIntegrationsWithNewFiles",
                new {firmId});
        }

        public Task<IntegrationResponseDto<List<IntegratedFileGeneralInfoDto>>> GetIntegratedFilesGeneralInfoForPartnerAsync(
            int firmId,
            IntegrationPartners integrationPartner)
        {
            return GetAsync<IntegrationResponseDto<List<IntegratedFileGeneralInfoDto>>>(
                $"GetIntegratedFilesGeneralInfoForPartner",
                new {firmId, integrationPartner});
        }

        public Task<IntegrationResponseDto<IntegratedFileGeneralInfoDto>> GetLastIntegratedFileByIntegrationPartnerAsync(int firmId, IntegrationPartners integrationPartner)
        {
            return GetAsync<IntegrationResponseDto<IntegratedFileGeneralInfoDto>>(
                $"GetLastIntegratedFileByIntegrationPartner",
                new { firmId, integrationPartner });
        }
    }
}