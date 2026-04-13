using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Billing.Abstractions.Legacy.Dto;
using Moedelo.Billing.Abstractions.Legacy.Interfaces;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Billing.Clients.Legacy;

[InjectAsSingleton(typeof(IFirmBillingStateApiClient))]
internal sealed class FirmBillingStateApiClient : BaseLegacyApiClient, IFirmBillingStateApiClient
{
    public FirmBillingStateApiClient(
        IHttpRequestExecuter httpRequestExecutor,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<FirmBillingStateApiClient> logger)
        : base(
            httpRequestExecutor,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("InternalBillingApiEndpoint"),
            logger)
    {
    }

    public Task<FirmBillingStateDto> GetActualAsync(FirmId firmId)
    {
        return GetAsync<FirmBillingStateDto>($"/FirmState/Actual?firmId={firmId}");
    }

    public Task<IReadOnlyCollection<FirmBillingStateDto>> GetActualListAsync(
        IReadOnlyCollection<int> firmIds,
        HttpQuerySetting httpQuerySetting = null,
        CancellationToken cancellationToken = default)
    {
        return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<FirmBillingStateDto>>(
            "/FirmState/GetActualList",
            firmIds,
            setting: httpQuerySetting,
            cancellationToken: cancellationToken);
    }
}