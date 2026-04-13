using Moedelo.BankIntegrationsV2.Dto.BankOperation;
using Moedelo.BankIntegrationsV2.Dto.Integrations;
using Moedelo.Common.Enums.Enums.Integration;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrationsV2.Client.BankOperation
{
    [InjectAsSingleton]
    public class BankOperationClient : IBankOperationClient
    {
        private readonly IBankOperationClientV2 bankOperationClientV2;
        private readonly IBankOperationClientNetCore bankOperationClientNetCore;

        public BankOperationClient(IBankOperationClientV2 bankOperationClientV2, IBankOperationClientNetCore bankOperationClientNetCore)
        {
            this.bankOperationClientV2 = bankOperationClientV2;
            this.bankOperationClientNetCore = bankOperationClientNetCore;
        }

        public Task<bool> GetReadyMovementListForAllUsersOfIntegration(int integrationPartner)
        {
            return bankOperationClientV2.GetReadyMovementListForAllUsersOfIntegration(integrationPartner);
        }

        public Task<SberbankStatementSummaryResponseDto> GetSberbankStatementSummaryAsync(SberbankStatementSummaryRequestDto dto)
        {
            return bankOperationClientV2.GetSberbankStatementSummaryAsync(dto);
        }

        public Task<IntegrationTurnResponseDto> IntegrationTurnAsync(IntegrationTurnRequestDto dto, HttpQuerySetting setting = null)
        {
            return bankOperationClientV2.IntegrationTurnAsync(dto, setting);
        }

        public async Task<RequestMovementListResponseDto> RequestMovementListAsync(RequestMovementListRequestDto dto)
        {
            var response = await bankOperationClientNetCore.RequestMovementListAsync(dto).ConfigureAwait(false);

            return new RequestMovementListResponseDto
            {
                Message = response.Message,
                RequestId = response.IntegrationRequestId.ToString(),
                Status = response.IsSuccess ? IntegrationResponseStatusCode.Ok : IntegrationResponseStatusCode.Error
            };
        }

        public Task<bool> SendDailyRequestAsync(DateTime beginDate, DateTime finishDate, int integrationPartner, bool withoutCheckCreated = false)
        {
            return bankOperationClientNetCore.SendDailyRequestAsync(beginDate, finishDate, integrationPartner, withoutCheckCreated);
        }

        public Task<SendPaymentOrderResponseData> SendPaymentOrdersAsync(List<PaymentOrderDto> paymentOrders, IntegrationIdentityDto identity)
        {
            return bankOperationClientV2.SendPaymentOrdersAsync(paymentOrders, identity);
        }

        public Task<bool> SendReRequestAsync(int partner, DateTime startDate, DateTime endDate)
        {
            return bankOperationClientV2.SendReRequestAsync(partner, startDate, endDate);
        }
    }
}
