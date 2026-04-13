using System;
using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Dto.Money
{
    public class ReconciliationResponseDto
    {
        public Guid SessionId { get; set; }

        public int SettlementAccountId { get; set; }

        public DateTime CreateDate { get; set; }

        public ReconciliationStatus ReconciliationStatus { get; set; }
    }
}