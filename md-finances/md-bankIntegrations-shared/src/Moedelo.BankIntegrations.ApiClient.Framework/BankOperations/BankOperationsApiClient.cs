using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Accounts;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.Invoice;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.BankOperations;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.BankOperations
{
    [InjectAsSingleton(typeof(IBankOperationsApiClient))]
    public class BankOperationsApiClient: BaseCoreApiClient, IBankOperationsApiClient
    {
        private readonly SettingValue endpoint;

        public BankOperationsApiClient(
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
            this.endpoint = settingRepository.Get("IntegrationApiNetCore");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task<IntegrationResponseDto<SendBankInvoiceResponseDto>> SendInvoiceAsync(int firmId, int userid, SendBankInvoiceRequestDto dto)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userid).ConfigureAwait(false);
            var response = await PostAsync<SendBankInvoiceRequestDto, IntegrationResponseDto<SendBankInvoiceResponseDto>>(
                "/private/api/v1/BankOperation/SendInvoice", dto, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response;
        }
        
        public async Task<IntegrationResponseDto<List<InvoiceDetailResponseDto>>> GetListInvoiceDetailByDocumentBaseIdsAsync(int firmId, int userid, IReadOnlyCollection<long> documentBaseIds)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userid).ConfigureAwait(false);
            var response = await PostAsync<IReadOnlyCollection<long>,IntegrationResponseDto<List<InvoiceDetailResponseDto>>>(
                "/private/api/v1/BankOperation/GetListInvoiceDetailByDocumentBaseIds",
                documentBaseIds,
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response;
        }

        public async Task<IntegrationResponseDto<bool>> GetHaveInvoiceAccessAsync(int userid, int firmId, int partner)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userid).ConfigureAwait(false);
            var response = await GetAsync<IntegrationResponseDto<bool>>(
                "/private/api/v1/BankOperation/GetHaveInvoiceAccess",
                new { firmId, partner},
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response;
        }

        public async Task<GetAccountsResponseDto> GetAccountsAsync(int userId, int firmId,  IntegrationPartners partner)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<ApiDataResult<GetAccountsResponseDto>>(
                "/private/api/v1/BankOperation/GetAccounts",
                new { firmId, partner},
                queryHeaders: tokenHeaders).ConfigureAwait(false);
            return response.data;
        }

        public async Task RequestMovementsAfterTurnIntegrationByFirmAsync(RequestMovementForAllSettlementsDto dto)
        {
            await PostAsync($"/private/api/v1/BankOperation/RequestMovementsAfterTurnIntegrationByFirm", dto).ConfigureAwait(false);
        }
    }
}