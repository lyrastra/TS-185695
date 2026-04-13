using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Legacy.Wrappers;

namespace Moedelo.Requisites.ApiClient.Legacy;

[InjectAsSingleton(typeof(IFirmCalendarForNoticeApiClient))]
public class FirmCalendarForNoticeApiClient : BaseLegacyApiClient, IFirmCalendarForNoticeApiClient
{
    public FirmCalendarForNoticeApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<FirmCalendarForNoticeApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            auditHeadersGetter,
            settingRepository.Get("FirmRequisitesApiEndpoint"),
            logger)
    {
    }

    public async Task<CalendarEventNoticeDto[]> GetCalendarForNoticeAsync(FirmCalendarForNoticeRequestDto requestDto)
    {
        var result =
            await GetAsync<ListResponseWrapper<CalendarEventNoticeDto>>("/FirmCalendarForNotice/GetAsync", requestDto).ConfigureAwait(false);

        return result.Items;
    }

    public Task<IReadOnlyDictionary<int, List<CalendarEventNoticeDto>>> GetFirmsCalendarForNoticeAsync(
        FirmsCalendarForNoticeRequestDto requestDto,
        CancellationToken cancellationToken)
    { 
        return PostAsync<FirmsCalendarForNoticeRequestDto,IReadOnlyDictionary<int, List<CalendarEventNoticeDto>>>(
            "/FirmCalendarForNotice/GetAsync",
            requestDto,
            cancellationToken: cancellationToken);
    }
}