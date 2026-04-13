using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.RptV2.Dto.Turnovers
{
    public class SubcontoTurnoverDto
    {
        public long SubcontoId { get; set; }

        public SubcontoType SubcontoType { get; set; }

        public decimal IncomingDebit { get; set; }

        public decimal IncomingCredit { get; set; }

        public decimal CurDebit { get; set; }

        public decimal CurCredit { get; set; }

        public decimal OutgoingDebit { get; set; }

        public decimal OutgoingCredit { get; set; }
    }
}
