using System;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.Invoice;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationMonitor;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation.PaymentRegistry;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;

namespace Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore
{
    [InjectAsSingleton(typeof(IBankOperationClient))]
    public class BankOperationClient : BaseApiClient, IBankOperationClient
    {
        private static readonly HttpQuerySetting CreateSalaryPaymentRegistryHttpSetting = new(new TimeSpan(0, 0, 2, 30));

        public BankOperationClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BankOperationClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationApiNetCore"),
                logger)
        {
        }

        public async Task<CertificateThumbprintDto> GetCertificateThumbprintByPartnerAsync(IntegrationPartners partner)
        {
            var result = await GetAsync<ApiDataResult<CertificateThumbprintDto>>($"/private/api/v1/BankOperation/GetCertificateThumbprint?partner={(int)partner}");
            return result.data;
        }

        public Task<Dto.BankOperation.IntegrationResponseDto<SendBankInvoiceResponseDto>> SendInvoiceAsync(SendBankInvoiceRequestDto dto)
        {
            return PostAsync<SendBankInvoiceRequestDto, Dto.BankOperation.IntegrationResponseDto<SendBankInvoiceResponseDto>>("/private/api/v1/BankOperation/SendInvoice", dto);
        }

        public async Task<SalaryPaymentRegistryCreationResponseDto> CreateSalaryPaymentRegistryAsync(SalaryPaymentRegistryCreationRequestDto dto)
        {
            var result =
                await PostAsync<SalaryPaymentRegistryCreationRequestDto,
                    ApiDataResult<SalaryPaymentRegistryCreationResponseDto>>(
                    "/private/api/v1/BankOperation/CreatePaymentRegistry", dto, null,
                    CreateSalaryPaymentRegistryHttpSetting);
            return result.data;
        }

        public async Task RequestMovementsAfterTurnIntegrationByFirmAsync(RequestMovementForAllSettlementsDto dto)
        {
            await PostAsync("/private/api/v1/BankOperation/RequestMovementsAfterTurnIntegrationByFirm", dto);
        }
        
        public async Task<bool> SendAutoTodayRequestAsync(IntegrationPartners partner)
        {
            var uri = $"/private/api/v1/BankOperation/SendAutoTodayRequest?integrationPartner={(int)partner}";
            var result = await GetAsync<bool>(uri);
            return result;
        }
    }
}