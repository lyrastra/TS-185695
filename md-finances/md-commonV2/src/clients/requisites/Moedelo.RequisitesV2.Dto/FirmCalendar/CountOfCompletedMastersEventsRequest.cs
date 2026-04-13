using System.Collections.Generic;

namespace Moedelo.RequisitesV2.Dto.FirmCalendar
{
    public class CountOfCompletedMastersEventsRequest
    {
        public IList<int> FirmIdsList { get; set; }
        public int Year { get; set; }
    }
}