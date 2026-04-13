using System.Collections.Generic;

namespace Moedelo.RptV2.Dto.OutsourceCalendar
{
    public class DetachEventFromFirmsDto
    {
        public int CalendarId { get; set; }

        public List<int> FirmIds { get; set; }
    }
}
