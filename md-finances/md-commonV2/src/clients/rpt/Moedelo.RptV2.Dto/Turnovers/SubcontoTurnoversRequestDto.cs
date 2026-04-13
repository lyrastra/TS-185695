using System;

namespace Moedelo.RptV2.Dto.Turnovers
{
    public class SubcontoTurnoversRequestDto
    {
        public long AccountId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? SubcontoType { get; set; }
    }
}
