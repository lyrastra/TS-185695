using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.SberbankPayment;
using Moedelo.BankIntegrations.ApiClient.Dto.SberbankAcceptance;
using Moedelo.BankIntegrations.ApiClient.Dto.SberbankPaymentRequest;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.SberbankPayment
{
    [InjectAsSingleton(typeof(ISberbankPaymentAdapterClient))]
    public class SberbankPaymentAdapterClient : BaseApiClient, ISberbankPaymentAdapterClient
    {
        public SberbankPaymentAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SberbankPaymentAdapterClient> logger) : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("IntegrationApi"),
            logger)
        {
        }

        public Task<AdvanceAcceptanceReponseDto> GetAllowedAdvanceAcceptancesByFirmIdListAsync(
            IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<GetAllowedAdvanceAcceptancesByFirmIdListDto, AdvanceAcceptanceReponseDto>(
                "/SberbankPaymentRequest/GetAllowedAdvanceAcceptancesByFirmIdListAsync",
                new GetAllowedAdvanceAcceptancesByFirmIdListDto
                {
                    FirmIdList = firmIds
                });
        }

        public Task<int> CreateAcceptanceAsync(SberbankAcceptanceDto dto)
        {
            return PostAsync<SberbankAcceptanceDto, int>(
                "/SberbankPaymentRequest/CreateAcceptance", dto);
        }
    }
}