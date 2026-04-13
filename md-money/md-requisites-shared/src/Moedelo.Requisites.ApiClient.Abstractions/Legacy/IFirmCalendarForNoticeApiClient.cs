using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Requisites.ApiClient.Abstractions.Legacy;

public interface IFirmCalendarForNoticeApiClient
{
    Task<CalendarEventNoticeDto[]> GetCalendarForNoticeAsync(FirmCalendarForNoticeRequestDto requestDto);

    Task<IReadOnlyDictionary<int, List<CalendarEventNoticeDto>>> GetFirmsCalendarForNoticeAsync(FirmsCalendarForNoticeRequestDto requestDto, CancellationToken cancellationToken = default);
}