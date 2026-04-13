using System.Collections.Generic;

namespace Moedelo.RptV2.Dto.OutsourceCalendar
{
    public class AttachEventToFirmsDto
    {
        public int CalendarId { get; set; }

        public short StartDay { get; set; }

        public short StartMonth { get; set; }

        public short EndDay { get; set; }

        public short EndMonth { get; set; }

        public List<int> FirmIds { get; set; }
    }
}