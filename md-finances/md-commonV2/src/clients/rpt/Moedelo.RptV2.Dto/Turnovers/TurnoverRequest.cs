using System;

namespace Moedelo.RptV2.Dto.Turnovers
{
    public class TurnoverRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsBalance { get; set; }
    }
}
