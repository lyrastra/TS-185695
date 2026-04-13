using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Uralsibbank;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.Dto.UralsibbankSso;
using IntegrationDataDto = Moedelo.BankIntegrations.Dto.UralsibbankSso.IntegrationDataDto;
using Moedelo.BankIntegrations.Dto.Movements;

namespace Moedelo.BankAdapters.ApiClient.Uralsibbank
{
    [InjectAsSingleton]
    public class UralsibbankAdapterClient : BaseApiClient, IUralsibbankAdapterClient
    {
        public UralsibbankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UralsibbankAdapterClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("UralsibBankAdapterEndpoint"),
                  logger)
        {
        }

        public async Task<UralsibClientInfoDto> GetClientInfoAsync(string code, string redirectUri)
        {
            var response = await GetAsync<ApiDataResult<UralsibClientInfoDto>>(
               uri: "/sso/GetClientInfo",
               queryParams: new { redirectUri, code },
               setting: new HttpQuerySetting(TimeSpan.FromMinutes(1)));

            return response.data;
        }

        public async Task<int> SaveIntegrationDataAsync(IntegrationDataDto integrationDataDto)
        {
            var response = await PutAsync<IntegrationDataDto, ApiDataResult<int>>(
                  uri: "/sso/SaveIntegrationData",
                  data: integrationDataDto);

            return response.data;
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
