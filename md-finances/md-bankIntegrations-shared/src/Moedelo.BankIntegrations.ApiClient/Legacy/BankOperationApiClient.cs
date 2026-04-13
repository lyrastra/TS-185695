using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Legacy;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.InitIntegration;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using System;

namespace Moedelo.BankIntegrations.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IBankOperationApiClient))]
    public class BankOperationApiClient : BaseApiClient, IBankOperationApiClient
    {
        private const string ControllerName = "BankOperation";

        public BankOperationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BankOperationApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApi"),
                logger)
        {
        }

        public async Task<RequestMovementListResponseDto> RequestMovements(RequestMovementListRequestDto dto)
        {
            return await PostAsync<RequestMovementListRequestDto, RequestMovementListResponseDto>($"/{ControllerName}/RequestMovements", dto);
        }

        public async Task<IntegrationTurnResponseDto> IntegrationTurn(IntegrationTurnRequestDto dto)
        {
            return await PostAsync<IntegrationTurnRequestDto, IntegrationTurnResponseDto>($"/{ControllerName}/IntegrationTurn", dto);
        }

        public async Task<SendPaymentOrderResponseDto> SendPaymentOrdersAsync(SendPaymentOrderRequestDto dto)
        {
            var response = await PostAsync<SendPaymentOrderRequestDto, IntegrationResponseDto<SendPaymentOrderResponseDto>>(
                $"/{ControllerName}/SendPaymentOrders", dto, setting: new HttpQuerySetting(timeout: TimeSpan.FromMinutes(2)));
            return response.Data;
        }
    }
}
