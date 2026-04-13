using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IFirmInfoApiClient))]
    internal sealed class FirmInfoApiClient : BaseLegacyApiClient, IFirmInfoApiClient
    {
        public FirmInfoApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FirmRequisitesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public Task<InvoiceSignerDto> GetInvoiceSignerAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/FirmInfo/GetInvoiceSigner?firmId={firmId}&userId={userId}";

            return GetAsync<InvoiceSignerDto>(uri);
        }

        public Task<AutoLinkSettingsDto> GetAutoLinkSettingsAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/FirmInfo/GetAutoLinkSettings?firmId={firmId}&userId={userId}";

            return GetAsync<AutoLinkSettingsDto>(uri);
        }

        public Task SetAutoLinkSettingsAsync(FirmId firmId, UserId userId, AutoLinkSettingsDto settings)
        {
            var uri = $"/FirmInfo/SetAutoLinkSettings?firmId={firmId}&userId={userId}";

            return PostAsync(uri, settings);
        }
    }
}
