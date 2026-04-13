using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RequisitesV2.Dto.FirmCalendar;

namespace Moedelo.RequisitesV2.Client.FirmCalendarForNotice
{
    public interface IFirmCalendarForNoticeApiClient : IDI
    {
        Task<List<CalendarEventNoticeDto>> GetCalendarForNoticeAsync(FirmCalendarForNoticeRequestDto requestDto);

        Task<IReadOnlyDictionary<int, List<CalendarEventNoticeDto>>> GetFirmsCalendarForNoticeAsync(
            FirmsCalendarForNoticeRequestDto requestDto,
            CancellationToken cancellationToken = default);
    }
}