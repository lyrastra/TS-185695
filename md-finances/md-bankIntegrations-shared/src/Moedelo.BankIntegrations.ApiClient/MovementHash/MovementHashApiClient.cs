using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.MovementHash;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.ApiClient.Dto.MovementHashService;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Logging.ExtraLog.ExtraData;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Infrastructure.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.MovementHash
{
    [InjectAsSingleton]
    public class MovementHashApiClient : BaseApiClient, IMovementHashApiClient
    {
        private readonly ILogger<MovementHashApiClient> logger;
        public MovementHashApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MovementHashApiClient> logger) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MovementHashApiEndpoint"),
                logger)
        {
            this.logger = logger;
        }

        public async Task<bool> SaveMovementHashAsync(List<MovementHashDto> data)
        {
            var response = await PostAsync<List<MovementHashDto>, ApiDataResult<bool>>(
                uri: "/private/api/v1/Hash/Save",
                data: data, 
                setting: new HttpQuerySetting(TimeSpan.FromSeconds(30)));

            return response.data;
        }

        public async Task<List<bool>> CheckMovementHashList(List<MovementHashDto> data)
        {
            try
            {
                var response = await PostAsync<List<MovementHashDto>, ApiDataResult<List<bool>>>(
                    uri: "/private/api/v1/Hash/CheckList",
                    data: data);

                return response.data;
            }
            catch(Exception ex)
            {
                logger.LogErrorExtraData(new { data = data?.ToJsonString() }, ex, $"CheckMovementHashList Error");
                throw;
            }
        }

        public async Task<List<StatisticsOnIncompleteStatementDto>> GetStatisticsOnIncompleteStatementsAsync(DateTime startDate)
        {
            return (await GetAsync<ApiDataResult<List<StatisticsOnIncompleteStatementDto>>>(
             uri: "/private/api/v1/Monitor/GetStatisticsOnIncompleteStatements",
             queryParams: new { startDate = startDate.ToString("yyyy-MM-dd") },
             setting: new HttpQuerySetting(TimeSpan.FromSeconds(20)))).data;
        }
    }
}