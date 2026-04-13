using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationPartners = Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums.IntegrationPartners;

namespace Moedelo.BankIntegrationsV2.Client.BankOperation
{
    [InjectAsSingleton]
    public class BankOperationClientV2 : BaseApiClient, IBankOperationClientV2
    {
        private const string ControllerName = "/BankOperation/";
        private readonly SettingValue apiEndPoint;

        public BankOperationClientV2(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + ControllerName;
        }

        public async Task<IntegrationTurnResponseDto> IntegrationTurnAsync(IntegrationTurnRequestDto dto, HttpQuerySetting setting = null)
        {
            return (await PostAsync<IntegrationTurnRequestDto, DataResponseWrapper<IntegrationTurnResponseDto>>("IntegrationTurn", dto, setting: setting)
                .ConfigureAwait(false)).Data;
        }

        public async Task<SendPaymentOrderResponseData> SendPaymentOrdersAsync(List<PaymentOrderDto> paymentOrders, IntegrationIdentityDto identity)
        {
            var dto = new SendPaymentOrderDto { PaymentOrders = paymentOrders, Identity = identity };
            return (await PostAsync<SendPaymentOrderDto, DataResponseWrapper<SendPaymentOrderResponseData>>("SendPaymentOrders", dto).ConfigureAwait(false)).Data;
        }

        public Task<RequestMovementListResponseDto> RequestMovementListAsync(RequestMovementListRequestDto dto)
        {
            return PostAsync<RequestMovementListRequestDto, RequestMovementListResponseDto>("RequestMovements", dto);
        }

        public Task<SberbankStatementSummaryResponseDto> GetSberbankStatementSummaryAsync(SberbankStatementSummaryRequestDto dto)
        {
            return PostAsync<SberbankStatementSummaryRequestDto, SberbankStatementSummaryResponseDto>("GetSberbankStatementSummary", dto);
        }

        public async Task<bool> SendDailyRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner, bool withoutCheckCreated)
        {
            return (await GetAsync<DataResponseWrapper<bool>>("SendDaylyRequest", 
                new { beginDate = beginDate.ToString("yyyy-MM-dd"),
                    finishDate = finishDate.ToString("yyyy-MM-dd"),
                    integrationPartner,
                    withoutCheckCreated})
                .ConfigureAwait(false))
                .Data;
        }

        public async Task<bool> SendDailyLagRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner)
        {
            return (await GetAsync<DataResponseWrapper<bool>>("SendDaylyLagRequest",
                new
                {
                    beginDate = beginDate.ToString("yyyy-MM-dd"),
                    finishDate = finishDate.ToString("yyyy-MM-dd"),
                    integrationPartner
                })
                .ConfigureAwait(false))
                .Data;
        }

        public async Task<bool> GetReadyMovementListForAllUsersOfIntegration(int integrationPartner)
        {
            return (await GetAsync<DataResponseWrapper<bool>>("GetReadyMovementListForAllUsersOfIntegration",
                new {integrationPartner}).ConfigureAwait(false))
                .Data;
        }

        public async Task<bool> SendReRequestAsync(int integrationPartner, DateTime startDate, DateTime endDate)
        {
            return (await GetAsync<DataResponseWrapper<bool>>("SendReRequest",
                new { integrationPartner,
                    startDate = startDate.ToString("yyyy-MM-dd"),
                    endDate = endDate.ToString("yyyy-MM-dd")})
                .ConfigureAwait(false))
                .Data;
        }
    }
}