using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Akbars;
using Moedelo.BankIntegrations.ApiClient.BssBanks;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System.Threading.Tasks;
using Moedelo.Common.Logging.ExtraLog.ExtraData;

namespace Moedelo.BankIntegrations.ApiClient.Akbarsbank
{
    [InjectAsSingleton]
    public class AkbarsBankAdapterClient : BssBankAdapterClient, IAkbarsBankAdapterClient
    {
        private readonly ISettingRepository settingRepository;

        public AkbarsBankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AkbarsBankAdapterClient> logger) :
            base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AkbarsbankAdapterEndpoint"),
                logger)
        {
            this.settingRepository = settingRepository;
        }

        public override async Task<ApiDataResult<T>> GetClientInfoAsync<T>(string redirectUri, string dboServerUri, string authCode, HttpQuerySetting setting = null)
        {
            logger.LogInformationExtraData(new { redirectUri, authCode, AkbarsbankAdapterEndpoint = settingRepository.Get("AkbarsbankAdapterEndpoint").Value }, $"GetClientInfoAsync");
            return await base.GetClientInfoAsync<T>(redirectUri, dboServerUri, authCode, setting);
        }

        public override Task<ApiDataResult<int>> SaveIntegrationDataAsync<T>(T data, HttpQuerySetting setting = null)
        {
            logger.LogInformationExtraData(new { AkbarsBankAdapterClient = settingRepository.Get("AkbarsbankAdapterEndpoint").Value }, $"SaveIntegrationDataAsync");
            return base.SaveIntegrationDataAsync(data, setting);
        }

        public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/BankOperation/RequestMovements",
                data: requestDto);

            return response.data;
        }
    }
}
