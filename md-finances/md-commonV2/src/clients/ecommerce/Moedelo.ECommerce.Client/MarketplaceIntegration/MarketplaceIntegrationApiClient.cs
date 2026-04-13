using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Marketplaces;
using Moedelo.ECommerce.Client.DtoWrappers;
using Moedelo.ECommerce.Dto.MarketplaceIntegration;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;

namespace Moedelo.ECommerce.Client.MarketplaceIntegration
{
    [InjectAsSingleton]
    public class MarketplaceIntegrationApiClient : BaseCoreApiClient, IMarketplaceIntegrationApiClient
    {
        private const string MarketplaceIntegrationController = "/api/v1/MarketplaceIntegration";
        
        private readonly ISettingRepository settingRepository;
        
        public MarketplaceIntegrationApiClient(
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
            this.settingRepository = settingRepository;
        }
        
        protected override string GetApiEndpoint()
        {
            return settingRepository.Get("ECommerceApiEndpoint").Value + MarketplaceIntegrationController;
        }
        
        public async Task<IntegrationManagementResultDto> AddAsync(int firmId, int userId, MarketplaceType marketplace, string credentialsJson)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            
            var result = await PostAsync<string, DataDto<IntegrationManagementResultDto>>(
                    $"/Add?marketplace={(int)marketplace}", 
                    credentialsJson,
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            if (result?.Data == null)
                throw new Exception("ECommerceApiEndpoint MarketplaceIntegration Add must return data");

            return result.Data;
        }

        public async Task<IntegrationManagementResultDto> UpdateAsync(int firmId, int userId, MarketplaceType marketplace, string credentialsJson)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<string, DataDto<IntegrationManagementResultDto>>(
                    $"/Update?marketplace={(int)marketplace}",
                    credentialsJson,
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            if (result?.Data == null)
                throw new Exception("ECommerceApiEndpoint MarketplaceIntegration Update must return data");

            return result.Data;
        }

        public async Task<string> GetAsync(int firmId, int userId, MarketplaceType marketplace)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            
            var result = await GetAsync<DataDto<string>>(
                    $"/Get?marketplace={(int)marketplace}",
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            return result?.Data;
        }

        public async Task<bool> TurnAsync(int firmId, int userId, MarketplaceType marketplace, bool status)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<DataDto<bool>>(
                    $"/Turn?marketplace={(int)marketplace}&status={status}", 
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            if (result?.Data == null)
                throw new Exception("ECommerceApiEndpoint MarketplaceIntegration Turn must return data");

            return result.Data;
        }

        public async Task<DateTime?> GetCredentialsExpirationDateAsync(int firmId, int userId, MarketplaceType marketplace)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await GetAsync<DataDto<DateTime?>>(
                    $"/CredentialsExpirationDate?marketplace={(int)marketplace}",
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            return result.Data;
        }

        public async Task<List<IntegrationStatusDto>> GetStatusesAsync(int firmId, int userId, bool isNeedMarketplaceRequest)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await GetAsync<List<IntegrationStatusDto>>(
                    $"/Statuses?isNeedMarketplaceRequest={isNeedMarketplaceRequest}",
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<bool> GetAutoImportAsync(int firmId, int userId, MarketplaceType marketplace)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await GetAsync<DataDto<bool>>(
                    $"/AutoImport?marketplace={(int)marketplace}", 
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            return result.Data;
        }
        
        public async Task<int> SetAutoImportAsync(int firmId, int userId, MarketplaceType marketplace, bool status)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);

            var result = await PostAsync<DataDto<int>>(
                    $"/AutoImport?marketplace={(int)marketplace}&status={status}", 
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);

            return result.Data;
        }
    }
}